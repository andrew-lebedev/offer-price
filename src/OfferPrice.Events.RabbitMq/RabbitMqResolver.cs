using System;

namespace OfferPrice.Events.RabbitMq;

public class RabbitMqResolver : IQueueResolver
{
    private readonly RabbitMqSettings _settings;

    public RabbitMqResolver(RabbitMqSettings settings)
    {
        _settings = settings;
    }
    
    public string Get<T>() where T : Event
    {
        if (_settings.Queues.TryGetValue(typeof(T).Name, out var queue))
        {
            return queue;
        }

        throw new ArgumentOutOfRangeException(nameof(queue), "There are not available queues");
    }
}