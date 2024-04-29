using System.IdentityModel.Tokens.Jwt;

namespace BKA.Tools.CrewFinding.API.Functions.Authentications;

public class TokenDecoder
{
    private readonly Token _token;

    public TokenDecoder(HttpRequestData request)
    {
        var authorizationHeader = request.Headers.GetValues("Authorization").FirstOrDefault();

        _token = new Token(authorizationHeader ?? string.Empty);
    }

    public TokenDecoder(string userToken)
    {
        _token = new Token(userToken);
    }

    public string GetUserId()
    {
        var securityTokenHandler = new JwtSecurityTokenHandler();

        var readToken = securityTokenHandler.ReadJwtToken(_token.Value);
        var userId = readToken.Claims.First(claim => claim.Type == "sub").Value;
        
        return userId;
    }
}