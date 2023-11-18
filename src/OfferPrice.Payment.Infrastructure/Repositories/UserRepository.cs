
using MongoDB.Driver;
using OfferPrice.Payment.Domain.Interfaces;
using OfferPrice.Payment.Domain.Models;

namespace OfferPrice.Payment.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }

    public Task Create(User user, CancellationToken token)
    {
        return _users.InsertOneAsync(user, cancellationToken: token);
    }

    public Task<User> GetById(string id, CancellationToken token)
    {
        return _users
            .Find(Builders<User>.Filter.Eq(x => x.Id, id))
            .SingleOrDefaultAsync(token);
    }
}

