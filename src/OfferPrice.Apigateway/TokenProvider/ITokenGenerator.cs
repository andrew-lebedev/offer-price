using OfferPrice.Apigateway.TokenProvider.Models;

namespace OfferPrice.Apigateway.TokenProvider;

public interface ITokenGenerator
{
    AccessToken GenerateAccessToken(string clientId);

    RefreshToken GenerateRefreshToken();
}

