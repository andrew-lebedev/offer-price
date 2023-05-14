namespace OfferPrice.Events;

public interface IQueueResolver
{
    string Get<T>() where T : Event;
}