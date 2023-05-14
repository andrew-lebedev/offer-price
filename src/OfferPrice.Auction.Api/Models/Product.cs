namespace OfferPrice.Auction.Api.Models;

public class Product
{
    public Product(Domain.Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Image = product.Image;
        UserId = product.UserId;
        Brand = product.Brand;
    }
    
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string UserId { get; set; }
    public string Brand { get; set; }
}