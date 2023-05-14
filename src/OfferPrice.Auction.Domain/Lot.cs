using System;
using System.Collections.Generic;

namespace OfferPrice.Auction.Domain;
public class Lot
{
    public Lot()
    {
        Id = Guid.NewGuid().ToString();
        BetHistory = new List<Bet>();
    }

    public string Id { get; set; }

    public Product Product { get; set; }

    public string Winner { get; set; } // todo: change to user

    public string Status { get; set; }

    public List<Bet> BetHistory { get; set; }

    public DateTime? Start { get; set; }

    public DateTime? End { get; set; }

    public void Schedule(DateTime date)
    {
        Start = date;
        Status = "Planned"; // todo: to const
    }
}
