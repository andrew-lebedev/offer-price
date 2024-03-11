using MongoDB.Driver;
using OfferPrice.Auction.Domain.Interfaces;
using OfferPrice.Auction.Domain.Models;

namespace OfferPrice.Auction.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Lot> _lots;

    public ProductRepository(IMongoDatabase database)
    {
        _lots = database.GetCollection<Lot>("lots");
    }

    public Task CreateProductAndLot(Product product, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;//todo: add insertion of lot or product?
    }

    public async Task<Product> GetUserProductById(string id, string userId, CancellationToken cancellationToken)
    {
        var lot = await _lots
            .Find(
                Builders<Lot>.Filter.Eq(x => x.Product.Id, id) &
                Builders<Lot>.Filter.Eq(x => x.Product.User.Id, userId))
            .SingleOrDefaultAsync(cancellationToken);

        return lot.Product;
    }

    public async Task<ICollection<Product>> GetUserProducts(string userId, CancellationToken cancellationToken)
    {
        var lots = await _lots.Find(Builders<Lot>.Filter.Eq(x => x.Product.User.Id, userId)).ToListAsync(cancellationToken);

        return lots.Select(x => x.Product).ToList();
    }

    public Task UpdateProduct(string lotId, Product product, CancellationToken cancellationToken)
    {
        return _lots.UpdateOneAsync(
            Builders<Lot>.Filter.Eq(x => x.Id, lotId) & Builders<Lot>.Filter.Eq(x => x.Product.Id, product.Id),
            Builders<Lot>.Update
                .Set(x => x.Product.Name, product.Name)
                .Set(x => x.Product.Description, product.Description)
                .Set(x => x.Product.Brand, product.Brand)
                .Set(x => x.Product.Category, product.Category)
                .Set(x => x.Product.Images, product.Images),
            cancellationToken: cancellationToken
        );
    }
}
