namespace WeatherService;

public static class Extensions
{
    public static string GetRequiredValue(this IConfiguration configuration, string key)
    {
        var value = configuration[key];
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"Configuration key '{key}' is missing or empty.");
        }
        return value;
    }
}