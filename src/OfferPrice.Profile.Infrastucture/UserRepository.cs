
using MongoDB.Driver;
using OfferPrice.Profile.Domain;

namespace OfferPrice.Profile.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("Users");
    }

    public async Task DeleteUser(string id, CancellationToken token)
    {
        await _users.DeleteOneAsync(Builders<User>.Filter.Eq(x => x.Id, id), token);
    }

    public Task<List<User>> GetUsers(CancellationToken token)
    {
        return _users.Find(Builders<User>.Filter.Empty)
                     .ToListAsync(token);
    }

    public Task<User> GetUserById(string id, CancellationToken token)
    {
        return _users.Find(Builders<User>.Filter.Eq(x => x.Id, id))
                     .SingleOrDefaultAsync(token);
    }

    public async Task InsertUser(User user, CancellationToken token)
    {
        await _users.InsertOneAsync(user, new InsertOneOptions(), token);
    }

    public async Task UpdateUser(User user, CancellationToken token)
    {
        await _users.UpdateOneAsync(
            Builders<User>.Filter.Where(x => x.Id == user.Id),
            Builders<User>.Update.Set(x => x.Name, user.Name)
                                 .Set(x => x.Surname, user.Surname)
                                 .Set(x => x.Middlename, user.Middlename)
                                 .Set(x => x.Email, user.Email)
                                 .Set(x => x.Phone, user.Phone),
            new UpdateOptions(),
            token);
    }
}

