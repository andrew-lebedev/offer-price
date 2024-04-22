using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using OfferPrice.Auction.Api.Settings;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Events.Events;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Jobs;

public class PlaningAuctionJob : BackgroundService
{
    private readonly ILotRepository _lotRepository;
    private readonly IBus _bus;
    private readonly AuctionSettings _settings;
    private readonly ILogger<PlaningAuctionJob> _logger;

    public PlaningAuctionJob(
        ILotRepository lotRepository,
        IBus bus,
        AuctionSettings settings,
        ILogger<PlaningAuctionJob> logger)
    {
        _lotRepository = lotRepository;
        _bus = bus;
        _settings = settings;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Starting planning lots");
            await Handle(stoppingToken);
            await Task.Delay(_settings.PauseInMs, stoppingToken);
        }
    }

    private async Task Handle(CancellationToken cancellationToken)
    {
        var lots = await _lotRepository.GetNonPlanned(cancellationToken);

        foreach (var lot in lots)
        {
            var plannedLots = await _lotRepository.GetSameLots(lot, cancellationToken);

            if (plannedLots.IsNullOrEmpty())
            {
                lot.Schedule(DateTime.Now.AddHours(1));
            }
            else
            {
                var lastTimePlannedLot = plannedLots.First();

                lot.Schedule(lastTimePlannedLot.Start.Value.AddMinutes(10));
            }

            await _lotRepository.Update(lot, cancellationToken);

            _logger.LogInformation($"Lot with id {lot.Id} has been planned.");

            await _bus.Publish<NotificationCreateEvent>(new()
            {
                UserId = lot.Product.User.Id,
                Subject = "Auction",
                Body = $"Your auction has been planned on {lot.Start}",
            });
        }
    }
}
