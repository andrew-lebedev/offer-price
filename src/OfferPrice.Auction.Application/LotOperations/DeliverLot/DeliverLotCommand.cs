using MediatR;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Application.LotOperations.DeliverLot;

public class DeliverLotCommand : IRequest<Lot>
{
    public string Id { get; set; }

    public string UserId { get; set; }
}
