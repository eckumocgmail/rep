using Console_UserInterface.Location;
using Console_UserInterface.Shared;

namespace Console_UserInterface
{
    public class UserInterfaceModule
    {
        public static void AddUserInterfaceServices( IServiceCollection services, IConfiguration configuration )
        {
            services.AddNavMenu();
            LocationModule.AddLocationService(services);
            services.AddTransient<IconsProvider>();
        }
    }
}
