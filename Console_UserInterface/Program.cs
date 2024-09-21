using Console_AuthModel.AuthorizationModel;
using Google_LoginApplication.Areas.Identity.Modules.ReCaptcha;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using pickpoint_delivery_service;
using Console_UserInterface.AppUnits.AuthorizationBlazor;
using System.Reflection;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_UserInterface.AppUnits;
using Console_UserInterface.Services;
using Blazored.Modal;
using Console_UserInterface.AppUnits.InterfaceModule;
using Ex;

/// приложение
namespace Console_UserInterface
{
    /// <summary>
    /// Программирование
    /// </summary>
    public class Program
    {

        /// <summary>
        /// Тестирование
        /// </summary>
        public static class Test
        {
            public static void TestApp()            
                => new UserInterfaceUnit(AppProviderService.GetInstance()).DoTest(false).ToDocument().WriteToConsole();

            public static void TestTypes()
            {
                ServiceFactory.Get().AddType(typeof(Dictionary<,>));
                ServiceFactory.Get().Info(typeof(Dictionary<,>).GetTypeName());
                "Dictionary<String,Object>".ToType();
            }

            public static void TestInputField()
            {
                var model = new InputFormModel();
                model.Item = new AttributesInputTest.CollectionModel();
                var field = model.CreateFormField(model.Item.GetType(), nameof(AttributesInputTest.CollectionModel.ListDateModel));
                field.ToJsonOnScreen().WriteToConsole();
            }
        }


        /// <summary>
        /// Точка входа
        /// </summary>
        public static void Main(string[] args)
        {
            
             
            RegTypes();           
            UpdateDatabases();
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            Configure(builder);
        }


        /// <summary>
        /// Регистрация типов
        /// </summary>
        private static void RegTypes()
        {
            ServiceFactory.Get().Info("регистрирую типы данных ");
            ServiceFactory.Get().AddTypes(Assembly.GetExecutingAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetCallingAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetEntryAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetExecutingAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(Assembly.GetCallingAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(Assembly.GetEntryAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly.Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(System.Object).Assembly);
            ServiceFactory.Get().AddTypes(typeof(string).Assembly);
            ServiceFactory.Get().AddTypes(typeof(JsonPropertyAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Newtonsoft.Json.JsonArrayAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(System.Data.SqlClient.SqlClientPermission).Assembly);
        }


        
        public static void Configure(WebApplicationBuilder builder)
        {
            var app = builder.Build();
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.MapControllers();

            app.UseMiddleware<AppRouterMiddleware>();
            ApiOdbc.UseOdbc(app);
            ApiCall.UseApi(app)
                ;
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.Run();
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddBlazorContextMenu();        
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddBlazoredModal();

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();
            
            
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

            UserInterfaceModule.AddUserInterfaceServices(builder.Services, builder.Configuration);
            SesionModule.AddSessionService(builder.Services);
            RecaptchaModule.ConfigureServices(builder.Configuration, builder.Services);
            AuthorizationModule.ConfigureServices(builder.Services);
            ModuleUser.ConfigureServices(builder.Configuration, builder.Services);
            ModuleService.ConfigureServices(builder.Configuration, builder.Services);
            DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);
            //DeliveryDbContext.CreateDeliveryData(builder.Services, builder.Configuration);
            DbContextUserInitializer.CreateUserData(builder.Services, builder.Configuration);
        }


        [Label("Создание структуры баз данных")]
        public static void UpdateDatabases()
        {          
            using (var db = new DbContextUser())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            using (var db = new DbContextService())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.ServiceContexts.ToList().ToJsonOnScreen().WriteToConsole();
            }
        }

        

                    
    }
}
