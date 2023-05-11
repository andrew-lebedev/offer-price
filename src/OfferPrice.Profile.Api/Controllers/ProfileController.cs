using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfferPrice.Profile.Api.Models;
using OfferPrice.Profile.Domain;

namespace OfferPrice.Profile.Api.Controllers;

[Route("api/users")]
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
        var users = await _users.Get(token);

        var userResponse = users.Select(_mapper.Map<Models.User>);

        return Ok(userResponse);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] string id, CancellationToken token)
    {
        var user = await _users.GetById(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var userResponse = _mapper.Map<Models.User>(user);

        return Ok(userResponse);
    }

    [HttpPost]
    public async Task<IActionResult> InsertUser([FromBody] UserRequest userRequest, CancellationToken token)
    {
        var user = _mapper.Map<Domain.User>(userRequest);

        await _users.Insert(user, token);

        var userResponse = _mapper.Map<Models.User>(user);

        return Ok(userResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] UserRequest userRequest, CancellationToken token)
    {
        var user = await _users.GetById(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        var userUpdated = _mapper.Map<Domain.User>(userRequest);

        await _users.Update(userUpdated, token);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] string id, CancellationToken token)
    {
        var user = _users.GetById(id, token);

        if (user == null)
        {
            return NotFound(ProblemDetailsFactory.CreateProblemDetails(HttpContext, 404));
        }

        await _users.Delete(id, token);

        return Ok();
    }
}

