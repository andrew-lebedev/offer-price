
namespace OfferPrice.Profile.Domain;
public class User
{
    public User()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public List<Role> Role { get; set; }
}

