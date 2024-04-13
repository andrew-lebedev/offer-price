using MediatR;

namespace OfferPrice.Auction.Application.LotOperations.DeleteLot;

public class DeleteLotCommand : IRequest
{
    public string Id { get; set; }

    public string UserId { get; set; }
}
