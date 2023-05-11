using MongoDB.Driver;
using OfferPrice.Catalog.Domain;

namespace OfferPrice.Catalog.Infrastructure;
public class LikeRepository : ILikeRepository
{
    private readonly IMongoCollection<Like> _likes;

    public LikeRepository(IMongoDatabase database)
    {
        _likes = database.GetCollection<Like>("likes");
    }

    public async Task Create(Like like, CancellationToken token)
    {
        await _likes.InsertOneAsync(
            like,
            cancellationToken: token
            );
    }

    public async Task Delete(string id, CancellationToken token)
    {
        await _likes.DeleteOneAsync(
            Builders<Like>.Filter.Eq(x => x.Id, id),
            cancellationToken: token
            );
    }

    public Task<Like> GetByProductAndUserId(string productId, string userId, CancellationToken token)
    {
        return _likes.Find(
            Builders<Like>.Filter.Where(x => x.UserId == userId && x.ProductId == productId)
            ).SingleOrDefaultAsync(token);
    }

    public Task<long> GetCountById(string id, CancellationToken token)
    {
        return _likes.CountDocumentsAsync(
            Builders<Like>.Filter.Where(x => x.Id == id),
            cancellationToken: token
            );
    }
}

