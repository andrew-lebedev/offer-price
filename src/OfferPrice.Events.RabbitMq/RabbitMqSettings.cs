using System.Collections.Generic;

namespace OfferPrice.Events.RabbitMq;

public class RabbitMqSettings
{
    public string Host { get; set; }
    public IReadOnlyDictionary<string, string> Queues { get; set; }
}