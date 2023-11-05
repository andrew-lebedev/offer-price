namespace OfferPrice.Profile.Domain;

public interface IUserRepository
{

    Task<User> Get(string id, CancellationToken token);

    Task<User> GetByEmailAndPassword(string email, string password, CancellationToken token);

    Task Create(User user, CancellationToken token);

    Task Update(User user, CancellationToken token);
}

