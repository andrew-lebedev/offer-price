using OfferPrice.Events.Interfaces;
using System;

namespace OfferPrice.Events.RabbitMq;

public class RabbitMqResolver : IQueueResolver, IExchangeResolver
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

    public string GetExchange<T>() where T : Event
    {
        if (_settings.Exchanges.TryGetValue(typeof(T).Name, out var exchange))
        {
            return exchange;
        }

        throw new ArgumentOutOfRangeException(nameof(exchange), "There are not available queues");
    }
}