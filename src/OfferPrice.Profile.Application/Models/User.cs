namespace OfferPrice.Profile.Application.Models;

public class User
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public IEnumerable<string> Roles { get; set; }

    public Events.Models.User ToEvent()
    {
        return new()
        {
            Id = Id,
            Name = Name,
            LastName = LastName,
            MiddleName = MiddleName,
            Email = Email,
            Phone = Phone
        };
    }
}
