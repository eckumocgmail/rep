using System;
using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using NSwag.CodeGeneration.CSharp;

public class AuthorizationServicesTest : TestingElement
{
    public AuthorizationServicesTest(IServiceProvider provider) : base(provider)
    {
    }


    public async Task<string> GenerateCSharpClient(string url)
    {
        var document = await NSwag.OpenApiDocument.FromUrlAsync(url);
        var settings = new CSharpClientGeneratorSettings
        {
            ClassName = "{controller}Client",
        };
        var generator = new CSharpClientGenerator(document, settings);
        return generator.GenerateFile();
    }
    public override void OnTest()
    {

        var url = "https://localhost:5001/swagger/v1/swagger.json";
        try
        {            
            var signup = provider.Get<SignupService>();
            var context = signup.Signup(
                new ServiceSertificate() {
                    PublicKey = new byte[] { 1 },
                    PrivateKey = new byte[] { 1 }
                },
                new ServiceInfo() {
                    Name = "UserApi",
                    Url = "https://localhost:7138/user/api",
                    Version = "1.0.0"
                });        
        }
        catch(Exception ex)
        {
            Failed = true;
            Messages.Add(ex.Message);
        }

    }
}
