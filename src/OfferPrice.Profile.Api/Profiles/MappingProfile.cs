using OfferPrice.Profile.Api.Models;

namespace OfferPrice.Profile.Api.Profiles;
public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserRequest, Domain.User>();
        CreateMap<UpdateUserRequest, Domain.User>();

        CreateMap<Domain.User, User>();

        CreateMap<Domain.User, LoginUserResponse>()
            .ForMember(x => x.Roles, y => y.MapFrom(k => k.Roles.Select(m => m.Name)));

        CreateMap<RegistrationUserRequest, Domain.User>()
            .ForMember(x => x.PasswordHash, y => y.MapFrom(k => k.Password)); // TODO: remake to hash later
    }
}

