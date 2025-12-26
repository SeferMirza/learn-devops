using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherService;

public class JwtTokenBuilder(IConfiguration _configuration, TimeProvider _timeProvider)
{
    readonly int _defaultExpiresInMinutes = 20;

    string Key => _configuration.GetValue($"Authentication:Jwt:{nameof(Key)}", "7F9aP2LkQxM4WJtE8RZsD0HnYcB5U3Vv");
    string? Issuer => _configuration.GetValue<string>($"Authentication:Jwt:{nameof(Issuer)}");
    string? Audience => _configuration.GetValue<string>($"Authentication:Jwt:{nameof(Audience)}");

    public string Build(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Console.WriteLine($"Building JWT for Issuer: {Issuer}, Audience: {Audience}");

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_defaultExpiresInMinutes),
            signingCredentials: creds
        );
        var res = new JwtSecurityTokenHandler().WriteToken(token);
        Console.WriteLine($"ress {res}");
        return res;
    }
}