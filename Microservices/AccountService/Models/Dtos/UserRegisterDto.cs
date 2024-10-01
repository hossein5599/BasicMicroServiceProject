using Newtonsoft.Json;

namespace AccountService.Models.Dtos;
/// <summary>
/// UserRegisterDto
/// </summary>
public class UserRegisterDto
{
    [JsonProperty("email")]
    public string? Email { get; set; }

    [JsonProperty("username")]
    public string? Username { get; set; }

    [JsonProperty("password")]
    public string? Password { get; set; }

}
