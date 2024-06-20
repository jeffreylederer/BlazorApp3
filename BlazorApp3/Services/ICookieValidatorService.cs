using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazerApp3.Services
{
    public interface ICookieValidatorService
    {
        Task ValidateAsync(CookieValidatePrincipalContext context);
    }
}
