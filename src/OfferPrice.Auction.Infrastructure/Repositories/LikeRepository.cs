using MongoDB.Driver;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;
using OfferPrice.Common;

namespace OfferPrice.Auction.Infrastructure.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly IMongoCollection<Like> _likes;

    public LikeRepository(IMongoDatabase database)
    {
        _likes = database.GetCollection<Like>("likes");
        _likes.Indexes.CreateOne(new CreateIndexModel<Like>(
            Builders<Like>.IndexKeys.Ascending(l => l.LotId).Ascending(l => l.UserId)
        ));
    }

    public Task Create(Like like, CancellationToken token)
    {
        return _likes.InsertOneAsync(
            like,
            cancellationToken: token
        );
    }

    public Task Delete(string lotId, string userId, CancellationToken token)
    {

        var filter = Builders<Like>.Filter.Eq(x => x.UserId, userId)
                     & Builders<Like>.Filter.Eq(x => x.LotId, lotId);
        return _likes.DeleteOneAsync(filter, cancellationToken: token);
    }

    public Task<Like> Get(string lotId, string userId, CancellationToken token)
    {
        return _likes.Find(
            Builders<Like>.Filter.Where(x => x.UserId == userId && x.LotId == lotId)
        ).FirstOrDefaultAsync(token);
    }

    public async Task<PageResult<string>> Get(string userId, Paging paging, CancellationToken cancellationToken)
    {
        var userFilter = Builders<Like>.Filter.Eq(x => x.UserId, userId);
        var sort = Builders<Like>.Sort.Descending(x => x.CreateDate);

        var totalTask = _likes.CountDocumentsAsync(userFilter, cancellationToken: cancellationToken);
        var likeTasks = _likes
            .Find(userFilter)
            .Sort(sort)
            .Skip((paging.Page - 1) * paging.PerPage)
            .Limit(paging.PerPage)
            .ToListAsync(cancellationToken);

        await Task.WhenAll(totalTask, likeTasks);

        return new PageResult<string>(
            paging.Page,
            paging.PerPage,
            totalTask.Result,
            likeTasks.Result.Select(x => x.LotId).ToList());
    }

    public Task<long> GetCount(string lotId, CancellationToken token)
    {
        return _likes.CountDocumentsAsync(
            Builders<Like>.Filter.Where(x => x.LotId == lotId),
            cancellationToken: token
        );
    }
}
