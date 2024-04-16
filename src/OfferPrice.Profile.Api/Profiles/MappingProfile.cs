using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Application.Models;
using OfferPrice.Profile.Application.UserOperations.LoginUser;
using OfferPrice.Profile.Application.UserOperations.RegisterUser;
using OfferPrice.Profile.Application.UserOperations.UpdateUser;

namespace OfferPrice.Profile.Api.Profiles;
public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Models.User, User>()
            .ForMember(x => x.Roles, y => y.MapFrom(k => k.Roles.Select(m => m.Name)))
            .ReverseMap();

        CreateMap<RegistrationUserRequest, RegisterUserCommand>();
        CreateMap<RegisterUserCommand, Domain.Models.User>();

        CreateMap<LoginUserRequest, LoginUserCommand>();
        CreateMap<User, LoginUserResponse>();

        CreateMap<UpdateUserRequest, UpdateUserCommand>();
        CreateMap<UpdateUserCommand, Domain.Models.User>();

        CreateMap<User, GetUserResponse>();
    }
}

