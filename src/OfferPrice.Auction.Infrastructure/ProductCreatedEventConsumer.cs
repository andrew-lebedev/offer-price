using Microsoft.Extensions.Logging;
using OfferPrice.Auction.Domain;
using OfferPrice.Events.Events;
using OfferPrice.Events.Interfaces;
using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Auction.Infrastructure;

public class ProductCreatedEventConsumer : RabbitMqConsumer<ProductCreatedEvent>
{
    private readonly ILotRepository _lotRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ProductCreatedEventConsumer> _logger;

    public ProductCreatedEventConsumer(
        ILotRepository lotRepository,
        IUserRepository userRepository,
        IQueueResolver queueResolver,
        IExchangeResolver exchangeResolver,
        RabbitMqSettings settings,
        ILogger<ProductCreatedEventConsumer> logger
    ) : base(queueResolver, exchangeResolver, settings, logger)
    {
        _lotRepository = lotRepository;
        _userRepository = userRepository;
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

        var user = await _userRepository.Get(message.Product.User, cancellationToken);
        if (user == null)
        {
            _logger.LogError("User {user_id} not found", message.Product.User);
            throw new Exception($"User {message.Product.User} not found");
        }

        lot = new Lot
        {
            Product = new Domain.Product(message.Product, user),
            Price = message.Product.Price
        };
        await _lotRepository.Create(lot, cancellationToken);

        _logger.LogInformation("Lot {lot} was successfully created", lot);
    }
}