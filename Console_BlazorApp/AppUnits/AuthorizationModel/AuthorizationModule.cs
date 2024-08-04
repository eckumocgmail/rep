using Console_AuthModel.AuthorizationServices.Authentication;
using Console_AuthModel.AuthorizationServices.AuthorizationApi;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Console_AuthModel.AuthorizationModel
{
    public class AuthorizationModule
    {
        public static void ConfigureServices( IServiceCollection services)
        {
            services.AddTransient<APIAuthorization, AuthorizationService>();
            services.AddTransient<IUserMessagesService, UserMessagesService>();
 
          
        }
    }
}
