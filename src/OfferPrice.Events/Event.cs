namespace OfferPrice.Events;

public abstract class Event
{
    public Event()
    {
        Id = Guid.NewGuid().ToString();
        Timestamp = DateTime.UtcNow;
    }
    
    public string Id { get; init; }
    public DateTime Timestamp { get; init; }
}