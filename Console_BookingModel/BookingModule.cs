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
}