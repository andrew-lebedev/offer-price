namespace OfferPrice.Notifications.Domain.Models;

public class User
{
    public User()
    {
    }

    public User(Events.Models.User user)
    {
        Id = user.Id;
        Email = user.Email;
        Settings = new NotificationsSettings() { IsNotificationEnabled = true };
    }
    public string Id { get; set; }

    public string Email { get; set; }

    public NotificationsSettings Settings { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public int Version { get; set; }
}
