namespace OfferPrice.Events.Interfaces;

public interface IQueueResolver
{
    string Get<T>() where T : Event;
}