using MediatR;
using OfferPrice.Notifications.Application.Models;

namespace OfferPrice.Notifications.Application.NotificationOperations.ReadNotification;

public class ReadNotificationCommand : IRequest
{
    public ICollection<Notification> Notifications { get; set; }
}
