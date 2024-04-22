namespace OfferPrice.Notifications.Application.Models;

public class Notification
{
    public string Id { get; set; }

    public string UserId { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreateDate { get; set; }
}
