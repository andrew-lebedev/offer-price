using MongoDB.Driver;
using OfferPrice.Auction.Domain;

namespace OfferPrice.Auction.Infrastructure;
public class AuctionRepository : IAuctionRepository
{
    private readonly IMongoCollection<Domain.Auction> _auctions;

    public AuctionRepository(IMongoDatabase database)
    {
        _auctions = database.GetCollection<Domain.Auction>("auctions");
    }

    public async Task Create(Domain.Auction auction, CancellationToken token)
    {
        await _auctions.InsertOneAsync(
            auction,
            cancellationToken: token
            );
    }

    public async Task Delete(string id, CancellationToken token)
    {
        await _auctions.DeleteOneAsync(
            Builders<Domain.Auction>.Filter.Eq(x => x.Id, id),
            cancellationToken: token
            );
    }

    public Task<Domain.Auction> GetById(string id, CancellationToken token)
    {
        return _auctions.Find(Builders<Domain.Auction>.Filter.Eq(x => x.Id, id))
                        .SingleOrDefaultAsync(token);
    }

    public Task<List<Domain.Auction>> Get(CancellationToken token)
    {
        return _auctions.Find(Builders<Domain.Auction>.Filter.Empty)
                        .ToListAsync(token);
    }

    public async Task Update(Domain.Auction auction, CancellationToken token)
    {
        await _auctions.UpdateOneAsync(
            Builders<Domain.Auction>.Filter.Eq(x => x.Id, auction.Id),
            Builders<Domain.Auction>.Update.Set(x => x.Product, auction.Product)
                                           .Set(x => x.Winner, auction.Winner)
                                           .Set(x => x.BetHistory, auction.BetHistory)
                                           .Set(x => x.End, auction.End)
                                           .Set(x => x.Status, auction.Status),
            cancellationToken: token
            );
    }
}
