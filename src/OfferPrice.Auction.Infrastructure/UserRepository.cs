using MongoDB.Driver;
using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Infrastructure;

public class UserRepository : IUserRepository
{    
    private readonly IMongoCollection<User> _users;

    public UserRepository(IMongoDatabase database)
    {
        _users = database.GetCollection<User>("users");
    }
    
    public Task<User> Get(string id, CancellationToken token)
    {
        return _users.Find(Builders<User>.Filter.Eq(x => x.Id, id)).SingleOrDefaultAsync(token);
    }

    public Task Create(User user, CancellationToken token)
    {
        var utc = DateTime.UtcNow;
        user.Version = 1;
        user.Created = utc;
        user.Updated = utc;
        
        return _users.InsertOneAsync(user, cancellationToken: token);
    }
}