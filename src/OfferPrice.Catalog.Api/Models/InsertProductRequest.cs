﻿using System.ComponentModel.DataAnnotations;

namespace OfferPrice.Catalog.Api.Models;
public class InsertProductRequest
{
    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public string Image { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public string User { get; set; }

    [Required]
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Invalid minimal price")]
    public decimal Price { get; set; }
}


