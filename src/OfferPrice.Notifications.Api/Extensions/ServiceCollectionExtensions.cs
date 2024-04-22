using MassTransit;
using MongoDB.Driver;
using OfferPrice.Auction.Infrastructure.Consumers;
using OfferPrice.Events.Events;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Events.RabbitMq.Options;
using OfferPrice.Notifications.Api.Settings;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Infrastructure.Consumers;
using OfferPrice.Notifications.Infrastructure.Repositories;

namespace OfferPrice.Notifications.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, DatabaseSettings settings)
    {
        services.AddSingleton(settings);

        services.AddSingleton(
            _ => new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName));

        services.AddSingleton<INotificationRepository, NotificationRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();

        return services;
    }

    public static IServiceCollection AddRMQ(this IServiceCollection services, RabbitMqSettings settings)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.AddConsumer<UserUpdatedConsumer>();
            x.AddConsumer<NotificationCreateConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.AddRMQHost(settings);
                cfg.AddRMQConsumer<UserCreatedConsumer, UserCreatedEvent>(context, settings);
                cfg.AddRMQConsumer<UserUpdatedConsumer, UserUpdatedEvent>(context, settings);
                cfg.AddRMQConsumer<NotificationCreateConsumer, NotificationCreateEvent>(context, settings);
            });
        });

        return services;
    }
}
