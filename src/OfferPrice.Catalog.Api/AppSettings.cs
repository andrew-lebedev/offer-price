using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Catalog.Api;

public class AppSettings
{
    public DatabaseSettings Database { get; set; }
    
    public RabbitMqSettings RabbitMq { get; set; }
}
