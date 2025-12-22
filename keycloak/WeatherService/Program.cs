using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WeatherService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.Audience = builder.Configuration["Jwt:Audience"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuers =
            [
                "http://localhost:8080/realms/test-realm",
                "http://keycloak:8080/realms/test-realm"
            ]
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "Weather Service is running!");

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

app.Run();