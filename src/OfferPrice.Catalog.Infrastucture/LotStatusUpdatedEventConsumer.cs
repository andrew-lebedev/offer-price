using Microsoft.Extensions.Logging;
using OfferPrice.Catalog.Domain;
using OfferPrice.Events;
using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Catalog.Infrastructure;

public class LotStatusUpdatedEventConsumer : RabbitMqConsumer<LotStatusUpdatedEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<LotStatusUpdatedEventConsumer> _logger;

    public LotStatusUpdatedEventConsumer(
        IProductRepository productRepository,
        IQueueResolver queueResolver,
        RabbitMqSettings settings,
        ILogger<LotStatusUpdatedEventConsumer> logger
    ) : base(queueResolver, settings, logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    protected override async Task Execute(LotStatusUpdatedEvent message, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(message.ProductId, cancellationToken);
        if (product == null)
        {
            _logger.LogInformation("Product {lot_id} not found", message.ProductId);
            return;
        }

        product.Status = ProductStatus.GetFromLotStatus(message.Status);
        if (string.IsNullOrWhiteSpace(product.Status))
        {
            return;
        }
        await _productRepository.Update(product, cancellationToken);
        
        _logger.LogInformation("Product {product_id} was successfully updated with status {product_status}", product.Id, product.Status);
    }
}