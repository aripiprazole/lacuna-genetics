namespace LacunaGenetics.Auth;

public interface IAuthService
{
    public Task<LoginResponse> Login(LoginRequest data);

    public Task<RegisterResponse> Register(RegisterRequest data);
}