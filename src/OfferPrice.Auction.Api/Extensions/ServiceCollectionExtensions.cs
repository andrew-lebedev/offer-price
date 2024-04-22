using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OfferPrice.Auction.Api.Jobs;
using OfferPrice.Auction.Api.Settings;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Infrastructure;
using OfferPrice.Auction.Infrastructure.Consumers;
using OfferPrice.Auction.Infrastructure.Repositories;
using OfferPrice.Events.Events;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Events.RabbitMq.Options;

namespace OfferPrice.Auction.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection services, AuctionSettings settings)
    {
        services.AddSingleton(settings);
        services.AddHostedService<StartAuctionJob>();
        services.AddHostedService<FinishAuctionJob>();
        services.AddHostedService<PlaningAuctionJob>();

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, DatabaseSettings settings)
    {
        services.AddSingleton(settings);

        ConventionRegistry.Register("IgnoreExtraElementsConvention", new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
        }, _ => true);

        services.AddSingleton(_ => new MongoClient(settings.ConnectionString).GetDatabase(settings.Name));

        services.AddSingleton<ILotRepository, LotRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<ILikeRepository, LikeRepository>();
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<ILocationRepository, LocationRepository>();

        return services;
    }

    public static IServiceCollection AddRMQ(this IServiceCollection services, RabbitMqSettings settings)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserCreatedConsumer>();
            x.AddConsumer<UserUpdatedConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.AddRMQHost(settings);

                cfg.AddRMQProducer<NotificationCreateEvent>(settings);

                cfg.AddRMQConsumer<UserCreatedConsumer, UserCreatedEvent>(context, settings);
                cfg.AddRMQConsumer<UserUpdatedConsumer, UserUpdatedEvent>(context, settings);
            });
        });

        return services;
    }
}