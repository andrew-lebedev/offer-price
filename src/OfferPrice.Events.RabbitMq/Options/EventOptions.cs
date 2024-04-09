namespace OfferPrice.Events.RabbitMq;

public class EventOptions
{
    public string Exchange { get; set; }

    public string Key { get; set; }

    public string Queue { get; set; }
}

