using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace AccountService.Models.Entities;
/// <summary>
/// User
/// </summary>
public class User : IdentityUser
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }

}
