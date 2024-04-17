
using MediatR;
using OfferPrice.Common;
using OfferPrice.Notifications.Application.Models;

namespace OfferPrice.Notifications.Application.NotificationOperations.GetNotification;

public class GetNotificationsCommand : IRequest<PageResult<Notification>>
{
    public string UserId { get; set; }

    public Paging Paging { get; set; }
}
