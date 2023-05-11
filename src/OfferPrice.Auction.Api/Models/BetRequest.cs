using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Api.Models;
public class BetRequest
{
    public User User { get; set; }

    public AuctionRequest Auction { get; set; }

    public decimal Price { get; set; }
}

