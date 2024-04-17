using AutoMapper;
using MediatR;
using OfferPrice.Notifications.Application.Exceptions;
using OfferPrice.Notifications.Application.Models;
using OfferPrice.Notifications.Domain.Interfaces;

namespace OfferPrice.Notifications.Application.NotificationSettingsOperations.GetSettings;

public class GetSettingsCommandHandler : IRequestHandler<GetSettingsCommand, NotificationSettings>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetSettingsCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<NotificationSettings> Handle(GetSettingsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new EntityNotFoundException("User was not found");
        }

        return _mapper.Map<NotificationSettings>(user.Settings);
    }
}
