using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherApi;

public class JwtTokenBuilder
{
    readonly int _defaultExpiresInMinutes = 1;
    readonly string _key = "7F9aP2LkQxM4WJtE8RZsD0HnYcB5U3Vv";
    readonly string _issuer = "http://localhost";
    readonly string _audience = "weather-api";

    public string Build(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_defaultExpiresInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}