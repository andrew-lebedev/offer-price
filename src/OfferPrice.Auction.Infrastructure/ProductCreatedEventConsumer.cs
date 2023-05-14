using Microsoft.Extensions.Logging;
using OfferPrice.Auction.Domain;
using OfferPrice.Events;
using OfferPrice.Events.RabbitMq;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Infrastructure;

public class ProductCreatedEventConsumer : RabbitMqConsumer<ProductCreatedEvent>
{
    private readonly ILotRepository _lotRepository;
    private readonly ILogger<ProductCreatedEventConsumer> _logger;

    public ProductCreatedEventConsumer(
        ILotRepository lotRepository,
        IQueueResolver queueResolver,
        RabbitMqSettings settings,
        ILogger<ProductCreatedEventConsumer> logger
    ) : base(queueResolver, settings, logger)
    {
        _lotRepository = lotRepository;
        _logger = logger;
    }

    protected override async Task Execute(ProductCreatedEvent message, CancellationToken cancellationToken)
    {
        var lot = await _lotRepository.GetByProductId(message.Product.Id, cancellationToken);
        if (lot != null)
        {
            _logger.LogInformation("Lot {lot_id} is already exists for product {product_id}", lot.Id, lot.Product.Id);
            return;
        }
        
        lot = new Lot
        {
            Product = new Domain.Product(message.Product)
        };
        await _lotRepository.Create(lot, cancellationToken);
        
        _logger.LogInformation("Lot {lot} was successfully created", lot);
    }
}