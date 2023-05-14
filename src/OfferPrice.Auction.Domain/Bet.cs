using System;

namespace OfferPrice.Auction.Domain;

public class Bet
{
    public string Id { get; set; }

    public string User { get; set; } // todo change to user

    public decimal Raise { get; set; }
    
    public DateTime Timestamp { get; set; }
}

