using Microsoft.Extensions.Logging;
using OfferPrice.Auction.Domain;
using OfferPrice.Events.Events;
using OfferPrice.Events.Interfaces;
using OfferPrice.Events.RabbitMq;

namespace OfferPrice.Auction.Infrastructure;

public class UserCreatedEventConsumer : RabbitMqConsumer<UserCreatedEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserCreatedEventConsumer> _logger;

    public UserCreatedEventConsumer(
        IUserRepository userRepository,
        IQueueResolver queueResolver,
        IExchangeResolver exchangeResolver,
        RabbitMqSettings settings,
        ILogger<UserCreatedEventConsumer> logger
    ) : base(queueResolver,exchangeResolver, settings, logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    protected override async Task Execute(UserCreatedEvent message, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(message.User.Id, cancellationToken);
        if (user != null)
        {
            _logger.LogInformation("User {lot_id} is already exists", user.Id);
            return;
        }

        user = new User(message.User);
        await _userRepository.Create(user, cancellationToken);
        
        _logger.LogInformation("User {user} was successfully created", user);
    }
}