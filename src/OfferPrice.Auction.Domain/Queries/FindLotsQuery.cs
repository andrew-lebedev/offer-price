using OfferPrice.Auction.Domain.Enums;
using OfferPrice.Common;

namespace OfferPrice.Auction.Domain.Queries;

public class FindLotsQuery
{
    public string ProductOwnerId { get; set; }

    public string WinnerId { get; set; }

    public LotStatus? LotStatus { get; set; }

    public string Location { get; set; }

    public string Category { get; set; }

    public decimal? MinPrice { get; set; }

    public decimal? MaxPrice { get; set; }

    public ProductStatus? ProductStatus { get; set; }

    public bool OnlyWithPhotos { get; set; }

    public bool OnlyWithVideos { get; set; }

    public SortType? SortType { get; set; }

    public Paging Paging { get; set; }
}