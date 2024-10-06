using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Выполняет регистрацию сервисов
/// </summary>
public static class CustomAttributesModule
{

    /// <summary>
    /// Выполняет регистрацию сервисов
    /// </summary>
    public static void AddCustomAttributes( this IServiceCollection services )
    {
        Console.WriteLine("Подключение функции динамической настройки аттрибутов типов");
        services.AddTransient<CustomDataProvider>();
        services.AddTransient<CustomService>();
        services.AddDbContext<CustomDbContext>(options =>
        {
            CustomDbContext.Configure(options);
        });
    }
}