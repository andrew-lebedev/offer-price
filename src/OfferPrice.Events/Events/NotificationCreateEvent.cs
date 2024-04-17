namespace OfferPrice.Events.Events;

public class NotificationCreateEvent : Event
{
    public string UserId { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }
}
