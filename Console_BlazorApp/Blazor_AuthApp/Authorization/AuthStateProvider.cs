using Microsoft.AspNetCore.Components.Authorization;
using MarketerWeb.Authorization.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MarketerWeb.Authorization;

public class AuthStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly UserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public User? CurrentUser { get; set; } = new();

    public AuthStateProvider(IHttpContextAccessor httpContextAccessor,UserService blazorSchoolUserService)
    {
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        _userService = blazorSchoolUserService;
        _httpContextAccessor = httpContextAccessor;
    }

    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;
        if (authenticationState is not null)
        {
            CurrentUser = User.FromClaimsPrincipal(authenticationState.User);
        }
    }    

    public User GetUserFromHttp()
    {
       
        var headers = _httpContextAccessor.HttpContext.Request.Headers;        
        if ( headers.ContainsKey("X-Auth-Roles") == false )
        {
            return null;
        }
        else
        {
            var roles = new List<string>();
            if(headers.ContainsKey("X-Auth-Roles") && headers["X-Auth-Roles"].Count() > 0)
            {
                foreach(var text in headers["X-Auth-Roles"].ToList())
                {
                    roles.AddRange(text.Split(",").ToList());
                }
            }
            var userId = headers.ContainsKey("X-Auth-UserId") && headers["X-Auth-UserId"].Count() > 0? headers["X-Auth-UserId"].First(): "";
            return new User()
            {
                Roles = roles,
                UserId = userId
            };
        }
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();        
        var user = GetUserFromHttp();
        if (user is not null)
        {
            var  roles = CurrentUser.Roles.ToHashSet();
            roles.ExceptWith(user.Roles);
            CurrentUser = user;
            principal = user.ToClaimsPrincipal();            
        }       
        await Task.CompletedTask;
        return new(principal);
    }

    public async Task LoginAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = new User() 
        { 
            Username = "eckumoc",
            Password = "1423",
            UserId = "eckumoc@gmail.com",
            Roles = new List<string>() { "admin" }, 

        };
            //GetUserFromHttp();
        var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.UserId);
        headers["X-Auth-Roles"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Roles.ToArray());
        CurrentUser = user;

        if (user is not null)
        {
            principal = user.ToClaimsPrincipal();
        }
        await Task.CompletedTask;
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task LoginAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = await _userService.FindUserFromDatabaseAsync(username, password);
        var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues():
            new Microsoft.Extensions.Primitives.StringValues(user.UserId);
        headers["X-Auth-Roles"] = user == null?
			new Microsoft.Extensions.Primitives.StringValues(): 
			new Microsoft.Extensions.Primitives.StringValues(user.Roles.ToArray());
        CurrentUser = user;

        if (user is not null)
        {
            principal = user.ToClaimsPrincipal();
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
}
