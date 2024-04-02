using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Api.Models.Responses;

public class AuctionFinishedResponse
{
    public AuctionFinishedResponse(Domain.Models.Lot lot)
    {
        Status = lot.Status;
        Winner = User.FromDomain(lot.Winner);
        ProductPrice = lot.Price;
    }

    public LotStatus Status { get; set; }
    public User Winner { get; set; }
    public decimal ProductPrice { get; set; }
}