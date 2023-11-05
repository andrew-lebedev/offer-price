using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Events;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Domain;

namespace OfferPrice.Profile.Api.Controllers.v1;

[Route("api/v{version:apiVersion}/users")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly IProducer _producer;
    private readonly IMapper _mapper;
    public ProfileController(IUserRepository users, IProducer producer, IMapper mapper)
    {
        _users = users;
        _producer = producer;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] string id, CancellationToken token)
    {
        var user = await _users.Get(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var userResponse = _mapper.Map<Models.User>(user);

        return Ok(userResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserRequest updateUserRequest, CancellationToken token)
    {
        var user = await _users.Get(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var update = _mapper.Map(updateUserRequest, user);
        await _users.Update(update, token);

        return Ok();
    }
}

