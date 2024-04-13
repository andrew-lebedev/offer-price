namespace OfferPrice.Auction.Application.Models;

public class Bet
{
    public Bet(Bet bet)
    {
        User = User.FromApplication(bet.User);
        Raise = bet.Raise;
        Timestamp = bet.Timestamp;
    }

    public User User { get; set; }
    public decimal Raise { get; set; }
    public DateTime Timestamp { get; set; }
}