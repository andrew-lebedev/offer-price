using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Api.Models
{
    public class Bet
    {
        public int Id { get; set; }

        public User User { get; set; }

        public Models.Auction Auction { get; set; }

        public decimal Price { get; set; }
    }
}
