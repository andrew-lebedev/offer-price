using MassTransit;
using Microsoft.Extensions.Logging;
using OfferPrice.Events.Events;
using OfferPrice.Payment.Domain.Interfaces;
using OfferPrice.Payment.Domain.Models;

namespace OfferPrice.Auction.Infrastructure.Consumers;

public class UserUpdatedConsumer : IConsumer<UserUpdatedEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserUpdatedConsumer> _logger;

    public UserUpdatedConsumer(
        IUserRepository userRepository,
        ILogger<UserUpdatedConsumer> logger
        )
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        var message = context.Message;

        var user = await _userRepository.GetById(message.User.Id, CancellationToken.None);
        if (user is null)
        {
            _logger.LogInformation("User isn't exists");
            return;
        }

        user = new User(message.User);
        await _userRepository.Update(user, CancellationToken.None);

        _logger.LogInformation("User {user} was successfully updated", user);
    }
}

