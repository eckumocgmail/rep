using Console_AuthModel.AuthorizationModel;

using Console_BlazorApp;
using Console_BlazorApp.AppUnits.DeliveryTests;

using Google_LoginApplication.Areas.Identity.Modules.ReCaptcha;

using MarketerWeb.Authorization;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

using NSwag.CodeGeneration.CSharp;

using pickpoint_delivery_service;

using WebApiDetaliusBotTests;

namespace Console_Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
           

            using (var db = new DbContextUser())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.UserContexts_.ToList().ToJsonOnScreen().WriteToConsole();

            }
            using (var db = new DbContextService())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.ServiceContexts.ToList().ToJsonOnScreen().WriteToConsole();

            }
            using (var db = new DeliveryDbContext())
            {
                db.ProductsInStock.ToJsonOnScreen().WriteToConsole();

            }

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddTransient(typeof(UserContext), sp => sp.GetService<SigninUser>().Verify());
            builder.Services.AddTransient(typeof(UserPerson), sp => sp.GetService<SigninUser>().Verify().Person);

            builder.Services.AddHttpContextAccessor();

            // Authorization
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<AuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProvider>());
            builder.Services.AddAuthorizationCore(config =>
            {
                //todo configure authorization
            });

            pickpoint_delivery_service.DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);
            pickpoint_delivery_service.DeliveryDbContext.CreateDeliveryData(builder.Services, builder.Configuration);

            new ReservationOrderCheckoutUnit(AppProviderService.GetInstance()).DoTest(false).ToDocument();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<AppRouterMiddleware>();

            RecaptchaModule.ConfigureServices(builder.Configuration, builder.Services);
            AuthorizationDbContext.ConfigureServices(builder.Services);
            AuthorizationModule.ConfigureServices(builder.Services);
            ModuleUser.ConfigureServices(builder.Configuration, builder.Services);
            ModuleService.ConfigureServices(builder.Configuration, builder.Services);
            DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);

            
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}