using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherService;

public class JwtTokenBuilder(IConfiguration _configuration)
{
    readonly int _defaultExpiresInMinutes = int.Parse(_configuration.GetRequiredValue("Authentication:Jwt:ExpiresInMinutes"));
    readonly string _key = _configuration.GetRequiredValue($"Authentication:Jwt:Key");
    readonly string _issuer = _configuration.GetRequiredValue($"Authentication:Jwt:Issuer");
    readonly string _audience = _configuration.GetRequiredValue($"Authentication:Jwt:Audience");

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