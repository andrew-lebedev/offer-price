using OfferPrice.Profile.Api.Models;

namespace OfferPrice.Profile.Api.Profiles;
public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserRequest, Domain.User>();
        CreateMap<UpdateUserRequest, Domain.User>();

        CreateMap<Domain.User, User>();
    }
}

