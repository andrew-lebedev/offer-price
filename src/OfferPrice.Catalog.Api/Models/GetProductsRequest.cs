using System.ComponentModel.DataAnnotations;

namespace OfferPrice.Catalog.Api.Models;
public class GetProductsRequest
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Page { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int PerPage { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }

    public string Category { get; set; }
}

