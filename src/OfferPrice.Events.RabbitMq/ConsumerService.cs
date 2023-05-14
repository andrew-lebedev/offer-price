using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Events.RabbitMq;

public class ConsumerService : BackgroundService
{
    public ConsumerService(IEnumerable<IConsumer> _)
    {
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}