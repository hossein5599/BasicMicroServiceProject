using AccountService.Data;
using AccountService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext dbContext;

    public UserRepository(ApplicationDbContext dbContext) => this.dbContext = dbContext;

    public async Task<User?> GetUserByUsername(string? username) => await this.dbContext.Users.Where(u => u.UserName == username).FirstOrDefaultAsync();

    public async Task<User?> GetUserById(int id) => await this.dbContext.Users.FindAsync(id);

    public async Task<IEnumerable<User>> GetAllUsers() => await this.dbContext.Users.ToListAsync();

    public async Task CreateUser(User user)
    {
        await this.dbContext.Users.AddAsync(user);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        this.dbContext.Users.Update(user);
        await this.dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var userToDelete = await this.dbContext.Users.FindAsync(id);
        if (userToDelete != null)
        {
            this.dbContext.Users.Remove(userToDelete);
        }
        await this.dbContext.SaveChangesAsync();
    }
}