using OfferPrice.Common;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Domain;
public interface ILotRepository
{
    Task<PageResult<Lot>> Find(FindLotsQuery query, CancellationToken token);

    Task<Lot> Get(string id, CancellationToken token);
    Task<Lot> GetByProductId(string productId, CancellationToken token);

    Task Create(Lot lot, CancellationToken token);

    Task Update(Lot lot, CancellationToken token);

    Task Delete(string id, CancellationToken token);
}

