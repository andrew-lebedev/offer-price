using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.LikeOperations.CreateLike;

public class CreateLikeCommand : IRequest
{
    public string LotId { get; set; }

    public string UserId { get; set; }
}
