using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using WeatherService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.AddAuthorization();

builder.Services.AddSingleton<JwtTokenBuilder>();

builder.Services.AddHttpClient<KeycloakClient>();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/weather", () =>
{
    var random = new Random();
    var weather = new WeatherResponse
    {
        Temperature = random.Next(-10, 40),
        Sky = random.Next(100) < 30 ? "Rainy" : "Sunny"
    };

    return Results.Ok(weather);
}).RequireAuthorization();

app.MapPost("/login-by-code", async ([FromServices] JwtTokenBuilder tokenBuilder, [FromServices] KeycloakClient keycloakClient, [FromBody] string code) =>
{
    var accessToken = await keycloakClient.GetTokenByCodeAsync(code);
    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
    var jwt = handler.ReadJwtToken(accessToken);
    var username = jwt.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

    List<Claim> claims = [];
    if (!string.IsNullOrEmpty(username))
    {
        claims.Add(new Claim(ClaimTypes.Name, username));
    }

    string token = tokenBuilder.Build(claims);
    return Results.Ok(new LoginResponse(token));
}).AllowAnonymous();

app.Run();