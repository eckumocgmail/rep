using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

[Label("Проверка функции авторизации")]
public class AuthorizationServiceTest : TestingElement
{
    public AuthorizationServiceTest(IServiceProvider provider) : base(provider)
    {
    }

    public override void OnTest()
    {
        try
        {
            var auth = provider.Get<APIAuthorization>();
            var result = auth.Signin("eckumoc@gmail.com", "Gye*34FRtw",false);
            this.Info(result.ToJsonOnScreen());
              
        }
        catch(Exception ex)
        {
            Failed = true;
            Messages.Add(ex.Message);
        }

    }
}
