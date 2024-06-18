namespace BlazorApp3.Services
{
    public interface IAuthService
    {
        AuthResponse? Login(AuthRequest request, IConfiguration configuration);
        bool Register(RegisterRequest request);


    }
}
