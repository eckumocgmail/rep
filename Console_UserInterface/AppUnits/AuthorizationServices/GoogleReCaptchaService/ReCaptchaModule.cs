using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_LoginApplication.Areas.Identity.Modules.ReCaptcha
{
    public class RecaptchaModule
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            InputConsole.GetLogger<RecaptchaModule>().Info("Регистрация служб ... ");
            services.AddSingleton(typeof(ReCaptchaOptions), sp =>
            {
                var options = configuration.GetSection(nameof(ReCaptchaOptions)).Get<ReCaptchaOptions>();
                return options == null ? new ReCaptchaOptions() : options;
            });
            services.AddScoped<ReCaptchaService>();
            InputConsole.GetLogger<RecaptchaModule>().Info("Регистрация служб ... Успешно завершена");
        }
    }
}
