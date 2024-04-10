using System.Collections.Generic;

namespace OfferPrice.Events.RabbitMq.Options;

public class RabbitMqSettings
{
    public string Host { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public IReadOnlyDictionary<string, EventOptions> Events { get; set; }
}