using OfferPrice.Auction.Domain;
using OfferPrice.Common;
using System;

namespace OfferPrice.Auction.Api.Models;

public class FindLotsRequest
{
    public string ProductOwnerId { get; set; }
    public string[] Statuses { get; set; }
    public int Page { get; set; }
    public int PerPage { get; set; }

    public FindLotsQuery ToQuery()
    {
        return new()
        {
            ProductOwnerId = ProductOwnerId,
            Statuses = Statuses ?? Array.Empty<string>(),
            Paging = new Paging(Page, PerPage)
        };
    }
}