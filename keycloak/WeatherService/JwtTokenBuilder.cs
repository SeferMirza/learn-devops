using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WeatherService;

public class JwtTokenBuilder(IConfiguration _configuration, TimeProvider _timeProvider)
{
    readonly int _defaultExpiresInMinutes = 20;

    string Key => _configuration.GetValue($"Authentication:Jwt:{nameof(Key)}", "WeatherService");
    string? Issuer => _configuration.GetValue<string>($"Authentication:Jwt:{nameof(Issuer)}");
    string? Audience => _configuration.GetValue<string>($"Authentication:Jwt:{nameof(Audience)}");

    public string Build(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var header = new JwtHeader(creds);
        var payload = new JwtPayload(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            notBefore: null,
            issuedAt: _timeProvider.GetUtcNow().DateTime,
            expires: _timeProvider.GetUtcNow().AddMinutes(_defaultExpiresInMinutes).DateTime
        );

        return new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(header, payload));
    }
}