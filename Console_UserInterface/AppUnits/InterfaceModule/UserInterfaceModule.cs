using Blazored.Modal;
using Console_UserInterface.AppUnits.InterfaceModule.InterfaceServices.InputModal;
using Console_UserInterface.Services.App;
using Console_UserInterface.Services.Location;
using Console_UserInterface.Services.View;

namespace Console_UserInterface.AppUnits.InterfaceModule
{
    public class UserInterfaceModule
    {
        public static void AddUserInterfaceServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<AppBuilder>();
            services.AddSingleton<AsyncContext>();
            services.AddTransient<HelpService>();
            services.AddTransient<ComponentRegistry>();
            services.AddTransient<IUserModelsService, UserModelsService>();
            services.AddTransient<IInputService, InputService>();
            
            services.AddNavMenu();
            services.AddBlazoredModal();
            services.AddInputModal();
            LocationModule.AddLocationService(services);
            services.AddTransient<IBootstrapModalService, BootstrapModalService>();
            services.AddTransient<IconsProvider>();
        }
    }
}
