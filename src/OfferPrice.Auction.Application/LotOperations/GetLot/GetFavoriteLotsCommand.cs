using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Common;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetFavoriteLotsCommand : IRequest<PageResult<Lot>>
{
    public string UserId { get; set; }

    public Paging Paging { get; set; }
}
