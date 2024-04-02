using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Api.Models.Requests;

public class LotRequest
{
    public ProductRequest InsertProductRequest { get; set; }

    public AuctionType AuctionType { get; set; }

    public string AdditionalInfo { get; set; }
}
