using OfferPrice.Auction.Domain.Enums;

namespace OfferPrice.Auction.Application.Models;

public class Product
{
    public Product()
    {
        Id = Guid.NewGuid().ToString();
    }

    public Product(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Images = product.Images;
        User = User.FromDomain(product.User);
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