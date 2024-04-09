using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using OfferPrice.Auction.Api.Hubs;
using OfferPrice.Auction.Api.Models.Responses;
using OfferPrice.Auction.Api.Settings;
using OfferPrice.Auction.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Api.Jobs;

public class StartAuctionJob : BackgroundService
{
    private readonly ILotRepository _lotRepository;
    private readonly IHubContext<AuctionHub> _hubContext;
    private readonly AuctionSettings _settings;

    public StartAuctionJob(ILotRepository lotRepository, IHubContext<AuctionHub> hubContext,AuctionSettings settings)
    {
        _lotRepository = lotRepository;
        _hubContext = hubContext;
        _settings = settings;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Handle(stoppingToken);
            await Task.Delay(_settings.PauseInMs, stoppingToken);
        }
    }

    private async Task Handle(CancellationToken cancellationToken)
    {
        var lots = await _lotRepository.GetNonStarted(DateTime.UtcNow, cancellationToken); // todo: make batches

        foreach (var lot in lots)
        {
            lot.Begin();

            try
            {
                // todo: add user or new method
                await _lotRepository.Update(lot, string.Empty, cancellationToken); // todo: add concurrency
                await _hubContext.Clients.Group(lot.Id).SendAsync(
                    nameof(IAuctionClient.OnAuctionStarted),
                    new AuctionStartedResponse(lot.Price, lot.Updated.AddSeconds(_settings.BetIntervalInSec)),
                    cancellationToken
                );
                
                //_producer.SendMessage(new LotStatusUpdatedEvent
                //{
                //    LotId = lot.Id,
                //    ProductId = lot.Product.Id,
                //    Status = lot.Status.ToString()
                //});
            }
            catch
            {
                // todo: add concurrency
            }
        }
    }
}