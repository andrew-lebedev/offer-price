using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models;
using OfferPrice.Auction.Api.Models.Requests;
using OfferPrice.Auction.Api.Models.Responses;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Common;
using OfferPrice.Common.Extensions;
using OfferPrice.Events.Events;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/lots")]
public class LotController : ControllerBase
{
    private readonly ILotRepository _lots;
    private readonly IUserRepository _users;
    private readonly IPublishEndpoint _publishEndpoint;

    private readonly IMapper _mapper;

    public LotController(
        ILotRepository lots,
        IUserRepository users,
        IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _lots = lots;
        _users = users;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindLotsRequest request, CancellationToken token)
    {
        var result = await _lots.Find(request.ToQuery(), token);

        return Ok(new FindLotsResponse(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, CancellationToken token)
    {
        var lot = await _lots.Get(id, token);

        if (lot == null)
        {
            return NotFound();
        }

        return Ok(_mapper.Map<Lot>(lot));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LotRequest createLotRequest, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);
        var user = await _users.Get(userId, cancellationToken);

        if (user == null)
        {
            return Conflict();
        }

        var lot = _mapper.Map<Domain.Models.Lot>(createLotRequest);

        await _lots.Create(lot, cancellationToken);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] LotRequest updateLotRequest, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var lot = _mapper.Map<Domain.Models.Lot>(updateLotRequest);

        await _lots.Update(lot, userId, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}/delivery")]
    public async Task<IActionResult> Deliver([FromRoute] string id, CancellationToken token)
    {
        var lot = await _lots.Get(id, token);

        if (lot == null)
        {
            return NotFound();
        }

        if (!lot.IsSold())
        {
            return Conflict();
        }

        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        lot.Deliver();
        await _lots.Update(lot, userId, token);

        await _publishEndpoint.Publish<LotStatusUpdatedEvent>(
            new
            {
                LotId = lot.Id,
                ProductId = lot.Product.Id,
                Status = lot.Status.ToString(),
            }, token);

        return Ok();
    }

    [HttpPost("{id}/schedule")]
    public async Task<IActionResult> Schedule([FromRoute] string id, [FromBody] ScheduleLotRequest request, CancellationToken cancellationToken)
    {
        var lot = await _lots.Get(id, cancellationToken);
        if (lot == null)
        {
            return NotFound();
        }

        var user = await _users.Get(request.UserId, cancellationToken);
        if (user == null)
        {
            return Conflict();
        }

        if (lot.Product.User.Id != request.UserId)
        {
            return Conflict();
        }

        if (lot.IsStarted())
        {
            return Conflict();
        }

        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        lot.Schedule(request.Start.RemoveSeconds());
        await _lots.Update(lot, userId, cancellationToken);

        //_producer.SendMessage(new LotStatusUpdatedEvent
        //{
        //    LotId = lot.Id,
        //    ProductId = lot.Product.Id,
        //    Status = lot.Status.ToString()
        //});

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuction([FromRoute] string id, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        await _lots.Delete(id, userId, token);

        return Ok();
    }
}
