
namespace OfferPrice.Auction.Domain;

public class Bet
{
    public string Id { get; set; }

    public User User { get; set; }

    public Auction Auction { get; set; }

    public decimal Price { get; set; }
}

