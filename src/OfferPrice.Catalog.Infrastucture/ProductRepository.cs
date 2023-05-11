using MongoDB.Driver;
using OfferPrice.Catalog.Domain;

namespace OfferPrice.Catalog.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoDatabase database)
    {
        _products = database.GetCollection<Product>("products");
    }

    public async Task<PageResult<Product>> Get
        (string name, string username, string category, int page, int perPage, CancellationToken token)
    {
        var filterByName =
            name != null ? Builders<Product>.Filter.Eq(x => x.Name, name) : Builders<Product>.Filter.Empty;

        var filterByUsername =
            username != null ? Builders<Product>.Filter.Eq(x => x.User, username) : Builders<Product>.Filter.Empty;

        var filterByCategory =
            category != null ? Builders<Product>.Filter.Eq(x => x.Category, category) : Builders<Product>.Filter.Empty;

        var filter = Builders<Product>.Filter.And(filterByName, filterByUsername, filterByCategory);

        var documentsTask = _products.CountDocumentsAsync(filter, cancellationToken: token);


        var productsTask = _products.Find(filterByName & filterByUsername & filterByCategory)
                       .Skip((page - 1) * perPage)
                       .Limit(perPage)
                       .ToListAsync(token);

        await Task.WhenAll(documentsTask, productsTask);

        var totalPages = (long)Math.Ceiling((double)(documentsTask.Result / perPage));

        return new PageResult<Product>(page, perPage, totalPages, productsTask.Result);
    }

    public Task<Product> GetById(string id, CancellationToken token)
    {
        return _products.Find(x => x.Id == id).FirstOrDefaultAsync(token);
    }

    public Task Insert(Product product, CancellationToken token)
    {
        return _products.InsertOneAsync(product, cancellationToken: token);
    }

    public Task Update(Product product, CancellationToken token)
    {
        return _products.UpdateOneAsync(
            Builders<Product>.Filter.Where(x => x.Id == product.Id),
            Builders<Product>.Update.Set(x => x.Name, product.Name)
                                    .Set(x => x.Category, product.Category)
                                    .Set(x => x.Description, product.Description)
                                    .Set(x => x.Image, product.Image)
                                    .Set(x => x.Brand, product.Brand)
                                    .Set(x => x.Price, product.Price)
                                    .Set(x => x.Status, product.Status),
            cancellationToken: token
            );
    }
}

