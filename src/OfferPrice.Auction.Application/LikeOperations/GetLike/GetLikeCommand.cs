using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.LikeOperations.GetLike;

public class GetLikeCommand : IRequest<Like>
{
    public string LotId { get; set; }

    public string UserId { get; set; }
}
