using MediatR;

namespace OfferPrice.Auction.Application.LikeOperations.DeleteLike;

public class DeleteLikeCommand : IRequest
{
    public string LotId { get; set; }

    public string UserId { get; set; }
}
