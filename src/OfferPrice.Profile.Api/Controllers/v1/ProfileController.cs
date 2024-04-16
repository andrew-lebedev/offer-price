using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Common;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Application.UserOperations.GetUser;
using OfferPrice.Profile.Application.UserOperations.UpdateUser;

namespace OfferPrice.Profile.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/users")]
[ApiController]
[ApiVersion("1.0")]
public class ProfileController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;


    public ProfileController(
        IPublishEndpoint publishEndpoint,
        IMediator mediator,
        IMapper mapper)
    {
        _publishEndpoint = publishEndpoint;
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken token)
    {
        var clientId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = new GetUserCommand() { ClientId = clientId };

        var user = await _mediator.Send(cmd, token);

        var userResponse = _mapper.Map<GetUserResponse>(user);

        return Ok(userResponse);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest updateUserRequest, CancellationToken token)
    {
        var clientId = ClaimValuesExtractionHelper.GetClientIdFromUserClaimIn(HttpContext);

        var cmd = _mapper.Map<UpdateUserCommand>(updateUserRequest);
        cmd.UserId = clientId;

        await _mediator.Send(cmd, token);

        await _publishEndpoint.Publish(new(), token);

        return Ok();
    }
}

