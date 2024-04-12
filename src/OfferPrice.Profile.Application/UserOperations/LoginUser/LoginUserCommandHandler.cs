using AutoMapper;
using MediatR;
using OfferPrice.Profile.Application.Exceptions;
using OfferPrice.Profile.Application.Models;
using OfferPrice.Profile.Domain.Interfaces;

namespace OfferPrice.Profile.Application.UserOperations.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<User> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByEmailAndPassword(request.Email, request.Password, cancellationToken);

        if (userEntity == null)
        {
            throw new UserNotFoundException("User not found");
        }

        return _mapper.Map<User>(userEntity);
    }
}
