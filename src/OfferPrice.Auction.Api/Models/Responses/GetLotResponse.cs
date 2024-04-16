using OfferPrice.Auction.Domain.Enums;
using System.Collections.Generic;
using System;
using OfferPrice.Auction.Application.Models;

namespace OfferPrice.Auction.Api.Models.Responses;

public class GetLotResponse
{
    public string Id { get; set; }
    public Product Product { get; set; }
    public User Winner { get; set; }
    public List<Bet> BetHistory { get; set; }
    public decimal Price { get; set; }
    public string Status { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public AuctionType AuctionType { get; set; }
    public string AdditionalInfo { get; set; }
}
