namespace OfferPrice.Auction.Api.Models;

public class Product
{
    public Product(Domain.Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Image = product.Image;
        User = product.User;
        Brand = product.Brand;
        Price = product.Price;
    }
    
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string User { get; set; }
    public string Brand { get; set; }
    public decimal Price { get; set; }
}