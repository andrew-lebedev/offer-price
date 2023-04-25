namespace OfferPrice.Catalog.Api.Models;
public class ProductRequest
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public string User { get; set; }

    public decimal Price { get; set; }
}


