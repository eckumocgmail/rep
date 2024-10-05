using Blazored.Modal;
using Console_UserInterface.AppUnits.InterfaceModule.InterfaceServices.InputModal;
using Console_UserInterface.Services;
using Console_UserInterface.Services.Location;

namespace Console_UserInterface.AppUnits.InterfaceModule
{
    public class UserInterfaceModule
    {
        public static void AddUserInterfaceServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AppBuilder>();
            services.AddTransient<HelpService>();
            
            services.AddNavMenu();
            services.AddBlazoredModal();
            services.AddInputModal();
            LocationModule.AddLocationService(services);
            services.AddTransient<IBootstrapModalService, BootstrapModalService>();
            services.AddTransient<IconsProvider>();
        }
    }
}
