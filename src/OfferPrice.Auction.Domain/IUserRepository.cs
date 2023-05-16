using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Domain;

public interface IUserRepository
{
    Task<User> Get(string id, CancellationToken token);
    Task Create(User user, CancellationToken token);
}