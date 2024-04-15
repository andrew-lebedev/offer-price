using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetLotWithUserBetsCommand : IRequest<IEnumerable<Lot>>
{
    public string UserId { get; set; }
}
