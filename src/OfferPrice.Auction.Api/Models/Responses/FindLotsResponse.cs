using OfferPrice.Auction.Application.Models;
using OfferPrice.Common;
using System.Collections.Generic;
using System.Linq;

namespace OfferPrice.Auction.Api.Models.Responses;

public class FindLotsResponse
{
    public FindLotsResponse(PageResult<Lot> pageResult)
    {
        Lots = pageResult.Items;
        Total = pageResult.Total;
        Page = pageResult.Page;
        PerPage = pageResult.PerPage;
    }

    public List<Lot> Lots { get; set; }
    public long Total { get; set; }
    public int Page {  get; set; }
    public int PerPage { get; set; }
}