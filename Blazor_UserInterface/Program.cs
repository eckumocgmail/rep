using Blazor_UserInterface.Data;
using AppFormsModule;
using Console_UserInterface.AppUnits;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Console_UserInterface.AppUnits.AuthorizationBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Console_AuthModel.AuthorizationModel;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_UserInterface.Services;
using Google_LoginApplication.Areas.Identity.Modules.ReCaptcha;
using pickpoint_delivery_service;

namespace Blazor_UserInterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var unit = new DeliveryServicesUnit();
            //unit.DoTest().ToDocument().WriteToConsole();
            new InputFormModel(new UserMessage()).ToJsonOnScreen().WriteToConsole();
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly.Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(typeof(Console_InputApplication.Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Console_DataConnector.Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Console_UserInterface.Program).Assembly);
            /*ServiceFactory.Get().Info(typeof(UserAccount).New().GetType().Name);
            object p = typeof(UserAccount).New();
            p = "UserAccount".New();
            ServiceFactory.Get().Info(p.ToJsonOnScreen());
            ServiceFactory.Get().Info(p.GetType().GetProperties().Select(p => p.Name).ToJsonOnScreen());
            p.GetOwnPropertyNames().ToJsonOnScreen().WriteToConsole();*/

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddInputForm();
            builder.Services.AddBlazorContextMenu();
            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();
            builder.Services.AddTransient<GeoLocationService>();
            builder.Services.AddSingleton<AppRouterMiddleware>();
            builder.Services.AddScoped<SqlServerWebApi>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<UserAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<UserAuthStateProvider>());
            builder.Services.AddAuthorizationCore(config =>
            {
                //todo configure authorization
            });
            RecaptchaModule.ConfigureServices(builder.Configuration, builder.Services);
            AuthorizationModule.ConfigureServices(builder.Services);
            ModuleUser.ConfigureServices(builder.Configuration, builder.Services);
            ModuleService.ConfigureServices(builder.Configuration, builder.Services);
            DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);
            //DeliveryDbContext.CreateDeliveryData(builder.Services, builder.Configuration);
            DbContextUserInitializer.CreateUserData(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run(); 
        }
    }
}