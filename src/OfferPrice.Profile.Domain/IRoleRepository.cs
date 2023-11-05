
namespace OfferPrice.Profile.Domain;

public interface IRoleRepository
{
    Task<List<Role>> Get(CancellationToken token);

    Task<Role> GetByName(string roleName, CancellationToken token);
}


