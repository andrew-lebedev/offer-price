using MediatR;
using OfferPrice.Notifications.Application.Models;

namespace OfferPrice.Notifications.Application.NotificationSettingsOperations.GetSettings;

public class GetSettingsCommand : IRequest<NotificationSettings>
{
    public string UserId { get; set; }
}
