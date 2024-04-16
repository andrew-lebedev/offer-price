using OfferPrice.Auction.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Domain.Interfaces;

public interface ICategoryRepository
{
    Task Create(Category category, CancellationToken cancellationToken);

    Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken);

    Task<Category> GetByName(string name, CancellationToken cancellationToken);
}
