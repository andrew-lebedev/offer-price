using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.LotOperations.GetLot;

public class GetLotCommand : IRequest<Lot>
{
    public string Id { get; set; }
}
