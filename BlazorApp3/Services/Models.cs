namespace BlazorApp3.Services
{
    public class AuthRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public record AuthResponse
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
    }

    public class RegisterRequest
    {
       
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
