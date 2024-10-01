using AccountService.Models.Entities;

namespace AccountService.Repositories;
public interface IUserRepository
{
    Task<User?> GetUserByUsername(string? username);
    Task CreateUser(User user);
    Task DeleteUser(int id);
    Task<IEnumerable<User>> GetAllUsers();
    Task<User?> GetUserById(int id);
    Task UpdateUser(User user);
}
