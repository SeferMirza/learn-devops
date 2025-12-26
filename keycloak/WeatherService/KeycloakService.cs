using System.Text.Json;

namespace WeatherService;

public class KeycloakClient(IConfiguration _configuration, HttpClient _httpClient)
{
    private readonly HttpClient _httpClient = _httpClient;
    private readonly string _tokenEndpoint = _configuration.GetRequiredValue("Keycloak:TokenEndpoint");
    private readonly string _clientId = _configuration.GetRequiredValue("Keycloak:ClientId");
    private readonly string _clientSecret = _configuration.GetRequiredValue("Keycloak:ClientSecret");

    public async Task<string?> GetTokenByCodeAsync(string code, string redirectUri)
    {
        var parameters = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "code", code },
            { "client_id", _clientId },
            { "client_secret", _clientSecret },
            { "redirect_uri", redirectUri }
        };

        var content = new FormUrlEncodedContent(parameters);
        var response = await _httpClient.PostAsync(_tokenEndpoint, content);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var accessToken = doc.RootElement.GetProperty("access_token").GetString();

        return accessToken;
    }
}