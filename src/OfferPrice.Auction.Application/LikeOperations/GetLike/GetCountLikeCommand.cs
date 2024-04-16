using MediatR;

namespace OfferPrice.Auction.Application.LikeOperations.GetLike;

public class GetCountLikeCommand : IRequest<long>
{
    public string LotId { get; set; }
}
