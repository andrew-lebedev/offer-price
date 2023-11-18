using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Payment.Api.Settings;

public class AppSettings
{
    public DatabaseSettings Database { get; set; }
    public RabbitMqSettings RabbitMq { get; set; }
}

