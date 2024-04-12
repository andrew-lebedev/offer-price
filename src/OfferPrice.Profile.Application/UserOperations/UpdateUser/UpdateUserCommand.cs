using MediatR;

namespace OfferPrice.Profile.Application.UserOperations.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}
