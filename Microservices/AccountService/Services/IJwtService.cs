namespace AccountService.Services;
public interface IJwtService
{
    /// <summary>
    /// To Get The User From Jwt Token
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    string? GetUserIdFromJwt(HttpContext httpContext, CancellationToken cancellationToken);
}
