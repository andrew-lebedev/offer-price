namespace OfferPrice.Auction.Api.Models.Requests;

public class RaiseBetRequest
{
    public string UserId { get; set; }
    public decimal Raise { get; set; }
}