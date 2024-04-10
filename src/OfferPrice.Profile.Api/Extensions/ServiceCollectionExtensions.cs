using MongoDB.Driver;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Profile.Api.Settings;
using OfferPrice.Events.RabbitMq.Options;
using OfferPrice.Profile.Domain.Interfaces;
using MassTransit;
using OfferPrice.Events.Events;
using OfferPrice.Profile.Infrastructure.Repositories;

namespace OfferPrice.Profile.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, DatabaseSettings settings)
    {
        services.AddSingleton(
            _ => new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    public static IServiceCollection AddRMQ(this IServiceCollection services, RabbitMqSettings settings)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.AddRMQHost(settings);

                cfg.AddRMQProducer<UserCreatedEvent>(settings);
                cfg.AddRMQProducer<UserUpdatedEvent>(settings);
            });
        });

        return services;
    }
}

