using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace BKA.Tools.CrewFinding.API.Functions.Authentications;

public class TokenDecoder
{
    private readonly Token _token;

    public TokenDecoder(HttpRequest request)
    {
        var authorizationHeader = request.Headers["Authorization"];

        _token = new Token(authorizationHeader);
    }

    public string GetUserId()
    {
        var jwtHandler = new JwtSecurityTokenHandler();

        var jwtToken = jwtHandler.ReadJwtToken(_token.Value);
        var userId = jwtToken.Claims.First(claim => claim.Type == "sub").Value;
        
        return userId;
    }
}