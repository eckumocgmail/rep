using BookingModel.ServiceServiceModel;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
public static class BookingModule
{
    public static void AddBooking(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ServiceDbContext>(ServiceDbContext.Configure);
        services.AddScoped<ServiceController>( );
        
    }
    public static void AddOpenApi2(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    //ExcelOleDataSource.ReadFile(@"D:\System-Config\MyExpirience\Console_BlazorApp\Console_BookingModel\Resources\works.xlsx").ToJsonOnScreen().WriteToConsole();
        
}