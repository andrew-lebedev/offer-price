namespace OfferPrice.Events.Events;

public class LotStatusUpdatedEvent : Event
{
    public string LotId { get; init; }
    public string ProductId { get; init; }
    public string Status { get; init; }
}