using OfferPrice.Auction.Application.Models;
using OfferPrice.Common;
using System.Collections.Generic;
using System.Linq;

namespace OfferPrice.Auction.Api.Models.Responses;

public class FindLotsResponse
{
    public FindLotsResponse(PageResult<Lot> pageResult)
    {
        Lots = pageResult.Items.Select(x => new Lot(x)).ToList();
        Total = pageResult.Total;
    }

    public List<Lot> Lots { get; set; }
    public long Total { get; set; }
}