using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Events.Events;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Domain.Interfaces;
using OfferPrice.Profile.Domain.Models;

namespace OfferPrice.Profile.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthController(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken token)
    {
        var userEntity = await _userRepository.GetByEmailAndPassword(request.Email, request.Password, token);

        if (userEntity == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var userResponse = _mapper.Map<LoginUserResponse>(userEntity);

        return Ok(userResponse);
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserRequest createUserRequest, CancellationToken token)
    {
        var user = _mapper.Map<Domain.Models.User>(createUserRequest);

        var role = await _roleRepository.GetByName("user", token);

        user.Roles = new List<Role> { role };

        await _userRepository.Create(user, token);

        await _publishEndpoint.Publish<UserCreatedEvent>(new(user.ToEvent()));

        return Ok();
    }
}

