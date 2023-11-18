using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OfferPrice.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfferPrice.Events.RabbitMq;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRabbitMqProducer(this IServiceCollection services, RabbitMqSettings settings)
    {
        services.TryAddSingleton(settings);
        services.TryAddSingleton<IQueueResolver, RabbitMqResolver>();
        services.TryAddSingleton<IExchangeResolver, RabbitMqResolver>();
        services.AddSingleton<IProducer, RabbitMqProducer>();

        return services;
    }

    public static IServiceCollection AddRabbitMqConsumer<TConsumer>(this IServiceCollection services,
        RabbitMqSettings settings)
        where TConsumer : class, IConsumer
    {
        services.TryAddSingleton(settings);
        services.TryAddSingleton<IQueueResolver, RabbitMqResolver>();
        services.TryAddSingleton<IExchangeResolver, RabbitMqResolver>();
        services.AddSingleton<IConsumer, TConsumer>();
        
        services.TryAddConsumerService();

        return services;
    }

    private static void TryAddConsumerService(this IServiceCollection services)
    {
        var isRegistered = services.FirstOrDefault(s => s.ImplementationType == typeof(ConsumerService));
        if (isRegistered == null)
        {
            services.AddHostedService<ConsumerService>();
        }
    }
}