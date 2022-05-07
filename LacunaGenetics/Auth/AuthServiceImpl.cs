using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace LacunaGenetics.Auth;

public class AuthServiceImpl : IAuthService
{
    private readonly HttpClient _client;

    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public AuthServiceImpl(HttpClient client)
    {
        _client = client;
    }

    public async Task<LoginResponse> Login(LoginRequest data)
    {
        var response = await _client.PostAsJsonAsync("/api/users/login", data, SerializerOptions);
        var content = await response.Content.ReadFromJsonAsync<LoginResponse>(SerializerOptions);
        if (content == null)
            throw new NullReferenceException("Login response is null");

        if (content.Message != null)
            throw new Exception($"Could not login: {content.Message}");

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(content.AccessToken);

        return content;
    }

    public async Task<RegisterResponse> Register(RegisterRequest data)
    {
        var response = await _client.PostAsJsonAsync("/api/users/register", data, SerializerOptions);
        var content = await response.Content.ReadFromJsonAsync<RegisterResponse>(SerializerOptions);
        if (content == null)
            throw new NullReferenceException("Register response is null");

        if (content.Message != null)
            throw new Exception($"Could not register: {content.Message}");

        return content;
    }
}