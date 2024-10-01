using AccountService.Helpers;
using AccountService.Models.Entities;
using AccountService.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.Services;
public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IConfiguration configuration;

    public UserService(IConfiguration configuration, IUserRepository userRepository)
    {
        this.configuration = configuration;
        this.userRepository = userRepository;
    }

    /// <summary>
    /// To Authenticate The User's Existence In The Database
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<bool> AuthenticateAsync(string? username, string? password)
    {
        // Find The User By Username
        var user = await this.userRepository.GetUserByUsername(username);

        if (user != null && SecurePasswordHasher.Verify(password ?? "", user.PasswordHash))
        {
            // Authentication Successful
            return true;
        }
        else
        {
            // Authentication Failed
            return false;
        }
    }
    /// <summary>
    /// Generates Jwt Token For Users
    /// </summary>
    /// <param name="username"></param>
    /// <param name="role"></param>
    /// <returns></returns>
    public async Task<(string, DateTime)> GenerateTokenForUsers(string? username, string? role)
    {
        // Create and sign JWT token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(this.configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, username??""),
                new Claim(ClaimTypes.Role, role??""),
                        }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512),
        };

        var token = await Task.Run(() => tokenHandler.CreateToken(tokenDescriptor));
        var tokenString = tokenHandler.WriteToken(token);

        var result = (tokenString, token.ValidTo);

        return result;

    }

    /// <summary>
    /// Get The User By Its Username
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetUserByUsernameAsync(string? username) => await this.userRepository.GetUserByUsername(username);

    /// <summary>
    /// User Creation
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task CreateUserAsync(User user)
    {
        //Puts Hashed Password In Username Password In The Database
        user.PasswordHash = SecurePasswordHasher.Hash(user.PasswordHash);
        await this.userRepository.CreateUser(user);
    }

    public async Task UpdateUserAsync(User user) => await this.userRepository.UpdateUser(user);
    public async Task<IEnumerable<User>> GetAllUsersAsync() => await this.userRepository.GetAllUsers();
    public async Task DeleteUserAsync(int id) => await this.userRepository.DeleteUser(id);
}