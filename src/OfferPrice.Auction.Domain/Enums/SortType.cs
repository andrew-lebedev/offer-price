using System.ComponentModel;

namespace OfferPrice.Auction.Domain.Enums;

public enum SortType
{
    [Description("По новизне")]
    Novetly = 0,

    [Description("По цене восходящей")]
    Ascending = 1,

    [Description("По цене низходящей")]
    Descending = 2
}
