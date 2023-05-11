namespace OfferPrice.Auction.Domain;
public class Auction
{
    public Auction()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public Product Product { get; set; }

    public User Winner { get; set; }

    public string Status { get; set; }

    public List<Bet> BetHistory { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}
