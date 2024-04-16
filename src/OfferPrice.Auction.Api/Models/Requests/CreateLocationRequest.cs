using System.Collections.Generic;

namespace OfferPrice.Auction.Api.Models.Requests;

public class CreateLocationRequest
{
    public string City { get; set; }

    public IEnumerable<string> Disctricts { get; set; }
}
