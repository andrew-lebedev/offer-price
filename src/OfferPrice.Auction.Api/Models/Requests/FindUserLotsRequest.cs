using OfferPrice.Auction.Domain.Enums;
using OfferPrice.Auction.Domain.Queries;
using OfferPrice.Common;

namespace OfferPrice.Auction.Api.Models.Requests;

public class FindUserLotsRequest
{
    public string WinnerId { get; set; }

    public LotStatus Status { get; set; }

    public int Page { get; set; }

    public int PerPage { get; set; }

    public FindLotsQuery ToQuery(string userId)
    {
        return new()
        {
            ProductOwnerId = userId,
            WinnerId = WinnerId,
            LotStatus = Status,
            Paging = new Paging(Page, PerPage)
        };
    }
}
