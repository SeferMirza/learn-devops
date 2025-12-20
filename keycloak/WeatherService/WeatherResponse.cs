namespace WeatherService;

public record WeatherResponse
{
public int Temperature { get; init; }
public string? Sky { get; init; }
}