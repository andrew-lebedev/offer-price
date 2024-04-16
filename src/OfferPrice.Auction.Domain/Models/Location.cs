using System.Collections.Generic;

namespace OfferPrice.Auction.Domain.Models;

public class Location
{
    public string Id { get; set; }

    public string City { get; set; }

    public IEnumerable<string> Districts { get; set; }
}
