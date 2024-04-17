using MediatR;
using OfferPrice.Notifications.Application.Exceptions;
using OfferPrice.Notifications.Domain.Interfaces;

namespace OfferPrice.Notifications.Application.NotificationSettingsOperations.SwitchSettings;

public class SwitchSettingsCommandHandler : IRequestHandler<SwitchSettingsCommand>  
{
    private readonly IUserRepository _userRepository;

    public SwitchSettingsCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(SwitchSettingsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException("User was not found.");
        }

        user.Settings.IsNotificationEnabled = request.SwitchSettings;

        await _userRepository.Update(user, cancellationToken);
    }
}
