using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Application.LotOperations.UpdateLot;

public class UpdateLotCommand : IRequest
{
    public string UserId { get; set; }

    public Product InsertProductRequest { get; set; }

    public AuctionType AuctionType { get; set; }

    public string AdditionalInfo { get; set; }
}
