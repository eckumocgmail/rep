using Microsoft.EntityFrameworkCore;

namespace Console_UserInterface.Services.Location
{
    public class LocationModule
    {
        public static void AddLocationService(IServiceCollection services)
        {
            services.AddDbContext<LocationDbContext>(options =>
            options.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={typeof(LocationDbContext).GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
            services.AddTransient<PageService>();
            services.AddTransient<RouteService>();
            services.AddTransient<LocationService>();
        }
    }
}
