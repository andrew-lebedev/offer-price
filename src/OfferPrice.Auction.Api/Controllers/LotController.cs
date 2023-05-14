using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models;
using OfferPrice.Auction.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Controllers;
[ApiController]
[Route("api/lots")]
public class LotController : ControllerBase
{
    private readonly ILotRepository _lots;

    private readonly IMapper _mapper;

    public LotController(ILotRepository lots, IMapper mapper)
    {
        _lots = lots;
        _mapper = mapper;
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

        return Ok(_mapper.Map<Models.Lot>(lot));
    }

    [HttpPost("{id}/schedule")]
    public async Task<IActionResult> Schedule(string id, [FromBody] ScheduleLotRequest request, CancellationToken cancellationToken)
    {
        var lot = await _lots.Get(id, cancellationToken);
        if (lot == null)
        {
            return NotFound();
        }

        if (lot.Product.UserId != request.UserId)
        {
            return Conflict();
        }

        lot.Schedule(request.Start);
        await _lots.Update(lot, cancellationToken);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] AuctionRequest auctionRequest, CancellationToken token)
    {
        var lot = await _lots.Get(id, token);

        if (lot == null)
        {
            return NotFound();
        }

        var update = _mapper.Map(auctionRequest, lot);
        await _lots.Update(update, token);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuction([FromRoute] string id, CancellationToken token)
    {
        await _lots.Delete(id, token);

        return Ok();
    }
}
