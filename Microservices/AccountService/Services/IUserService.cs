using AccountService.Models.Entities;

namespace AccountService.Services;
public interface IUserService
{
    /// <summary>
    /// Generates Jwt Token For Users
    /// </summary>
    /// <param name="username"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<(string, DateTime)> GenerateTokenForUsers(string? username, string? role);
    /// <summary>
    /// To Authenticate The User's Existence In The Database
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<bool> AuthenticateAsync(string? username, string? password);
    /// <summary>
    /// User Creation
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task CreateUserAsync(User user);

    /// <summary>
    /// Get The User By Its Username
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User?> GetUserByUsernameAsync(string? username);
    Task DeleteUserAsync(int id);
    Task<IEnumerable<User>> GetAllUsersAsync();

    Task UpdateUserAsync(User user);
}