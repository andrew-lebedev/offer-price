using AutoMapper;
using MediatR;
using OfferPrice.Profile.Application.Exceptions;
using OfferPrice.Profile.Application.Models;
using OfferPrice.Profile.Domain.Interfaces;

namespace OfferPrice.Profile.Application.UserOperations.GetUser;

public class GetUserCommandHandler : IRequestHandler<GetUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.ClientId, cancellationToken);

        if (user == null)
        {
            throw new UserNotFoundException("User not found");
        }

        return _mapper.Map<User>(user);
    }
}
