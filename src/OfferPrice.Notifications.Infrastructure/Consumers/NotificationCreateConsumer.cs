using MassTransit;
using Microsoft.Extensions.Logging;
using OfferPrice.Events.Events;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Notifications.Infrastructure.Consumers;

public class NotificationCreateConsumer : IConsumer<NotificationCreateEvent>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailProviderService _emailProviderService;
    private readonly ILogger<NotificationCreateConsumer> _logger;

    public NotificationCreateConsumer(
        INotificationRepository notificationRepository,
        IUserRepository userRepository,
        IEmailProviderService emailProviderService,
        ILogger<NotificationCreateConsumer> logger)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
        _emailProviderService = emailProviderService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationCreateEvent> context)
    {
        var message = context.Message;

        var user = await _userRepository.Get(message.UserId, CancellationToken.None);

        if (user == null)
        {
            _logger.LogInformation("User {lot_id} is already exists", user.Id);
            return;
        }

        var notification = new Notification()
        {
            Id = Guid.NewGuid().ToString(),
            UserId = user.Id,
            Body = message.Body,
            Subject = message.Subject,
            IsRead = false,
            CreateDate = DateTime.UtcNow,
        };

        await _notificationRepository.Create(notification, CancellationToken.None);

        await _emailProviderService.SendEmail(user.Email, notification);
    }
}
