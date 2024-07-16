using BlazorApp3.Model;
using BlazorApp3.NewModels.DTOs;

namespace BlazerApp3.Services
{
    public interface IUsersService : IDisposable
    {
        Task<string> GetSerialNumberAsync(int userId);
        Task<User> FindUserAsync(string username, string password);
        ValueTask<User> FindUserAsync(int userId);
        Task UpdateUserLastActivityDateAsync(int userId);
        
        IAsyncEnumerable<UserRegisterDTO> GetAllUsersAsync();

    }
}
