using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Events.Events;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Application.UserOperations.LoginUser;
using OfferPrice.Profile.Application.UserOperations.RegisterUser;

namespace OfferPrice.Profile.Api.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;

    public AuthController(
        IMapper mapper,
        IPublishEndpoint publishEndpoint,
        IMediator mediator)
    {
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request, CancellationToken token)
    {
        var cmd = _mapper.Map<LoginUserCommand>(request);

        var commandResponse = await _mediator.Send(cmd, token);

        var userResponse = _mapper.Map<LoginUserResponse>(commandResponse);

        return Ok(userResponse);
    }

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] RegistrationUserRequest createUserRequest, CancellationToken token)
    {
        var cmd = _mapper.Map<RegisterUserCommand>(createUserRequest);

        var user = await _mediator.Send(cmd, token);

        await _publishEndpoint.Publish<UserCreatedEvent>(new(user.ToEvent()), token);

        return Ok();
    }
}

