using MongoDB.Driver;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IMongoCollection<Category> _categories;

    public CategoryRepository(IMongoDatabase database)
    {
        _categories = database.GetCollection<Category>("categories");
    }

    public Task Create(Category category, CancellationToken cancellationToken)
    {
        return _categories.InsertOneAsync(category, cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken)
    {
        return await _categories.Find(Builders<Category>.Filter.Empty).ToListAsync(cancellationToken);
    }

    public Task<Category> GetByName(string name, CancellationToken cancellationToken)
    {
        return _categories.Find(Builders<Category>.Filter.Eq(x => x.Name, name)).SingleOrDefaultAsync(cancellationToken);
    }
}
