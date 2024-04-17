using MassTransit;
using Microsoft.Extensions.Logging;
using OfferPrice.Events.Events;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Auction.Infrastructure.Consumers;

public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserCreatedConsumer> _logger;

    public UserCreatedConsumer(
        IUserRepository userRepository,
        ILogger<UserCreatedConsumer> logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserCreatedEvent> context)
    {
        var message = context.Message;

        var user = await _userRepository.Get(message.User.Id, CancellationToken.None);
        if (user != null)
        {
            _logger.LogInformation("User {lot_id} is already exists", user.Id);
            return;
        }

        user = new User(message.User);

        await _userRepository.Create(user, CancellationToken.None);

        _logger.LogInformation("User {user} was successfully created", user);
    }
}
