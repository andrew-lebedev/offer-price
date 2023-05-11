using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Api.Models
{
    public class Auction
    {
        public string Id { get; set; }
        public Product Product { get; set; }

        public User Winner { get; set; }

        public List<BetRequest> BetHistory { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}
