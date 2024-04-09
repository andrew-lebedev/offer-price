using MassTransit;
using OfferPrice.Events.RabbitMq.Helpers;
using OfferPrice.Events.RabbitMq.Options;
using RabbitMQ.Client;

namespace OfferPrice.Events.RabbitMq;

public static class ServiceCollectionExtensions
{
    public static void AddRMQHost(this IRabbitMqBusFactoryConfigurator cfg, RabbitMqSettings settings)
    {
        cfg.Host(settings.Host, "/", s =>
        {
            s.Username(settings.Username);
            s.Password(settings.Password);
        });
    }

    public static void AddRMQProducer<TEvent>(this IRabbitMqBusFactoryConfigurator cfg, RabbitMqSettings settings)
        where TEvent : Event
    {
        var resolve = RabbitMqEventResolver.Resolve<TEvent>(settings);

        cfg.Message<TEvent>(e => e.SetEntityName(resolve.Exchange));
        cfg.Publish<TEvent>(e => e.ExchangeType = ExchangeType.Direct);
        cfg.Send<TEvent>(e =>
        {
            e.UseRoutingKeyFormatter(formatter => resolve.Key);
        });
    }

    public static void AddRMQConsumer<TConsumer, TEvent>
        (this IReceiveConfigurator<IRabbitMqReceiveEndpointConfigurator> cfg,
        IBusRegistrationContext context,
        RabbitMqSettings settings)
        where TConsumer : IConsumer
        where TEvent : Event
    {
        var resolve = RabbitMqEventResolver.Resolve<TEvent>(settings);

        cfg.ReceiveEndpoint(resolve.Queue, e =>
        {
            e.ConfigureConsumeTopology = false;

            e.ConfigureConsumer(context, typeof(TConsumer));

            e.Bind(resolve.Exchange, ex =>
            {
                ex.RoutingKey = resolve.Key;
                ex.ExchangeType = ExchangeType.Direct;
            });
        });
    }
}