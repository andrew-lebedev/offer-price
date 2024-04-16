using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Application.Models;

public class Lot
{
    public Lot(Lot lot)
    {
        Id = lot.Id;
        Product = new Product(lot.Product);
        Winner = User.FromDomain(lot.Winner);
        BetHistory = lot.BetHistory.Select(x => new Bet(x)).ToList();
        Price = lot.Price;
        Status = lot.Status;
        Start = lot.Start;
        End = lot.End;
        AuctionType = lot.AuctionType;
    }

    public string Id { get; set; }
    public Product Product { get; set; }
    public User Winner { get; set; }
    public List<Bet> BetHistory { get; set; }
    public decimal Price { get; set; }
    public LotStatus Status { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public AuctionType AuctionType { get; set; }
    public string AdditionalInfo { get; set; }
}