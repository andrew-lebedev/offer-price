using AutoMapper;
using MediatR;
using OfferPrice.Profile.Application.Exceptions;
using OfferPrice.Profile.Domain.Interfaces;

namespace OfferPrice.Profile.Application.UserOperations.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException("User not found");
        }

        var userWithAnotherEmail = await _userRepository.GetByEmail(request.Email, cancellationToken);

        if (userWithAnotherEmail is not null)
        {
            throw new UserEmailException("User with this email already exists");
        }

        var update = _mapper.Map<Domain.Models.User>(request);
        await _userRepository.Update(update, cancellationToken);
    }
}
