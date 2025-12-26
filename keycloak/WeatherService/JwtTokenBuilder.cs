using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherService;

public class JwtTokenBuilder(IConfiguration _configuration)
{
    readonly int _defaultExpiresInMinutes = 20;

    string Key => _configuration.GetRequiredValue($"Authentication:Jwt:{nameof(Key)}");
    string Issuer => _configuration.GetRequiredValue($"Authentication:Jwt:{nameof(Issuer)}");
    string Audience => _configuration.GetRequiredValue($"Authentication:Jwt:{nameof(Audience)}");

    public string Build(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_defaultExpiresInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}