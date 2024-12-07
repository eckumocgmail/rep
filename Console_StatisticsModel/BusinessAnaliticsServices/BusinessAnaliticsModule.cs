using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class BusinessAnaliticsModule
{
    public static void ConfigureServices( IServiceCollection services, IConfiguration configuration )
    {
        services.AddDbContext<BusinessDataModel>(BusinessDataModel.Configure);
        services.AddTransient<BusinessAnaliticsService>();
    }
}
