
namespace OfferPrice.Auction.Domain;

public class Bet
{
    public string Id { get; set; }

    public string User { get; set; } // todo change to user

    public Lot Lot { get; set; }

    public decimal Price { get; set; }
}

