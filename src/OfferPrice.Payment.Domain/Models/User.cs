namespace OfferPrice.Payment.Domain.Models;

public class User
{
    public string Id { get; set; }

    public string Email { get; set; }

    public User(Events.Models.User user)
    {
        Id = user.Id;
        Email = user.Email;
    }
}

