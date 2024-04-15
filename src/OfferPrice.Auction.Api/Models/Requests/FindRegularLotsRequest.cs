using OfferPrice.Auction.Domain.Enums;
using OfferPrice.Auction.Domain.Queries;
using OfferPrice.Common;

namespace OfferPrice.Auction.Api.Models.Requests;

public class FindRegularLotsRequest
{
    public string Location { get; set; }

    public string Category { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public ProductStatus ProductStatus { get; set; }

    public bool OnlyWithPhotos { get; set; }

    public bool OnlyWithVideos { get; set; }

    public SortType SortType { get; set; }

    public int Page { get; set; }

    public int PerPage { get; set; }

    public FindLotsQuery ToQuery()
    {
        return new()
        {
            Paging = new Paging(Page, PerPage)
        };
    }
}