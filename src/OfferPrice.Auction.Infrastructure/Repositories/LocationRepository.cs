using MongoDB.Driver;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly IMongoCollection<Location> _locations;

    public LocationRepository(IMongoDatabase database)
    {
        _locations = database.GetCollection<Location>("locations");
    }

    public Task Create(Location location, CancellationToken cancellationToken)
    {
        return _locations.InsertOneAsync(location, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Location>> GetAll(CancellationToken cancellationToken)
    {
        return await _locations.Find(Builders<Location>.Filter.Empty).ToListAsync(cancellationToken);
    }

    public Task<Location> GetByCity(string city, CancellationToken cancellationToken)
    {
        return _locations.Find(Builders<Location>.Filter.Eq(x => x.City, city)).SingleOrDefaultAsync(cancellationToken);
    }
}
