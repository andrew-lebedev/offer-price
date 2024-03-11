using OfferPrice.Auction.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace OfferPrice.Auction.Domain.Interfaces;

public interface IProductRepository
{
    Task<ICollection<Product>> GetUserProducts(string userId, CancellationToken cancellationToken);

    Task<Product> GetUserProductById(string id, string userId, CancellationToken cancellationToken);

    Task CreateProductAndLot(Product product, CancellationToken cancellationToken);

    Task UpdateProduct(string lotId, Product product, CancellationToken cancellationToken);
}

