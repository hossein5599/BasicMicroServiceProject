using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AccountService.Services;
public class JwtService : IJwtService
{
    private readonly IConfiguration configuration;


    public JwtService(IConfiguration configuration) => this.configuration = configuration;
    /// <summary>
    /// To Get The User From Jwt Token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public string? GetUserIdFromJwt(HttpContext httpContext, CancellationToken cancellationToken)
    {

        var jwt = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
        if (string.IsNullOrEmpty(jwt))
        {
            return null;
        }
        var result = "";
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration["Jwt:Key"]);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);
            var jwtToken = (JwtSecurityToken)validatedToken;
            var username = jwtToken.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            result = username;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        if (string.IsNullOrEmpty(result))
        {
            return null;
        }
        return result;

    }
}
