namespace OfferPrice.Profile.Domain;

public interface IUserRepository
{
    Task<List<User>> GetUsers(CancellationToken token);

    Task<User> GetUserById(string id, CancellationToken token);

    Task InsertUser(User user, CancellationToken token);

    Task UpdateUser(User user, CancellationToken token);

    Task DeleteUser(string id, CancellationToken token);
}

