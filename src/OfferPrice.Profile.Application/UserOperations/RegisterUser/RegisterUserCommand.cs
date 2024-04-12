using MediatR;
using OfferPrice.Profile.Application.Models;

namespace OfferPrice.Profile.Application.UserOperations.RegisterUser;

public class RegisterUserCommand : IRequest<User>
{
    public string Name { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }

    public string Password { get; set; }
}
