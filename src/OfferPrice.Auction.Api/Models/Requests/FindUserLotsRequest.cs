using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Api.Models.Requests;

public class FindUserLotsRequest
{
    public string ProductOwnerId { get; set; }

    public string WinnerId { get; set; }

    public LotStatus Status { get; set; }

    public int Page { get; set; }

    public int PerPage { get; set; }
}
