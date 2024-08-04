
using System.Collections.Generic;

using Microsoft.Extensions.Logging;
/// <summary>
/// Служба уровня приложения, содержит пользователей сеансов
/// </summary>
public class AuthorizationServices : AuthorizationCollection<ServiceContext>, APIServices
{
    public AuthorizationServices(AuthorizationOptions options) : base( options) { }

    public IDictionary<string, string> GetApis()
    {
        throw new System.NotImplementedException();
    }

    public IDictionary<string, string> Signin(UserContext user)
    {
        throw new System.NotImplementedException();
    }
}

