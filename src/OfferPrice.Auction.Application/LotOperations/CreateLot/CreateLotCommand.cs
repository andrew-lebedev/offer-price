using MediatR;
using OfferPrice.Auction.Application.Models;
using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Application.LotOperations.CreateLot;

public class CreateLotCommand : IRequest
{
    public string UserId { get; set; }

    public Product InsertProduct { get; set; }

    public AuctionType AuctionType { get; set; }

    public string AdditionalInfo { get; set; }
}
