using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models;
using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Api.Controllers;
[ApiController]
[Route("api/auctions")]
public class AuctionController : ControllerBase
{
    private readonly IAuctionRepository _auctions;

    private readonly IMapper _mapper;

    public AuctionController(IAuctionRepository auctions, IMapper mapper)
    {
        _auctions = auctions;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAuctions(CancellationToken token)
    {
        var auctions = await _auctions.Get(token);

        return Ok(auctions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuctionById([FromRoute] string id, CancellationToken token)
    {
        var auction = await _auctions.GetById(id, token);

        if (auction == null)
        {
            return NotFound();
        }

        var auctionResponse = _mapper.Map<Models.Auction>(auction);

        return Ok(auctionResponse);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAuction([FromBody] AuctionRequest auctionRequest, CancellationToken token)
    {
        var auction = _mapper.Map<Domain.Auction>(auctionRequest);

        await _auctions.Create(auction, token);

        var auctionResponse = _mapper.Map<Models.Auction>(auction);

        return Ok(auctionResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuction([FromRoute] string id, [FromBody] AuctionRequest auctionRequest, CancellationToken token)
    {
        var auction = await _auctions.GetById(id, token);

        if (auction == null)
        {
            return NotFound();
        }

        var auctionUpdate = _mapper.Map(auctionRequest, auction);

        await _auctions.Update(auctionUpdate, token);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuction([FromRoute] string id, CancellationToken token)
    {
        await _auctions.Delete(id, token);

        return Ok();
    }
}
