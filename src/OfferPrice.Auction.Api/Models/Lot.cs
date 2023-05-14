﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OfferPrice.Auction.Api.Models;

public class Lot
{
    public Lot(Domain.Lot lot)
    {
        Id = lot.Id;
        Product = new Product(lot.Product);
        Winner = lot.Winner;
        BetHistory = lot.BetHistory.Select(x => new Bet(x)).ToList();
        Status = lot.Status;
        Start = lot.Start;
        End = lot.End;
    }
    
    public string Id { get; set; }
    public Product Product { get; set; }
    public string Winner { get; set; } // todo: change to entity
    public List<Bet> BetHistory { get; set; }
    public string Status { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}