using OfferPrice.Catalog.Api.Models;

namespace OfferPrice.Catalog.Api.DataService
{
    public interface IDatabaseService
    {
        Task<List<Product>> GetProducts(string name,string username,string category);

        Task<Product> GetProductById(string id);

        Task InsertProduct(Product product);

        Task UpdateProduct(string id, Product product);

        Task HideProduct(string id);

        Task ShowProduct(string id);
    }
}
