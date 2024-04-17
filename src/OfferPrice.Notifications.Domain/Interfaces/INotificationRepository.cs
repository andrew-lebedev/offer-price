using OfferPrice.Common;
using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Notifications.Domain.Interfaces;

public interface INotificationRepository
{
    Task<PageResult<Notification>> Get(string userId, Paging paging, CancellationToken cancellationToken);

    Task Create(Notification notification, CancellationToken cancellationToken);

    Task Read(ICollection<Notification> notifications, CancellationToken cancellationToken);
}
