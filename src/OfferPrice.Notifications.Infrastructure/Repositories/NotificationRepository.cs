using MongoDB.Driver;
using OfferPrice.Common;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Notifications.Infrastructure.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notification> _notifications;

    public NotificationRepository(IMongoDatabase database)
    {
        _notifications = database.GetCollection<Notification>("notifications");
    }

    public Task Create(Notification notification, CancellationToken cancellationToken)
    {
        return _notifications.InsertOneAsync(notification, cancellationToken: cancellationToken);
    }

    public async Task<PageResult<Notification>> Get(string userId, Paging paging, CancellationToken cancellationToken)
    {
        var filter = Builders<Notification>.Filter.Where(x => x.UserId == userId);

        var totalTask = _notifications.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        var lotsTask = _notifications.Find(filter)
            .Sort(Builders<Notification>.Sort.Ascending(x => x.CreateDate))
            .Skip((paging.Page - 1) * paging.PerPage)
            .Limit(paging.PerPage)
            .ToListAsync(cancellationToken);

        await Task.WhenAll(totalTask, lotsTask);
        return new PageResult<Notification>(
            page: paging.Page,
            perPage: paging.PerPage,
            total: totalTask.Result,
            items: lotsTask.Result
            );
    }

    public Task Read(ICollection<Notification> notifications, CancellationToken cancellationToken)
    {
        return _notifications
            .UpdateManyAsync(
                Builders<Notification>.Filter.In(x => x.Id, notifications.Select(x => x.Id)),
                Builders<Notification>.Update.Set(x => x.IsRead, true),
                cancellationToken: cancellationToken);
    }
}
