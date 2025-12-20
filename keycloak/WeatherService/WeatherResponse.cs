public record WeatherResponse
{
    public int Temperature { get; init; }
    public bool HasRain { get; init; }
    public string Description { get; init; } = string.Empty;
    public string City { get; init; } = string.Empty;
    public DateTime Date { get; init; }
}
