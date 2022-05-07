using System.Text.Json.Serialization;

namespace LacunaGenetics.Auth;

public class RegisterRequest
{
    public string Username { get; }
    public string Email { get; }
    public string Password { get; }

    public RegisterRequest(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;
    }
}