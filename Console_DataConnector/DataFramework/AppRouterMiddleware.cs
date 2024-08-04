using Console_DataConnector.DataModule.DataODBC.Connectors;

using Google.Protobuf.WellKnownTypes;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




[Label("Маршрутизатор http-запросов")]
[Description("Использует динамически созданные объекты в качестве контроллеров приложения")]
public class AppRouterMiddleware: TypeNode<AppRouterMiddleware>, IMiddleware
{
     
    public async Task OnHttpRequest(HttpContext http)
    {
        await Task.CompletedTask;
        var manager = new OdbcDriverManager();
        var odbc = new OdbcDataSource("ASpbMarketPlace", "root", "sgdf1423");
        odbc.EnsureIsValide();
        var dm = new OdbcDatabaseManager(odbc);
        var tables = dm.GetTables();
         
        foreach (var next in http.Request.Path.ToString().Substring(1).Split("/"))
        {
            if (tables.Contains(next) == false)
            {
                var str = JsonConvert.SerializeObject(new
                {
                    message = $" Параметр маршрута {next} не поддерживается, исп. {tables.ToJson()}"
                });
                byte[] bytes = Encoding.ASCII.GetBytes(str + "\r\n\r\n");
                await http.Response.Body.WriteAsync(bytes);
                
                
                http.Response.StatusCode = 200;
            }
        }
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        this.Info(context.Request.Path);
        using (var streamWriter = new StreamWriter(context.Response.Body, System.Text.Encoding.UTF8))
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes("hello");
            context.Response.Body.Write(data, 0, data.Length);
        }
        await OnHttpRequest(context);
        ///return next.Invoke(context);
    }
}


