using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Events;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Domain;

namespace OfferPrice.Profile.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IProducer _producer;

    public AuthController(IUserRepository userRepository, IRoleRepository roleRepository, IProducer producer, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _producer = producer;
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
        var user = _mapper.Map<Domain.User>(createUserRequest);

        var role = await _roleRepository.GetByName("user", token);

        user.Roles = new List<Role> { role };

        await _userRepository.Create(user, token);

        _producer.SendMessage(new UserCreatedEvent(user.ToEvent()));

        return Ok();
    }
}

