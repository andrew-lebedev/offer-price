using AutoMapper;
using MediatR;
using OfferPrice.Profile.Application.Exceptions;
using OfferPrice.Profile.Domain.Interfaces;
using OfferPrice.Profile.Domain.Models;

namespace OfferPrice.Profile.Application.UserOperations.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Models.User>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<Models.User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email, cancellationToken);

        if (user is not null)
        {
            throw new UserEmailException("User with this email already exists");
        }

        user = _mapper.Map<User>(request);

        var role = await _roleRepository.GetByName("user", cancellationToken);

        user.Roles = new List<Role> { role };

        await _userRepository.Create(user, cancellationToken);

        return _mapper.Map<Models.User>(user);
    }
}
