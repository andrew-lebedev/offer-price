namespace OfferPrice.Profile.Domain;

public interface IUserRepository
{
    Task<List<User>> Get(CancellationToken token);

    Task<User> GetById(string id, CancellationToken token);

    Task Insert(User user, CancellationToken token);

    Task Update(User user, CancellationToken token);

    Task Delete(string id, CancellationToken token);
}

