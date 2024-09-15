using System.Security.Claims;

namespace MarketerWeb.Authorization.Models;

public class User
{
     
    // account
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string UserId { get; set; } = "";
    
    public List<string> Roles { get; set; } = new();

    public ClaimsPrincipal ToClaimsPrincipal() => new(new ClaimsIdentity(new Claim[]
    {
        new (ClaimTypes.NameIdentifier, UserId),
        new (ClaimTypes.Name, Username),
        new (ClaimTypes.Hash, Password),
    }.Concat(Roles.Select(r => new Claim(ClaimTypes.Role, r)).ToArray()),
    "BlazorSchool"));

    public static User FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {
        UserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "",
        Username = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
        Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",        
        Roles = principal.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
    };
}