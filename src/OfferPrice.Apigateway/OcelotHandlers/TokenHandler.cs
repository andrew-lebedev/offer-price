using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using OfferPrice.Apigateway.ConfigOptions;
using OfferPrice.Apigateway.Helpers;
using OfferPrice.Apigateway.TokenProvider;
using System.Net;
using System.Text.Json;

namespace OfferPrice.Apigateway.OcelotHandlers;

public class TokenHandler : DelegatingHandler
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly TokenConfigOptions _tokenConfigOptions;
    private readonly IMemoryCache _memoryCache;

    public TokenHandler(
        ITokenGenerator tokenGenerator, 
        IMemoryCache memoryCache, 
        IOptions<TokenConfigOptions> options)
    {
        _tokenGenerator = tokenGenerator;
        _memoryCache = memoryCache;
        _tokenConfigOptions = options.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var userServiceResponse = await base.SendAsync(request, cancellationToken);

        if (userServiceResponse.StatusCode is not HttpStatusCode.OK)
            return userServiceResponse;

        var content = await userServiceResponse.Content.ReadAsStringAsync();
        var parsedContent = JsonDocument.Parse(content);
        var clientId = parsedContent.RootElement.GetProperty("id").GetString();
        if (clientId == null)
        {
            throw new Exception("Client id is not found");
        }

        var accessToken = _tokenGenerator.GenerateAccessToken(clientId);
        if (accessToken is null)
        {
            throw new Exception("Access token is not generated");
        }

        var refreshToken = _tokenGenerator.GenerateRefreshToken();
        if (refreshToken is null)
        {
            throw new Exception("Refresh token is not generated");
        }

        var refreshTokenExpiration = TimeSpan.FromMinutes(_tokenConfigOptions.RefreshTokenLifetime);

        _memoryCache.Set(refreshToken.Token, clientId, new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = refreshTokenExpiration
        });

        return HttpResponseFactory.CreateResponseWithTokens(accessToken.Token, refreshToken.Token);
    }
}

