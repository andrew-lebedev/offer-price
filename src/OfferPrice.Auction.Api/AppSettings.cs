using OfferPrice.Auction.Api.Jobs;
using OfferPrice.Auction.Infrastructure;
using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Auction.Api;

public class AppSettings
{
    public DatabaseSettings Database { get; set; }
    public RabbitMqSettings RabbitMq { get; set; }
    public AuctionSettings Auction { get; set; }
}