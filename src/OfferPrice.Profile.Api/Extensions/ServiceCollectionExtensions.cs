using MongoDB.Driver;
using OfferPrice.Profile.Infrastructure;
using OfferPrice.Events.RabbitMq;
using OfferPrice.Profile.Api.Settings;
using OfferPrice.Events.RabbitMq.Options;
using OfferPrice.Profile.Domain.Interfaces;

namespace OfferPrice.Profile.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDatebase(this IServiceCollection services, DatabaseSettings settings)
    {
        services.AddSingleton(
            _ => new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    public static void RegisterRabbitMq(this IServiceCollection services, RabbitMqSettings settings)
    {
        services.AddRabbitMqProducer(settings);
    }
}

