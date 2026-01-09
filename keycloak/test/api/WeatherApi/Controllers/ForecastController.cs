using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers;

[ApiController]
[Route("forecast")]
public class ForecastController : ControllerBase
{
    [HttpGet("weather")]
    [Authorize]
    public IActionResult GetWeather()
    {
        return Ok("Too Cold!");
    }
}