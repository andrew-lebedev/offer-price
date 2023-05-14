namespace OfferPrice.Events;

public interface IProducer
{
    void SendMessage<T>(T message) where T: Event;
}