using MediatR;
using OfferPrice.Profile.Application.Models;

namespace OfferPrice.Profile.Application.UserOperations.GetUser;

public class GetUserCommand : IRequest<User>
{
    public string ClientId { get; set; }
}
