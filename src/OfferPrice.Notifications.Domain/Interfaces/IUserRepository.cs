using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Notifications.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> Get(string userId, CancellationToken cancellationToken);

    Task Create(User user, CancellationToken cancellationToken);

    Task Update(User user, CancellationToken cancellationToken);
}
