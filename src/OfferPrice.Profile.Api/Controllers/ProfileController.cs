using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Domain;

namespace OfferPrice.Profile.Api.Controllers;

[Route("users")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IUserRepository _users;
    private readonly IMapper _mapper;
    public ProfileController(IUserRepository users, IMapper mapper)
    {
        _users = users;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken token)
    {
        var users = await _users.GetUsers(token);

        var userResponse = users.Select(_mapper.Map<Models.User>);

        return Ok(userResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] string id, CancellationToken token)
    {
        var user = await _users.GetUserById(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var userResponse = _mapper.Map<Models.User>(user);

        return Ok(userResponse);
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser([FromBody] UserRequest insertUser, CancellationToken token)
    {
        var user = _mapper.Map<Domain.User>(insertUser);

        await _users.InsertUser(user, token);

        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserRequest updateUser, CancellationToken token)
    {
        var user = await _users.GetUserById(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var userUpdated = _mapper.Map<Domain.User>(updateUser);

        await _users.UpdateUser(userUpdated, token);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id, CancellationToken token)
    {
        var user = _users.GetUserById(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        await _users.DeleteUser(id, token);

        return Ok();
    }
}

