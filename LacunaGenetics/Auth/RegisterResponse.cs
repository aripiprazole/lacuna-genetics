using System.Text.Json.Serialization;

namespace LacunaGenetics.Auth;

public class RegisterResponse
{
    public readonly string Code;
    public readonly string? Message;
    
    [JsonConstructor]
    public RegisterResponse(string code, string? message)
    {
        Code = code;
        Message = message;
    }

    public override string ToString()
    {
        var str = $"RegisterResponse{{Code: {Code}";

        if (Message != null)
            str += $", Message: {Message}";

        return str + "}";
    }
}