using OfferPrice.Profile.Api.Models;

namespace OfferPrice.Profile.Api.Profiles;
public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserRequest, Domain.User>();

        CreateMap<Domain.User, Models.User>();
    }
}

