using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Profile.Api
{
    public class AppSettings
    {
        public DatabaseSettings Database { get; set; }
        public RabbitMqSettings RabbitMq { get; set; }
    }
}
