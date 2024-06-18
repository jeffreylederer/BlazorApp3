using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazerApp1.Services
{
    public interface ICookieValidatorService
    {
        Task ValidateAsync(CookieValidatePrincipalContext context);
    }
}
