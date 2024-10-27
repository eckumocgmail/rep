using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;
using System.Security.Cryptography.Xml;

namespace CustomAttributes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Assembly.GetCallingAssembly().GetName().Name);

            CustomAttributesUnit.Test();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCustomAttributes();

            var app = builder.Build();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.MapControllers();
            app.Run();
        }
    } 
}
