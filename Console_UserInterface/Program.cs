using Console_AuthModel.AuthorizationModel;
using Google_LoginApplication.Areas.Identity.Modules.ReCaptcha;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using pickpoint_delivery_service;
using Console_UserInterface.AppUnits.AuthorizationBlazor;
using System.Reflection;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_UserInterface.Services;
using Blazored.Modal;
using Console_UserInterface.AppUnits.InterfaceModule;
using Console_UserInterface.Shared;
using System.Data.OleDb;
using System.Data;
using BookingModel;
using Console_UserInterface.Services.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;

/// приложение
namespace Console_UserInterface
{

    /// <summary>
    /// Программирование
    /// </summary>
    public class Program
    {
        public SummaryViewModel[] Items { get; set; } = new SummaryViewModel[]
        {
            new SummaryViewModel()
            {
                Title = "Заголовок",
                Description = "Вырезка из статьи с найденым контентом, вырезка из статьи с найденым контентом, вырезка из статьи с найденым контентом, вырезка из статьи с найденым контентом, вырезка из статьи с найденым контентом, вырезка из статьи с найденым контентом. ",
                Links = new LinkViewModel[]
                {
                    new LinkViewModel()
                    {
                        Label = "Перейти"
                    }
                }
            }
        };


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



            using (var db = new DbContextUser())
            {
                var dbset = db.GetDbSet(nameof(UserAccount));
                foreach(var p in dbset)
                {
                    db.Info(((object)p).ToString());
                }
                
            }
                //Console_DataConnector.Program.Main(args);          

                args.Info(Assembly.GetExecutingAssembly().GetName().Name);
         
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            Configure(builder); 
        }

        static void testAttr()
        {
            //new SelectDataAttribute($"{nameof(UserRole)}.{nameof(UserRole.Code)}").Options.ToJsonOnScreen().WriteToConsole();

        }

        static void upgrade()
        {
            UpdateDatabases();
        }
        static void testHelp()
        {

            //new HelpService().GetContents().ToJsonOnScreen().WriteToConsole();
            //new HelpService().GetArticles().ToJsonOnScreen().WriteToConsole();
        }

        /// <summary>
        /// Регистрация типов
        /// </summary>
        private static void RegTypes()
        {
            ServiceFactory.Get().Info("регистрирую типы данных ");
            
            ServiceFactory.Get().AddTypes(typeof(ParameterAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(JsonPropertyAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(CustomDbContext).Assembly);
            ServiceFactory.Get().AddTypes(typeof(ServiceDbContext).Assembly);
            ServiceFactory.Get().AddTypes(Assembly.GetExecutingAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetCallingAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetEntryAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetExecutingAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(Assembly.GetCallingAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(Assembly.GetEntryAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly.Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(KeyAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(System.Object).Assembly);
            ServiceFactory.Get().AddTypes(typeof(string).Assembly);
            ServiceFactory.Get().AddTypes(typeof(JsonPropertyAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Newtonsoft.Json.JsonArrayAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(System.Data.SqlClient.SqlClientPermission).Assembly);


            ServiceFactory.Get().GetDbContexts().Select(ctx => ctx.Name).ToJsonOnScreen().WriteToConsole();
        }


        
        public static void Configure(WebApplicationBuilder builder)
        {
            var app = builder.Build();
            app.Use(async(http, next) => { 
                try
                {
                    await next.Invoke();
                }
                catch(Exception ex)
                {
                    foreach(var message in ex.ToMessages())
                    {
                        await http.Response.WriteAsync($"{message}");
                    }
                    
                }
            });
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseRouting();
            app.MapControllers();

            app.UseMiddleware<AppRouterMiddleware>();
            //ApiOdbc.UseOdbc(app);
            ApiCall.UseApi(app);
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.Run();
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddBlazorContextMenu();        
            builder.Services.AddBlazorBootstrap();
            builder.Services.AddBlazoredModal();
            builder.Services.AddTransient(typeof(IServiceCollection), sp => builder.Services);
            
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

            BookingModule.AddBooking(builder.Services, builder.Configuration);
            UserInterfaceModule.AddUserInterfaceServices(builder.Services, builder.Configuration);
            SesionModule.AddSessionService(builder.Services);
            RecaptchaModule.ConfigureServices(builder.Configuration, builder.Services);
            AuthorizationModule.ConfigureServices(builder.Services);
            ModuleUser.ConfigureServices(builder.Configuration, builder.Services);
            ModuleService.ConfigureServices(builder.Configuration, builder.Services);
            BusinessAnaliticsModule.ConfigureServices(builder.Services, builder.Configuration);
            DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);
            //DeliveryDbContext.CreateDeliveryData(builder.Services, builder.Configuration);
            DbContextUserInitializer.CreateUserData(builder.Services, builder.Configuration);
        }


        [Label("Создание структуры баз данных")]
        public static void UpdateDatabases()
        {
            CustomDbContext.Build();
            BookingInitializer.InitData();
            using (var db = new NavMenuDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
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
