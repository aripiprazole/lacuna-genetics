﻿using System.Text.Json.Serialization;

namespace LacunaGenetics.Auth;

public class LoginRequest
{
    public string Username { get; }
    public string Password { get; }

    public LoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }
}