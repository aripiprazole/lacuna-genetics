using System.Text.Json.Serialization;

namespace LacunaGenetics.Auth;

public class LoginResponse
{
    public string? AccessToken { get; }
    public string Code { get; }
    public string? Message { get; }

    [JsonConstructor]
    public LoginResponse(string accessToken, string code, string? message)
    {
        AccessToken = accessToken;
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        var str = $"RegisterResponse{{Code: {Code}";

        if (AccessToken != null)
            str += $", AccessToken: {AccessToken}";

        if (Message != null)
            str += $", Message: {Message}";

        return str + "}";
    }
}