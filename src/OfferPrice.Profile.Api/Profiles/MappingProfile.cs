using OfferPrice.Profile.Api.Models;

namespace OfferPrice.Profile.Api.Profiles;
public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<UserRequest, Domain.User>();

        CreateMap<Domain.User, Models.User>();
    }
}

