namespace OfferPrice.Catalog.Domain;
public interface ILikeRepository
{
    Task<Like> GetByProductAndUserId(string productId, string userId, CancellationToken token);

    Task<long> GetCountById(string id, CancellationToken token);

    Task Create(Like like, CancellationToken token);

    Task Delete(string id, CancellationToken token);
}

