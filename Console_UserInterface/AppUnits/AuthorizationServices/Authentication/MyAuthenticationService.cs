using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text.Json;
using System.Linq;

namespace Console_AuthModel.AuthorizationServices.Authentication
{
    public class MyAuthenticationService: IAuthenticationService
    {
        private readonly SigninUser signin;

        public MyAuthenticationService(SigninUser signin)
        {
            this.signin = signin;
        }

        public async Task<AuthenticateResult> AuthenticateAsync(HttpContext context, string? scheme)
        {
            await Task.CompletedTask;
            
            switch (scheme.ToUpper())
            {
                case "COOKIES":
                    {
                        if (this.signin.IsSignin())
                        {
                            ClaimsPrincipal user = new ClaimsPrincipal();
                            var tiket = new AuthenticationTicket(user, scheme);
                            return AuthenticateResult.Success(tiket);
                        }
                        else
                        {
                            return AuthenticateResult.Fail(new Exception("Не авторизован"));
                        }
                        break;
                    }
                case "BASIC":
                    {
                        ClaimsPrincipal user = new ClaimsPrincipal();
                        var tiket = new AuthenticationTicket(user, scheme);
                        return AuthenticateResult.Success(tiket);

                    }
                case "BEARER":
                    {
                        ClaimsPrincipal user = new ClaimsPrincipal()
                        {

                        };
                        var tiket = new AuthenticationTicket(user, scheme);

                        return AuthenticateResult.Success(tiket);
                        /*try
                        {
                            var token = GetTokenFromHeader(context, scheme);
                            var result = ValidateToken(scheme, token);
                            ClaimsPrincipal user = new ClaimsPrincipal();
                            var tiket = new AuthenticationTicket(user, scheme);

                            return AuthenticateResult.Success(tiket);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return AuthenticateResult.Fail(ex);
                        }*/

                    }
                default: throw new ArgumentException(scheme.ToUpper());
            }

        }

        private bool ValidateToken(string scheme, string token)
        {

            switch (scheme.ToUpper())
            {
                case "BASIC":
                    {

                        break;
                    }
                case "BEARER":
                    {

                        break;
                    }
                default: throw new ArgumentException("scheme");
            }
            return true;
        }

        private static string GetTokenFromHeader(HttpContext context, string? scheme)
        {
            var authenticationText = context.Request.Headers["Authorization"].ToString();
            var authenticationMessages = authenticationText.Split(",").ToList().Select(item => item.Trim()).Where(item => String.IsNullOrWhiteSpace(item) == false);
            var tokens = new Dictionary<string, string>(authenticationMessages.ToList().Select(item =>
            {
                var spices = item.Split(" ");
                return new KeyValuePair<string, string>(spices[0].ToUpper(), spices[1]);
            }));
            if (tokens.ContainsKey(scheme.ToUpper()) == false)
            {
                throw new Exception($"Нет токена {scheme}");
            }
            else
            {
                var token = tokens[scheme.ToUpper()];
                return token;
            }
        }

        public async Task ChallengeAsync(HttpContext context, string? scheme, AuthenticationProperties properties)
        {
            await Task.CompletedTask;
            Console.WriteLine($"ChallengeAsync {scheme} \n {JsonSerializer.Serialize(properties)}");
        }

        public async Task ForbidAsync(HttpContext context, string? scheme, AuthenticationProperties properties)
        {
            await Task.CompletedTask;
            Console.WriteLine($"ForbidAsync {scheme} \n {JsonSerializer.Serialize(properties)}");
            //await context.Response.WriteAsync("forbid");
        }

        public async Task SignInAsync(HttpContext context, string? scheme, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            await Task.CompletedTask;
            Console.WriteLine($"SignInAsync {scheme} \n {JsonSerializer.Serialize(properties)}");

            //await context.Response.WriteAsync("SignInAsync");
        }

        public async Task SignOutAsync(HttpContext context, string? scheme, AuthenticationProperties properties)
        {
            await Task.CompletedTask;
            Console.WriteLine($"SignOutAsync {scheme} \n {JsonSerializer.Serialize(properties)}");
            //await context.Response.WriteAsync("SignOutAsync");
        }
    }
}
