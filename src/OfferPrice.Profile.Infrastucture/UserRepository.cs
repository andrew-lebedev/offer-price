
using MongoDB.Driver;
using OfferPrice.Profile.Domain;

namespace OfferPrice.Profile.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }

    public async Task Delete(string id, CancellationToken token)
    {
        await _users.DeleteOneAsync(Builders<User>.Filter.Eq(x => x.Id, id), token);
    }

    public Task<List<User>> Get(CancellationToken token)
    {
        return _users.Find(Builders<User>.Filter.Empty)
                     .ToListAsync(token);
    }

    public Task<User> GetById(string id, CancellationToken token)
    {
        return _users.Find(Builders<User>.Filter.Eq(x => x.Id, id))
                     .SingleOrDefaultAsync(token);
    }

    public async Task Insert(User user, CancellationToken token)
    {
        await _users.InsertOneAsync(user, cancellationToken: token);
    }

    public async Task Update(User user, CancellationToken token)
    {
        await _users.UpdateOneAsync(
            Builders<User>.Filter.Where(x => x.Id == user.Id),
            Builders<User>.Update.Set(x => x.Name, user.Name)
                                 .Set(x => x.Surname, user.Surname)
                                 .Set(x => x.Middlename, user.Middlename)
                                 .Set(x => x.Email, user.Email)
                                 .Set(x => x.Phone, user.Phone),
            cancellationToken: token);
    }
}

