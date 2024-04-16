using OfferPrice.Auction.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Domain.Interfaces;

public interface ILocationRepository
{
    Task Create(Location location, CancellationToken cancellationToken);

    Task<IEnumerable<Location>> GetAll(CancellationToken cancellationToken);

    Task<Location> GetByCity(string city, CancellationToken cancellationToken);
}
