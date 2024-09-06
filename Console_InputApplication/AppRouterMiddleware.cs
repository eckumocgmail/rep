using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

using Newtonsoft.Json;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

[Label("Маршрутизатор http-запросов")]
[Description("Использует динамически созданные объекты в качестве контроллеров приложения")]
public class AppRouterMiddleware: TypeNode<AppRouterMiddleware>, IMiddleware
{
    /// <summary>
    /// Параметры маршрутизации (маршрут, тип, действие).    
    /// </summary>
    private ConcurrentDictionary<string, string> routing { get; set; } = new()
    {
         
    };

    public AppRouterMiddleware()
    {
        this.AddRoute($"/app/{GetType().GetTypeName()}/{nameof(GetRoutes)}",
            GetType().GetTypeName(),
            nameof(GetRoutes));
    }


    /// <summary>
    /// Регистрация маршрута в обработчике запроса
    /// </summary>    
    public void AddRoute(string route, string controller, string action)
    {
        this.Commit(() => 
        {
            var ctrl = controller.ToType();
            var method = controller.ToType().GetMethod(action);
            routing[route] = $"{controller}.{action}";
            return 1;
        }, new Dictionary<string, object>() 
        {
            { "route", route },
            { "controller", controller },
            { "action", action },
        });        
    }

    public ConcurrentDictionary<string, string> GetRoutes() => 
        new ConcurrentDictionary<string, string>(routing.Select(kv => new KeyValuePair<string, string>(kv.Key, $"{kv.Value}")));

    /// <summary>
    /// 
    /// </summary>
    public async Task InvokeAsync(HttpContext http, RequestDelegate todo)
    {
        await Task.CompletedTask;

        this.Info($"{http.Request.GetDisplayUrl()}");

        try
        {
            var query = http.Request.Path.ToString();
            foreach(var keyValue in routing)
            {
                var route = keyValue.Key;
                if( IsMatches(http, route) )
                {
                    var controller = keyValue.Value.Split(".")[0];
                    var action = keyValue.Value.Split(".")[1];

                    Clear();
                    var arguments = http.GetArgumentsForAction(keyValue.Key, controller.ToType().GetMethod(action));
                    MethodBase.GetCurrentMethod().EnsureIsValid(arguments);
                } 
            }
        }
        catch (Exception ex)
        {
            this.Error($"Ошибка при обработки запроса {http.Request.GetDisplayUrl()} : {ex.Message}\n {ex.StackTrace}");
        }
        await todo.Invoke(http);
    }

    private bool IsMatches(HttpContext http, string route)
    {
        var query = http.Request.GetDisplayUrl();
        while(route is not null && route.IndexOf("{")!=-1)
        {
            int i1 = route.IndexOf("{");
            if (i1 == -1)
            {
                return query == route;
            }
            else
            {
                int i2 = route.IndexOf("}/");                
                var left1 = route.Substring(0, i1);
                var left2 = query.Substring(0, i1);
                if (left1 != left2)
                {
                    return false;
                }

                route = route.Substring(i2 + 1);
                query = query.Substring(i1 + 1 + query.Substring(i1 + 1).IndexOf("/"));
            }
        }
        return true;
    }
}


