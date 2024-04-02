using System.ComponentModel;

namespace OfferPrice.Auction.Domain.Enums;

public enum ProductStatus
{
    [Description("The new one")]
    New = 0,

    [Description("Had at least one owner")]
    Used = 1,
}
