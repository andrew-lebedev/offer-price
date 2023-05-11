using OfferPrice.Auction.Domain;
using System.ComponentModel.DataAnnotations;

namespace OfferPrice.Auction.Api.Models;
public class AuctionRequest
{
    [Required]
    public Product Product { get; set; }

    [Required]
    public List<BetRequest> BetHistory { get; set; }

    [Required]
    public DateTime Start { get; set; }
}

