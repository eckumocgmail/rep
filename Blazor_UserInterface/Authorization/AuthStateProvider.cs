using Microsoft.AspNetCore.Components.Authorization;
 
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Console_AuthModel.AuthorizationModel.UserModel;

namespace MarketerWeb.Authorization;

public class AuthStateProvider2 : AuthenticationStateProvider, IDisposable
{
    private readonly UserService _userService;
    private readonly SigninUser _signin;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext? CurrentUser { get; set; } = new();

    public AuthStateProvider2(IHttpContextAccessor httpContextAccessor, SigninUser signin, UserService blazorSchoolUserService)
    {
        AuthenticationStateChanged += OnAuthenticationStateChangedAsync;
        _userService = blazorSchoolUserService;
        _signin = signin;
        _httpContextAccessor = httpContextAccessor;
    }

    private async void OnAuthenticationStateChangedAsync(Task<AuthenticationState> task)
    {
        var authenticationState = await task;
        if (authenticationState is not null)
        {
            CurrentUser = UserContext.FromClaimsPrincipal(authenticationState.User);
        }
    }    

    public UserContext GetUserFromHttp()
    {
        /*var headers = _httpContextAccessor.HttpContext.Request.Headers;        
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
        }*/
        var user = _signin.Verify();
        return user;
        /*return new UserContext()
        {
            Account = new() {
                Email = "eckumoc@gmail.com"
            },
            Roles = new List<UserRole>() { new UserRole(){
                Code="transport"
            } }
        };*/
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();        
        var user = GetUserFromHttp();
        
        if (user is not null)
        {
            var  roles = CurrentUser?.Roles?.ToHashSet();
            if(roles is not null)
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
        var user = GetUserFromHttp();
        var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Account.Email);
        headers["X-Auth-Roles"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues() :
            new Microsoft.Extensions.Primitives.StringValues(user.Roles.Select(r=>r.Code).ToArray());
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
        var user = //await _userService.FindUserFromDatabaseAsync(username, password);
            _signin.Signin(username, password).Result;
        if(user is null) 
        { 
            await Task.CompletedTask;
            return;
        }
        if ( user.Roles is null)
            user.Roles = new();
        var headers = _httpContextAccessor.HttpContext.Request.Headers;
        headers["X-Auth-UserId"] = user == null ?
            new Microsoft.Extensions.Primitives.StringValues():
            new Microsoft.Extensions.Primitives.StringValues(user.Account.Email);
        headers["X-Auth-Roles"] = user == null?
			new Microsoft.Extensions.Primitives.StringValues(): 
			new Microsoft.Extensions.Primitives.StringValues(user.Roles.Select(r => r.Code).ToArray());
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
