namespace OfferPrice.Notifications.Application.Models;

public class Notification
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }

    public string Details { get; set; }

    public bool IsRead { get; set; }
}
