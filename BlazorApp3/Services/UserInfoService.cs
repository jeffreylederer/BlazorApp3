using Microsoft.AspNetCore.Components.Authorization;

namespace BlazerApp3.Services;

public class UserInfoService : IUserInfoService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public UserInfoService(AuthenticationStateProvider authenticationStateProvider) =>
        _authenticationStateProvider = authenticationStateProvider ??
                                       throw new ArgumentNullException(nameof(authenticationStateProvider));

    public async Task<string?> GetUserIdAsync()
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authenticationState.User.Identity?.Name;
    }
    
    public async Task<string?> GetUserClaimValueAsync(string claimType)
    {
        var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return authenticationState.User.Claims.FirstOrDefault(x => string.Equals(x.Type, claimType, StringComparison.Ordinal))?.Value;
    }
}