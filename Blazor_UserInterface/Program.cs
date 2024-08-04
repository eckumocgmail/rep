using Console_AuthModel.AuthorizationModel;
using Console_AuthModel.AuthorizationServices.Authentication;

using Console_BlazorApp.AppUnits;
using Console_BlazorApp.AppUnits.DeliveryServices;
using Console_BlazorApp.Shared;
using Console_DataConnector.DataModule.DataODBC.Connectors;
using Google_LoginApplication.Areas.Identity.Modules.ReCaptcha;

using MarketerWeb.Authorization;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using pickpoint_delivery_service;

namespace Console_BlazorApp
{
    public class Program
    {
        public static void Main(string[] args)        
        { 
             
            using (var db = new AuthorizationDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
                db.UserContexts_.ToList().ToJsonOnScreen().WriteToConsole();
            }
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
            

            var builder = WebApplication.CreateBuilder(args);
            pickpoint_delivery_service.DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);
            pickpoint_delivery_service.DeliveryDbContext.CreateDeliveryData(builder.Services, builder.Configuration);

            Thread.Sleep(2000);
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<AppRouterMiddleware>();
           
            builder.Services.AddTransient<MailRuService2>();
            builder.Services.AddTransient<InputModalService>();
            builder.Services.AddTransient<IModalService, ModalService>();


            builder.Services.AddScoped<IAuthenticationService, MyAuthenticationService>();
            builder.Services.AddScoped<AuthStateProvider>();
            builder.Services.AddSingleton<AuthenticationStateProvider>(sp => (AuthenticationStateProvider)sp.GetRequiredService<AuthStateProvider>());
            builder.Services.AddAuthenticationCore( options => {
             
            });
            builder.Services.AddAuthorizationCore(config =>
            {
                //todo configure authorization
            });

            builder.Services.AddInputModal();
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            RecaptchaModule.ConfigureServices(builder.Configuration, builder.Services);
            AuthorizationDbContext.ConfigureServices(builder.Services);
            AuthorizationModule.ConfigureServices(builder.Services);
            ModuleUser.ConfigureServices(builder.Configuration, builder.Services);
            ModuleService.ConfigureServices(builder.Configuration, builder.Services);
            DeliveryDbContext.ConfigureDeliveryServices(builder.Services, builder.Configuration);

            var app = builder.Build();
            
            UseOdbc(app);

            DeliveryDbContext.UseDeliveryServices(app);
            app.UseExceptionHandler("/Error");
          
            app.UseStaticFiles();

            app.UseRouting();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        private static void UseOdbc(WebApplication app )
        {
            var odbc = new OdbcSqlServerDataSource("DSN=" + "ASpbMarketPlace" + ";UID=" + "root" + ";PWD=" + "sgdf1423" + ";");
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