using MongoDB.Driver;

namespace OfferPrice.Catalog.Domain;
public interface IProductRepository
{
    Task<List<Product>> GetProducts(string name, string username, string category);

    Task<Product> GetProductById(string id);

    Task InsertProduct(Product product);

    Task<UpdateResult> UpdateProduct(string id, Product product);
}

