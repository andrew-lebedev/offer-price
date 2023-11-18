
namespace OfferPrice.Events.Interfaces;

public interface IExchangeResolver
{
    string GetExchange<T>() where T : Event;
}

