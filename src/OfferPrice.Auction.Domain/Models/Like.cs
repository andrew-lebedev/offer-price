using System;

namespace OfferPrice.Auction.Domain.Models;

public class Like
{
    public Like()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public string UserId { get; set; }

    public string LotId { get; set; }
}
