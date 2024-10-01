using Newtonsoft.Json;

namespace AccountService.Models.Dtos;

/// <summary>
/// UserLoginDto
/// </summary>
public class UserLoginDto
{
    [JsonProperty("username")]
    public string? Username { get; set; }

    [JsonProperty("password")]
    public string? Password { get; set; }

}
