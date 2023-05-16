using Microsoft.Extensions.DependencyInjection;

namespace OfferPrice.Auction.Api.Jobs;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJobs(this IServiceCollection services, AuctionSettings settings)
    {
        services.AddSingleton(settings);
        services.AddHostedService<StartAuctionJob>();
        services.AddHostedService<FinishAuctionJob>();

        return services;
    }
}