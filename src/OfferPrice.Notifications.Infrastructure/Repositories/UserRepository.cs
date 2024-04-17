using MongoDB.Driver;
using OfferPrice.Notifications.Domain.Interfaces;
using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Notifications.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }

    public Task Create(User user, CancellationToken cancellationToken)
    {
        var utc = DateTime.UtcNow;
        user.Version = 1;
        user.Created = utc;
        user.Updated = utc;

        return _users.InsertOneAsync(user, cancellationToken: cancellationToken);
    }

    public Task<User> Get(string userId, CancellationToken cancellationToken)
    {
        return _users.Find(Builders<User>.Filter.Eq(x => x.Id, userId))
                     .SingleOrDefaultAsync(cancellationToken);
    }

    public Task Update(User user, CancellationToken cancellationToken)
    {
        user.Updated = DateTime.UtcNow;

        return _users.UpdateOneAsync(
            Builders<User>.Filter.Where(x => x.Id == user.Id),
            Builders<User>.Update.Set(x => x.Email, user.Email)
                                 .Set(x => x.Settings, user.Settings),
            cancellationToken: cancellationToken);
    }
}
