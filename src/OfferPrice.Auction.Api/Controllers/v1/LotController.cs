using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models.Requests;
using OfferPrice.Auction.Api.Models.Responses;
using OfferPrice.Auction.Application.LotOperations.CreateLot;
using OfferPrice.Auction.Application.LotOperations.DeleteLot;
using OfferPrice.Auction.Application.LotOperations.DeliverLot;
using OfferPrice.Auction.Application.LotOperations.GetLot;
using OfferPrice.Auction.Application.LotOperations.UpdateLot;
using OfferPrice.Common;
using OfferPrice.Events.Events;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/lots")]
public class LotController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LotController(
        IMapper mapper,
        IPublishEndpoint publishEndpoint,
        IMediator mediator)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] FindRegularLotsRequest request, CancellationToken token)
    {
        var cmd = new GetRegularLotsCommand() { Query = request.ToQuery() };

        var result = await _mediator.Send(cmd, token);

        return Ok(new FindLotsResponse(result));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, CancellationToken token)
    {
        var cmd = new GetLotCommand() { Id = id };

        var lot = await _mediator.Send(cmd, token);

        return Ok(_mapper.Map<GetLotResponse>(lot));
    }

    [HttpGet("user")] //todo
    public async Task<IActionResult> GetUserLots([FromQuery] FindUserLotsRequest request, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new GetUserLotsCommand() { Query = request.ToQuery(userId) };

        var lots = await _mediator.Send(cmd, cancellationToken);

        return Ok(new FindLotsResponse(lots));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] LotRequest createLotRequest, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = _mapper.Map<CreateLotCommand>(createLotRequest);
        cmd.UserId = userId;

        await _mediator.Send(cmd, cancellationToken);

        await _publishEndpoint.Publish<NotificationCreateEvent>(new()
        {
            UserId = userId,
            Subject = "OfferPrice",
            Body = "You've successfully create lot"
        });

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromBody] LotRequest updateLotRequest, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);
        var cmd = _mapper.Map<UpdateLotCommand>(updateLotRequest);
        cmd.UserId = userId;

        await _mediator.Send(cmd, cancellationToken);

        return Ok();
    }

    [HttpPost("{id}/delivery")]
    public async Task<IActionResult> Deliver([FromRoute] string id, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new DeliverLotCommand() { Id = id, UserId = userId };

        var lot = await _mediator.Send(cmd, token);

        await _publishEndpoint.Publish<LotStatusUpdatedEvent>(
            new
            {
                LotId = lot.Id,
                ProductId = lot.Product.Id,
                Status = lot.Status.ToString(),
            }, token);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuction([FromRoute] string id, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new DeleteLotCommand() { Id = id, UserId = userId };

        await _mediator.Send(cmd, token);

        return Ok();
    }

    [HttpGet("favorites")]
    public async Task<IActionResult> GetFavorites([FromQuery] Paging paging, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new GetFavoriteLotsCommand() { UserId = userId, Paging = paging };

        var favorities = await _mediator.Send(cmd, cancellationToken);

        return Ok(favorities);
    }

    [HttpGet("with-user-bets")]
    public async Task<IActionResult> GetUserBets([FromQuery] Paging paging, CancellationToken cancellationToken)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);
        var cmd = new GetLotsWithUserBetsCommand()
        {
            Query = new Domain.Queries.FindLotsQuery()
            {
                BetsWithUserId = userId,
                Paging = paging
            }
        };

        var result = await _mediator.Send(cmd, cancellationToken);

        return Ok(new PageResult<GetUserBetResponse>(
            result.Page,
            result.PerPage,
            result.Total,
            result.Items.Select(x => new GetUserBetResponse(x)).ToList()));
    }
}
