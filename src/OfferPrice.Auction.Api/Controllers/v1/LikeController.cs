using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Api.Models.Responses;
using OfferPrice.Auction.Application.LikeOperations.CreateLike;
using OfferPrice.Auction.Application.LikeOperations.DeleteLike;
using OfferPrice.Auction.Application.LikeOperations.GetLike;
using OfferPrice.Common;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/lots/{lotId}/likes")]
public class LikeController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public LikeController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCountsOfLikes([FromRoute] string lotId, CancellationToken token)
    {
        var cmd = new GetCountLikeCommand() { LotId = lotId };
        var count = await _mediator.Send(cmd, token);

        return Ok(count);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLike([FromRoute] string lotId, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new CreateLikeCommand() { LotId = lotId, UserId = userId };

        await _mediator.Send(cmd, token);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLike([FromRoute] string lotId, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new DeleteLikeCommand() { LotId =  lotId, UserId = userId };

        await _mediator.Send(cmd, token);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetLike([FromRoute] string lotId, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new GetLikeCommand() { LotId = lotId, UserId = userId };

        var like = await _mediator.Send(cmd, token);

        var likeResponse = _mapper.Map<LikeResponse>(like);

        return Ok(likeResponse);
    }
}
