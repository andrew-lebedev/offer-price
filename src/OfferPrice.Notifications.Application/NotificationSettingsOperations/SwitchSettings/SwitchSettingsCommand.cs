using MediatR;

namespace OfferPrice.Notifications.Application.NotificationSettingsOperations.SwitchSettings;

public class SwitchSettingsCommand : IRequest
{
    public string UserId { get; set; }

    public bool SwitchSettings { get; set; }
}
