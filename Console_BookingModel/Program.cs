using Console_Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BookingModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //BookingInitializer.InitData();
            //BookingUnit.RunTest();
           
            var builder = WebApplication.CreateBuilder(args);
         

            builder.Services.AddSwaggerGen(ConfigureSwagger);    
            builder.Services.AddOpenApi2(builder.Configuration);    
            builder.Services.AddBooking(builder.Configuration);    
            builder.Services.AddControllers();                
            builder.Services.AddSingleton<WeatherForecastService>();
            var app = builder.Build();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(ConfigureSwaggerUI);
            app.MapControllers();
            var routes = new List<string>() { 
                "/api/services"
            };
            app.MapGet( "/api/", async http => {
                http.Response.StatusCode = 200;
                await http.Response.WriteAsync(new {
                    options = routes
                }.ToJsonOnScreen());
                await Task.CompletedTask;
            });
            app.MapGet("/", http => {
                return Task.CompletedTask;
            });
            app.Run();            
        }

        private static void ConfigureSwaggerUI(SwaggerUIOptions options)
        {
        }

        private static void ConfigureSwagger(SwaggerGenOptions options)
        {
        }
    }
}