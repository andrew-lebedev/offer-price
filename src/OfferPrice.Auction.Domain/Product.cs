namespace OfferPrice.Auction.Domain;
public class Product
{
    public Product()
    {
    }

    public Product(Events.Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Image = product.Image;
        UserId = product.User;
        Brand = product.Brand;
    }
    
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string UserId { get; set; }
    public string Brand { get; set; }
}

