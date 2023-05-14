using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Auction.Api;

public class AppSettings
{
    public DatabaseSettings Database { get; set; }
    public RabbitMqSettings RabbitMq { get; set; }
}