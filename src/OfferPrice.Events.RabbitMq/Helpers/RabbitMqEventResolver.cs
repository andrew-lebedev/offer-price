using OfferPrice.Events.RabbitMq.Options;
using System;

namespace OfferPrice.Events.RabbitMq.Helpers;

public static class RabbitMqEventResolver
{
    public static EventOptions Resolve<T>(RabbitMqSettings settings) where T : Event
    {
        if (settings.Events.TryGetValue(typeof(T).Name, out var eventOptions))
        {
            return eventOptions;
        }

        throw new ArgumentOutOfRangeException(nameof(eventOptions), "There are not available queues");
    }
}

