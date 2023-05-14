using MongoDB.Driver;
using OfferPrice.Auction.Domain;
using OfferPrice.Common;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Infrastructure;

public class LotRepository : ILotRepository
{
    private readonly IMongoCollection<Lot> _lots;

    public LotRepository(IMongoDatabase database)
    {
        _lots = database.GetCollection<Lot>("lots");
        _lots.Indexes.CreateOne(new CreateIndexModel<Lot>(
            Builders<Lot>.IndexKeys.Ascending(l => l.Product.Id),
            new CreateIndexOptions { Unique = true }
        ));
    }

    public Task Create(Lot lot, CancellationToken token)
    {
        return _lots.InsertOneAsync(lot, cancellationToken: token);
    }

    public Task Delete(string id, CancellationToken token)
    {
        return _lots.DeleteOneAsync(Builders<Lot>.Filter.Eq(x => x.Id, id), cancellationToken: token);
    }

    public Task<Lot> Get(string id, CancellationToken token)
    {
        return _lots.Find(Builders<Lot>.Filter.Eq(x => x.Id, id)).SingleOrDefaultAsync(token);
    }

    public Task<Lot> GetByProductId(string productId, CancellationToken token)
    {
        return _lots.Find(Builders<Lot>.Filter.Eq(x => x.Product.Id, productId)).FirstOrDefaultAsync(token);
    }

    public async Task<PageResult<Lot>> Find(FindLotsQuery query, CancellationToken token)
    {
        var ownerFilter = string.IsNullOrWhiteSpace(query.ProductOwnerId)
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.Eq(l => l.Product.UserId, query.ProductOwnerId);
        var statusesFilter = query.Statuses.Length == 0
            ? Builders<Lot>.Filter.Empty
            : Builders<Lot>.Filter.In(l => l.Status, query.Statuses);
        var filter = Builders<Lot>.Filter.And(ownerFilter, statusesFilter);

        var totalTask = _lots.CountDocumentsAsync(filter, cancellationToken: token);
        var lotsTask = _lots.Find(filter)
            .Skip((query.Paging.Page - 1) * query.Paging.PerPage)
            .Limit(query.Paging.PerPage)
            .ToListAsync(token);

        await Task.WhenAll(totalTask, lotsTask);
        return new PageResult<Lot>(
            page: query.Paging.Page,
            perPage: query.Paging.PerPage,
            total: totalTask.Result,
            items: lotsTask.Result
        );
    }

    public async Task Update(Lot lot, CancellationToken token)
    {
        await _lots.UpdateOneAsync(
            Builders<Lot>.Filter.Eq(x => x.Id, lot.Id),
            Builders<Lot>.Update
                .Set(x => x.Product, lot.Product)
                .Set(x => x.Winner, lot.Winner)
                .Set(x => x.BetHistory, lot.BetHistory)
                .Set(x => x.Start, lot.Start)
                .Set(x => x.End, lot.End)
                .Set(x => x.Status, lot.Status),
            cancellationToken: token
        );
    }
}