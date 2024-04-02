using OfferPrice.Auction.Domain.Enums;
using System.Collections.Generic;

namespace OfferPrice.Auction.Domain.Models;
public class Product
{
    public Product()
    {
    }

    public Product(Events.Models.Product product, User user)
    {
        Id = product.Id;
        Name = product.Name;
        User = user;
        Brand = product.Brand;
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<string> Images { get; set; }

    public User User { get; set; }

    public string Brand { get; set; }

    public string Category { get; set; }

    public string Location { get; set; }

    public decimal Price { get; set; }

    public ProductStatus State { get; set; }
}

