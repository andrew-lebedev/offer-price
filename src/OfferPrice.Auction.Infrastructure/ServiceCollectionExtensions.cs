using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Infrastructure;

public static class ServiceCollectionExtensions
{
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

        return services;
    }
}