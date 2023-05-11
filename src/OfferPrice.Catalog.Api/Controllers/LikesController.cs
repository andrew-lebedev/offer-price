using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Catalog.Api.Models;
using OfferPrice.Catalog.Domain;

namespace OfferPrice.Catalog.Api.Controllers;

[ApiController]
[Route("api/products")]
public class LikesController : ControllerBase //todo
{
    private readonly ILikeRepository _likes;

    private readonly IMapper _mapper;

    public LikesController(ILikeRepository likes, IMapper mapper)
    {
        _likes = likes;
        _mapper = mapper;
    }

    [HttpGet("{productId}/likes")]
    public async Task<IActionResult> GetCountsOfLikes([FromRoute] string productId, CancellationToken token)
    {
        var count = await _likes.GetCountById(productId, token);

        return Ok(count);
    }

    [HttpPost("likes")]
    public async Task<IActionResult> CreateLike([FromBody] LikeRequest likeRequest, CancellationToken token)
    {
        var like = _mapper.Map<Domain.Like>(likeRequest);

        await _likes.Create(like, token);

        return Ok(like);
    }

    [HttpDelete("{id}/likes")]
    public async Task<IActionResult> DeleteLike([FromRoute] string id, CancellationToken token)
    {
        await _likes.Delete(id, token);

        return Ok();
    }

    [HttpGet("{productId}/likes/by/{userId}")]
    public async Task<IActionResult> IsProductLikedByUser([FromRoute] string productId, [FromRoute] string userId, CancellationToken token)
    {
        var like = await _likes.GetByProductAndUserId(productId, userId, token);

        if (like == null)
        {
            return NotFound();
        }
        else
        {
            var likeResponse = _mapper.Map<Models.Like>(like);

            return Ok(likeResponse);
        }
    }
}

