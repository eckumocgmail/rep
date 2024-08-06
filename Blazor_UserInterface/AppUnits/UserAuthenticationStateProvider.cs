using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
 
using System.Data;
using System.Linq;
using Console_AuthModel.AuthorizationModel.UserModel;

 
/*
public class AuthStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly SigninUser _signin;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext? CurrentUser { get; set; } = new();
    public void Update(UserContext user)
    {
         
        var claim = ToClaimsPrincipal(user);
        NotifyAuthenticationStateChanged(Task.Run(() => new AuthenticationState(claim)));
       
    }
    public AuthStateProvider(IHttpContextAccessor httpContextAccessor, IServiceProvider provider)
    {
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        _signin = provider.Get<SigninUser>();
        _httpContextAccessor = httpContextAccessor;
    }

    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;
        if (authenticationState is not null)
        {
            CurrentUser = FromClaimsPrincipal(authenticationState.User);
        }
    }

    public UserContext GetUserFromHttp()
    {
        if (_signin.IsSignin() == false)
        {
            return null;
        }
        else
        {
            return _signin.Verify();
        }
    }

    public ClaimsPrincipal ToClaimsPrincipal(UserContext user)
    {
        if (user.Roles is null)
            user.Roles = new();
        var result = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new (ClaimTypes.NameIdentifier, user.Account.Email),
            new (ClaimTypes.Name, user.Person.GetFullName()),
            new (ClaimTypes.Hash, user.Account.Hash),
        }
        .Concat(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.Code)).ToArray()),

            "BlazorSchool"));

        return result;
    }
        

    public static UserContext FromClaimsPrincipal(ClaimsPrincipal principal) => new()
    {        
        Account = new()
        {
            Email = principal.FindFirst(ClaimTypes.Name)?.Value ?? "",
            Password = principal.FindFirst(ClaimTypes.Hash)?.Value ?? "",
        },        
        Roles = principal.FindAll(ClaimTypes.Role).Select(c => new UserRole(){ Code = c.Value }).ToList()
    };

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = GetUserFromHttp();
        if (user is not null)
        {
            var roles = CurrentUser.Roles.ToHashSet();
            roles.ExceptWith(user.Roles);
            CurrentUser = user;
            principal = ToClaimsPrincipal(user);
        }
        await Task.CompletedTask;
        return new(principal);
    }

    public async Task LoginAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = GetUserFromHttp();
        var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Account.Email);
        headers["X-Auth-Roles"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Roles.Select(r => r.Code).ToArray());
        CurrentUser = user;

        if (user is not null)
        {
            principal = ToClaimsPrincipal(user);
        }
        await Task.CompletedTask;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task LoginAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = _signin.Signin(username, password).Result;
        /* var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Account.Email);
        headers["X-Auth-Roles"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Roles.Select(r => r.Code).ToArray());* /
        CurrentUser = user;

        if (user is not null)
        {
            principal = ToClaimsPrincipal(user);
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task LogoutAsync()
    {
        // TODO: ClearBrowserUserDataAsync();
        var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = new Microsoft.Extensions.Primitives.StringValues();
        headers["X-Auth-Roles"] = new Microsoft.Extensions.Primitives.StringValues();
        await Task.CompletedTask;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }

    public void Dispose() => AuthenticationStateChanged -= OnAuthenticationStateChangedAsync;
}*/

 /**
public class UserAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly SigninUser _signin;
    private readonly DbContextUser _dbContextUser;

    public UserAuthenticationStateProvider(SigninUser signin,DbContextUser dbContextUser)
    {
        _signin = signin;
        _dbContextUser = dbContextUser;
    }

    public async Task Update(string name, List<string> roles)
    {
        ClaimsIdentity id = GetClaimIdentity(name, roles);
        NotifyAuthenticationStateChanged(Task.Run(() => new AuthenticationState(new ClaimsPrincipal(id))));
    }

    private static ClaimsIdentity GetClaimIdentity(string name, List<string> roles)
    {
        var claims = new List<Claim>() { new Claim("username", name) };
        claims.AddRange(roles.Select(role => new Claim("role", role)));
        ClaimsIdentity id = new ClaimsIdentity(claims, "Basic", "username", "role");
        return id;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        await Task.CompletedTask;
        if (_signin.IsSignin())
        {
            var user = _signin.Verify();
            var username = user.Account.Email;
            var roles = user.BusinessFunctions.Select(bf => _dbContextUser.UserRoles_.Find(bf.RoleId).Code).ToList();
            return new AuthenticationState(new ClaimsPrincipal(
                GetClaimIdentity(username, roles)
            ));
        }
        else
        {
            return new AuthenticationState(new ClaimsPrincipal(
            ));
        }

    }



}
 */