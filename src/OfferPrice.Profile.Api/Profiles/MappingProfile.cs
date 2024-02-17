using OfferPrice.Profile.Api.Models;

namespace OfferPrice.Profile.Api.Profiles;
public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserRequest, Domain.Models.User>();
        CreateMap<UpdateUserRequest, Domain.Models.User>();

        CreateMap<Domain.Models.User, User>();

        CreateMap<Domain.Models.User, LoginUserResponse>()
            .ForMember(x => x.Roles, y => y.MapFrom(k => k.Roles.Select(m => m.Name)));

        CreateMap<RegistrationUserRequest, Domain.Models.User>()
            .ForMember(x => x.PasswordHash, y => y.MapFrom(k => k.Password)); // TODO: remake to hash later
    }
}

