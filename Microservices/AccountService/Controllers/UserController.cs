using AccountService.Models.Dtos;
using AccountService.Models.Entities;
using AccountService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AccountService.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ILogger<UserController> logger;


    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        this.logger = logger;
        this.userService = userService;
    }
    /// <summary>
    /// To Register The User
    /// The Link = user/register
    /// </summary>
    /// <param name="userDTO"></param>
    /// <returns></returns>      
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto userDTO)
    {
        // validate The Input
        if (!this.ModelState.IsValid)
        {
            return this.BadRequest(this.ModelState);
        }

        var registrationSucceeded = false;

        var isThereUser = this.userService.GetUserByUsernameAsync(userDTO.Username).Result;

        // Creating A New User Object From The Dto
        var user = new User
        {
            UserName = userDTO.Username,
            PasswordHash = userDTO.Password,
            Email = userDTO.Email
        };
        try
        {
            if (isThereUser == null)
            {
                await this.userService.CreateUserAsync(user);

            }
            else
            {
                this.logger.LogError("{Message}", "There Is A User With This Selected Username Already, Pick Another Username");
                return this.BadRequest(new { status = false, msg = "There Is A User With This Selected Username Already , Pick Another Username" });

            }

            registrationSucceeded = true;

            if (registrationSucceeded)
            {
                return this.Ok(new { status = true });
            }
            else
            {
                return this.BadRequest(new { status = false, msg = "Registration Failed" });

            }

        }
        catch (Exception)
        {
            return this.BadRequest(new { status = false, msg = "Registration Failed" });
        }

    }
    /// <summary>
    /// To Login That Makes JwtToken
    /// The Link = user/login
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public ActionResult Login([FromBody] UserLoginDto userDto)
    {
        // Validating The Username And Password Retrieved From Database
        var result = this.userService.AuthenticateAsync(userDto.Username, userDto.Password).Result;

        if (result)
        {
            //Creating Token While Authentication Is Successful
            ////Later Use  [Authorize(Roles = "TypicalUserRole")] To Authorize Any Action Based On This Token
            var createdToken = this.userService.GenerateTokenForUsers(userDto.Username, "TypicalUserRole").Result;
            if (createdToken.Item1 == "")
            {
                return this.Unauthorized();
            }

            return this.Ok(new { status = true, user = userDto, token = createdToken.Item1, expiration = createdToken.Item2 });
        }
        else
        {
            //While Authentication Is Failed
            return this.Ok(new { status = false, msg = "Not Found With This username and password" });
        }


    }

    //For The Future Development
    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        try
        {
            var username = this.User.FindFirst(ClaimTypes.Name)?.Value;
            var user = await this.userService.GetUserByUsernameAsync(username);

            if (user == null)
            {
                return this.NotFound();
            }

            return this.Ok(user);
        }
        catch (Exception ex)
        {
            return this.BadRequest(ex.Message);
        }
    }


}