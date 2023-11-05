using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using OfferPrice.Apigateway.ConfigOptions;
using OfferPrice.Apigateway.Helpers;
using OfferPrice.Apigateway.Models;
using OfferPrice.Apigateway.TokenProvider;
using System.Linq;

namespace OfferPrice.Apigateway.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth/token")]
public class TokenController : ControllerBase
{
    private readonly IMemoryCache _memoryCache;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly JwtAuthenticationOptions _authenticationOptions;
    public TokenController(
        IMemoryCache memoryCache,
        ITokenGenerator tokenGenerator,
        IOptions<JwtAuthenticationOptions> options)
    {
        _memoryCache = memoryCache;
        _tokenGenerator = tokenGenerator;
        _authenticationOptions = options.Value;
    }

    [HttpGet("access")]
    public IActionResult GetNewAccesTokenByRefreshToken()
    {
        var refreshTokenHeaderName = _authenticationOptions.RefreshTokenHeaderName;
        if (refreshTokenHeaderName is null)
            throw new Exception("The Apigateway was not able to get refreshToken header name from .json");

        if (!Request.Headers.TryGetValue(refreshTokenHeaderName, out StringValues refreshTokenStringValues))
            return Unauthorized("The refresh token was not found in 'refreshToken' http header");

        var refreshToken = refreshTokenStringValues.ToString();
        if (!_memoryCache.TryGetValue(refreshToken, out string clientId))
            return Unauthorized("The refresh token was not found in database");

        var accessToken = _tokenGenerator.GenerateAccessToken(clientId);
        if (accessToken?.Token is null)
            throw new Exception("Access token is null");

        var result = new AuthenticationResponse(accessToken.Token, refreshToken);

        return Ok(result);
    }
}

