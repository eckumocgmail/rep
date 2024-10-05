using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class CustomAttributesModule
{
    public static void AddCustomAttributes( this IServiceCollection services )
    {        
        services.AddTransient<CustomDataProvider>();
        services.AddTransient<CustomService>();
        services.AddDbContext<CustomDbContext>(options =>
        {
            CustomDbContext.Configure(options);
        });
    }
}