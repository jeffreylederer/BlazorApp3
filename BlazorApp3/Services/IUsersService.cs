
using BlazerApp3.Models.DTOs;
using BlazorApp3.Model;

namespace BlazerApp3.Services
{
    public interface IUsersService : IDisposable
    {
        Task<string> GetSerialNumberAsync(int userId);
        Task<User> FindUserAsync(string username, string password);
        ValueTask<User> FindUserAsync(int userId);
        Task UpdateUserLastActivityDateAsync(int userId);
        Task<UserRegisterDTO> CreateUserAsync(UserRegisterDTO userRegisterDto);
        IAsyncEnumerable<UserRegisterDTO> GetAllUsersAsync();

    }
}
