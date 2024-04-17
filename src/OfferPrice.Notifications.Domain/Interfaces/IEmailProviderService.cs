using OfferPrice.Notifications.Domain.Models;

namespace OfferPrice.Notifications.Domain.Interfaces;

public interface IEmailProviderService
{
    Task SendEmail(string emailReciever, Notification notification);
}
