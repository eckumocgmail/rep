using Console_AuthModel.AuthorizationModel;
using Google_LoginApplication.Areas.Identity.Modules.ReCaptcha;
using Microsoft.AspNetCore.Components.Authorization;
using Console_DataConnector.DataModule.DataODBC.Connectors;
using Newtonsoft.Json;
using pickpoint_delivery_service;
using Console_UserInterface.AppUnits.AuthorizationBlazor;
using Microsoft.AspNetCore.Http.Extensions;
using Blazored.Modal.Services;
using System.Reflection;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;
using Console_UserInterface.AppUnits;
using Console_UserInterface.Services;
using Console_UserInterface.Location;
using Console_UserInterface.Shared;

namespace Console_UserInterface
{
    public class Program
    {
        public static class Test
        {
            public static void SessionTest() =>
                new SessionUnit(AppProviderService.GetInstance()).DoTest(false).ToDocument().WriteToConsole();
            public static void TestApp()            
                => new DeliveryServicesUnit().DoTest(true).ToDocument().WriteToConsole();
            
            public static void TestInputField()
            {
                var model = new InputFormModel();
                model.Item = new AttributesInputTest.CollectionModel();
                var field = model.CreateFormField(model.Item.GetType(), nameof(AttributesInputTest.CollectionModel.ListDateModel));
                field.ToJsonOnScreen().WriteToConsole();
            }
        }

        public static void Main(string[] args)
        {
            RegTypes();
            //UpdateDatabases();
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder);
            Configure(builder);
        }

        private static void RegTypes()
        {
            ServiceFactory.Get().AddTypes(typeof(JsonPropertyAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Newtonsoft.Json.JsonArrayAttribute).Assembly);
            ServiceFactory.Get().AddTypes(typeof(System.Data.SqlClient.SqlClientPermission).Assembly);
            ServiceFactory.Get().AddTypes(Assembly.GetExecutingAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetCallingAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetEntryAssembly());
            ServiceFactory.Get().AddTypes(Assembly.GetExecutingAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(Assembly.GetCallingAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(Assembly.GetEntryAssembly().Modules.Select(mod => mod.Assembly));
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly);
            ServiceFactory.Get().AddTypes(typeof(Program).Assembly.Modules.Select(mod => mod.Assembly));
        }

        public static void Configure(WebApplicationBuilder builder)
        {
            var app = builder.Build();
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<AppRouterMiddleware>();
            app.UseRouting();
            app.MapControllers();
            //UseOdbc(app);
            //UseApi(app);
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
            app.Run();
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddBlazorContextMenu();        
            builder.Services.AddBlazorBootstrap();
            
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddHttpContextAccessor();
            
            builder.Services.AddTransient<IconsProvider>();
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

            LocationModule.AddLocationService(builder.Services);
            SesionModule.AddSessionService(builder.Services);
            RecaptchaModule.ConfigureServices(builder.Configuration, builder.Services);
            AuthorizationDbContext.ConfigureServices(builder.Services);
            AuthorizationModule.ConfigureServices(builder.Services);
            ModuleUser.ConfigureServices(builder.Configuration, builder.Services);
            ModuleService.ConfigureServices(builder.Configuration, builder.Services);
            DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);
            //DeliveryDbContext.CreateDeliveryData(builder.Services, builder.Configuration);
            DbContextUserInitializer.CreateUserData(builder.Services, builder.Configuration);
        }


        [Label("�������� ��������� ��� ������")]
        public static void UpdateDatabases()
        {
            using (var db = new AuthorizationDbContext())
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

        public static void UseApi(WebApplication app)
        {
            app.MapWhen(http => http.Request.GetDisplayUrl().IndexOf("/api") != -1, app => {
                app.Run(async http => {
                    var uri = http.Request.GetDisplayUrl().Substring(http.Request.GetDisplayUrl().IndexOf("/api")+3);
                    if(uri.IndexOf("?")!=-1)
                    {
                        uri = uri.Substring(0, uri.IndexOf("?"));
                    }
                    
                    var api = http.RequestServices.Get<SqlServerWebApi>();
                    var args = new Dictionary<string, string>(http.Request.Query.Select(q => new KeyValuePair<string, string>(q.Key, q.Value.First())));
                    var headers = new Dictionary<string, string>(http.Request.Headers.Select(q => new KeyValuePair<string, string>(q.Key, q.Value.First())));
                    var response = await api.Request(uri, args, headers);
                    //http.Response.StatusCode = response.Item1;
                    http.Response.ContentType = "application/json; utf-8";
                    await http.Response.WriteAsync(response.Item2.ToJson());                    
                });
            });
            //app.Use(async (http, next) => { await http.Response.WriteAsync(new { }.ToJsonOnScreen()); await next.Invoke(); });
            app.Use(async (http, next) =>
            {
                if(http.Request.GetDisplayUrl().IndexOf("/api") != -1)
                {
                    
                    /*var signin = http.RequestServices.GetService<SigninUser>();
                    if (signin.IsSignin())
                    {
                        UserContext context = signin.Verify();
                        context.UserAgent = http.Request.Headers.UserAgent;
                        using (var db = new DbContextUser())
                        {
                            db.Update(context);
                            db.SaveChanges();
                        }
                        
                    }*/
                }
                else
                {
                    await next.Invoke();
                }
                
            });
        }

        private static void UseOdbc(WebApplication app)
        {
            var odbc = new OdbcSqlServerDataSource("DSN=" + "ASpbMarketPlace" + ";UId=" + "root" + ";PWD=" + "sgdf1423" + ";");
            odbc.EnsureIsValide();
            var dm = new OdbcDatabaseManager(odbc);

            string html = "";
            var tables = dm.GetTables();
            tables.Select(t => html += $@"<a href=""https://localhost:7243/{t}"">{t}</a>").ToList();
            app.MapGet($"/nav", () => html);
            app.MapGet($"/tables", () => JsonConvert.SerializeObject(tables));
            foreach (var table in tables)
            {
                try
                {

                    var tm = dm.GetTableManager(table);
                    var all = tm.SelectAll();
                    app.MapGet($"/{table}", () => JsonConvert.SerializeObject(all));
                    app.MapPut($"/{table}" + "/{json}", (string json) => tm.Update(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
                    app.MapPost($"/{table}" + "/{json}", (string json) => tm.Create(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
                    app.MapDelete($"/{table}" + "/{id:int}", (int id) => tm.Delete(id));
                    app.MapGet($"/{table}" + "/{id:int}", (int id) => JsonConvert.SerializeObject(tm.Select(id)));


                    //TODO: add foreight table to route
                    app.MapGet($"/{table}" + "/{id:int}" + $"/{"CarsModels"}", (int id) => JsonConvert.SerializeObject(dm.GetTableManager("CarsModels").SelectAll().Where(item => id.ToString() == item.Value<string>("BrandId"))));
                    app.MapPut($"/{table}" + "/{id:int}" + $"/{"CarsModels"}" + "/{json}", (string json) => tm.Update(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
                    app.MapPost($"/{table}" + "/{id:int}" + $"/{"CarsModels"}" + "/{json}", (string json) => tm.Create(JsonConvert.DeserializeObject<Dictionary<string, object>>(json)));
                    app.MapDelete($"/{table}" + "/{id:int}" + $"/{"CarsModels"}" + "/{id:int}", (int id) => tm.Delete(id));
                    app.MapGet($"/{table}" + "/{id:int}" + $"/{"CarsModels"}" + "/{id:int}", (int id) => JsonConvert.SerializeObject(tm.Select(id)));
                }
                catch (Exception)
                {

                }
            }
        }             
    }
}