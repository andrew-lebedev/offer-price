using MongoDB.Driver;
using OfferPrice.Catalog.Api.Models;

namespace OfferPrice.Catalog.Api.DataService;
public class DatabaseService : IDatabaseService
{
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Product> _products;

    public DatabaseService(IMongoDatabase database)
    {
        _database = database;

        _products = _database.GetCollection<Product>("Products");
    }

    public async Task<List<Product>> GetProducts(string name, string username, string category)
    {
        var filterByName =
            name != null ? Builders<Product>.Filter.Eq(x => x.Name, name) : Builders<Product>.Filter.Empty;

        var filterByUsername =
            username != null ? Builders<Product>.Filter.Eq(x => x.User, username) : Builders<Product>.Filter.Empty;

        var filterByCategory =
            category != null ? Builders<Product>.Filter.Eq(x => x.Category, category) : Builders<Product>.Filter.Empty;

        var products = await _products.Find(filterByName & filterByUsername & filterByCategory).ToListAsync();

        return products;
    }

    public async Task<Product> GetProductById(string id)
    {
        var product = await _products.Find(x => x.Id == id).FirstOrDefaultAsync();

        return product;
    }

    public async Task HideProduct(string id)
    {
        await _products.UpdateOneAsync(
            Builders<Product>.Filter.Where(x => x.Id == id),
            Builders<Product>.Update.Set(x => x.Status, "hidden")
            );
    }

    public async Task InsertProduct(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public async Task ShowProduct(string id)
    {
        await _products.UpdateOneAsync(
            Builders<Product>.Filter.Where(x => x.Id == id),
            Builders<Product>.Update.Set(x => x.Status, "observable")
            );
    }

    public async Task UpdateProduct(string id, Product product)
    {
        await _products.UpdateOneAsync(
            Builders<Product>.Filter.Where(x => x.Id == id),
            Builders<Product>.Update.Set(x => x.Name, product.Name)
                                    .Set(x => x.Category, product.Category)
                                    .Set(x => x.Description, product.Description)
                                    .Set(x => x.User, product.User)
                                    .Set(x => x.Price, product.Price)
            );

    }
}

