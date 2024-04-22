using OfferPrice.Auction.Domain.Enums;
using System;
using OfferPrice.Auction.Application.Models;
using System.Linq;

namespace OfferPrice.Auction.Api.Models.Responses;

public class GetUserBetResponse
{
    public GetUserBetResponse(Lot lot)
    {
        Id = lot.Id;
        Product = lot.Product;
        CurrentBet = lot.CurrentPrice;

        var lastIndex = lot.BetHistory.FindLastIndex(lot.BetHistory.Count - 1, x => x.User.Id == lot.Product.User.Id);
        UserBet = lastIndex == -1 ? 0 : 
            lot.BetHistory
                .TakeLast(lastIndex)
                .Sum(x => x.Raise) + lot.CurrentPrice;

        Status = lot.Status;
        Start = lot.Start;
        End = lot.End;
    }

    public string Id { get; set; }
    public Product Product { get; set; }
    public decimal CurrentBet { get; set; }
    public decimal UserBet { get; set; }
    public LotStatus Status { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
