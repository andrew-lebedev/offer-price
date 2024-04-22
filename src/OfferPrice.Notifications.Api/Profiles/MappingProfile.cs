using AutoMapper;
using OfferPrice.Notifications.Api.Models;
using OfferPrice.Notifications.Application.Models;

namespace OfferPrice.Notifications.Api.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Notification, Domain.Models.Notification>().ReverseMap();

        CreateMap<NotificationSettings, Domain.Models.NotificationsSettings>().ReverseMap();
    }
}
