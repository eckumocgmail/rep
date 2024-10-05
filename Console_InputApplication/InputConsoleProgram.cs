using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Console_InputApplication 
{
    public class Program
    {

        /// <summary>
        /// Конфигурация выполнения в Http
        /// </summary>
        public class HttpStartup
        {
            private readonly IConfiguration configuration;

            public HttpStartup(IConfiguration configuration)
            {
                this.configuration = configuration;
            }

            public void Configure(IApplicationBuilder app)
            {
                app.UseExceptionHandler();
                app.UseMiddleware<AppRouterMiddleware>();
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToController("PageNotFound","MainController");
                });
            }

            public void ConfigureServices(IServiceCollection services)
            {
                services.AddExceptionHandler(options => {
                    options.AllowStatusCode404Response = true;
                });
                services.AddControllersWithViews();
            }
        }

        /// <summary>
        /// Выполнение программы
        /// </summary>       
        public static void Main(string[] args)
        {            
            args.Info(Assembly.GetExecutingAssembly().GetName().Name);
            ServiceFactory.Get().AddTypes(typeof(Console_InputApplication.Program).Assembly);            

            if (args.Contains("http"))
            {
                Host.CreateDefaultBuilder()
                    .ConfigureWebHostDefaults(webBuilder =>
                        webBuilder.UseStartup<Program.HttpStartup>())
                    .Build().Run();
            }            
        }


        /*
        private static string SelectClass(string cs, string className)
        {

            string text = "";
            int n = 1;
            this.Info(cs);
            int i = cs.IndexOf("class " + className);
            if (i == -1)
                throw new ArgumentException(cs, className);
            while (cs[i] != '{')
            {
                Console.Clear();
                this.Info(cs.Substring(i));
                ++i;
            }
            ++i;
            while (n > 0 || cs.Length > i)
            {

                Console.Clear();
                Debug.WriteLine("N=" + n);

                //Debug.WriteLine(cs.Substring(i));
                text += cs[i];
                if (cs[i] == '{') n++;
                if (cs[i] == '}') n--;
                ++i;
            }
            return text;

        }

        public static string GetMethodCsCode(string cs, string className, string action)
        {
            try
            {
                InputConsole.Info(cs);

                string inner = SelectClass(cs, className);

                InputConsole.Clear();
                InputConsole.Info(inner);
                string code = SelectClass(inner, action);

                InputConsole.Clear();
                InputConsole.Info($"Метод {action} класса {className}");
                InputConsole.Info(code);
                return code;
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось прочитать метод {action} класса {className}", ex);
            }

        }
        */
    }
}
