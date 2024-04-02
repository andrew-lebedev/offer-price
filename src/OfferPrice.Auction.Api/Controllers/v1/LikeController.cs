using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Auction.Domain.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using OfferPrice.Common;

namespace OfferPrice.Auction.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/lots/{lotId}/likes")]
public class LikeController : ControllerBase
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public LikeController(ILikeRepository likeRepository, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _mapper = mapper;
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetCountsOfLikes([FromRoute] string lotId, CancellationToken token)
    {
        var count = await _likeRepository.GetCount(lotId, token);

        return Ok(count);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLike([FromRoute] string lotId, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var like = new Domain.Models.Like
        {
            LotId = lotId,
            UserId = userId
        };

        await _likeRepository.Create(like, token);

        return Ok(like);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLike([FromRoute] string lotId, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        await _likeRepository.Delete(lotId, userId, token);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetLike([FromRoute] string lotId, CancellationToken token)
    {
        var userId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var like = await _likeRepository.Get(lotId, userId, token);

        if (like == null)
        {
            return NotFound();
        }

        var likeResponse = _mapper.Map<Models.Like>(like);

        return Ok(likeResponse);
    }
}
