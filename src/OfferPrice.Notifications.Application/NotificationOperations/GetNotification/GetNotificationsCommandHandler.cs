using AutoMapper;
using MediatR;
using OfferPrice.Common;
using OfferPrice.Notifications.Application.Models;
using OfferPrice.Notifications.Domain.Interfaces;

namespace OfferPrice.Notifications.Application.NotificationOperations.GetNotification;

public class GetNotificationsCommandHandler : IRequestHandler<GetNotificationsCommand, PageResult<Notification>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IMapper _mapper;

    public GetNotificationsCommandHandler(INotificationRepository notificationRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _mapper = mapper;
    }

    public async Task<PageResult<Notification>> Handle(GetNotificationsCommand request, CancellationToken cancellationToken)
    {
        var result = await _notificationRepository.Get(request.UserId, request.Paging, cancellationToken);

        return _mapper.Map<PageResult<Notification>>(result);
    }
}
