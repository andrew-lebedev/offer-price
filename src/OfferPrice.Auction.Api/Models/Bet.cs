using System;

namespace OfferPrice.Auction.Api.Models;

public class Bet
{
    public Bet(Domain.Bet bet)
    {
        Id = bet.Id;
        User = bet.User;
        Raise = bet.Raise;
        Timestamp = bet.Timestamp;
    }
    
    public string Id { get; set; }
    public string User { get; set; } // todo: change to user
    public decimal Raise { get; set; }
    public DateTime Timestamp { get; set; }
}