namespace OfferPrice.Catalog.Domain;
public interface IProductRepository
{
    Task<PageResult<Product>> GetProducts(string name, string username, string category, int page, int perPage, CancellationToken token);

    Task<Product> GetProductById(string id, CancellationToken token);

    Task InsertProduct(Product product, CancellationToken token);

    Task UpdateProduct(Product product, CancellationToken token);
}

