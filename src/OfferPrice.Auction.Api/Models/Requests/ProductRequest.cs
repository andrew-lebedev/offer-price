using OfferPrice.Auction.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OfferPrice.Auction.Api.Models.Requests;

public class ProductRequest
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<string> Images { get; set; }

    [Required]
    public string Category { get; set; }

    public string Location { get; set; }

    public string Brand { get; set; }

    [Required]
    [Range(1, (double)decimal.MaxValue, ErrorMessage = "Invalid minimal price")]
    public decimal Price { get; set; }

    public ProductStatus State { get; set; }
}
