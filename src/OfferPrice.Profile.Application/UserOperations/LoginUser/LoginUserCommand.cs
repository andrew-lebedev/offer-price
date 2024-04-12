using MediatR;
using OfferPrice.Profile.Application.Models;

namespace OfferPrice.Profile.Application.UserOperations.LoginUser;

public class LoginUserCommand : IRequest<User>
{
    public string Email { get; set; }

    public string Password { get; set; }
}
