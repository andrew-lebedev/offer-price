using OfferPrice.Auction.Domain;
using OfferPrice.Common;

namespace OfferPrice.Auction.Api.Models;

public class FindLotsRequest
{
    public int Page { get; set; }
    public int PerPage { get; set; }

    public FindLotsQuery ToQuery()
    {
        return new() { Paging = new Paging(Page, PerPage) };
    }
}