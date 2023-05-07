using System.ComponentModel.DataAnnotations;

namespace OfferPrice.Profile.Api.Models;
public class UserRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }

    public string Middlename { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Phone]
    public string Phone { get; set; }
}

