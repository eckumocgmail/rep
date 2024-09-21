using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class ModuleService
{
    public static void ConfigureServices(IConfiguration configure, IServiceCollection services)
    {        
        var segment = new object();
        lock (segment)
        {             
            InputConsole.GetLogger<ModuleService>().LogInformation("Регистрация служб ... ");
            services.AddDbContext<DbContextService>(ConfigureDbContext);
            if (services.Any(descr => descr.ServiceType == typeof(AuthorizationOptions)) == false)
                services.AddSingleton<AuthorizationOptions>();
            services.AddSingleton<AuthorizationServices>();
            services.AddTransient(typeof(APIActiveCollection<ServiceContext>), sp => sp.GetRequiredService<AuthorizationServices>());
            services.AddTransient(typeof(APIServices), sp => sp.GetRequiredService<AuthorizationServices>());
            services.AddTransient<SigninService>();
            services.AddTransient<SignupService>();
            InputConsole.GetLogger<ModuleService>().LogInformation("Регистрация служб ... Успешно завершена");
        }        
    }

    private static void ConfigureDbContext(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={typeof(DbContextService).Name};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False" );
    }
}