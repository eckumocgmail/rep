
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;

public static class HttpContextExtensions
{
    /// <summary>
    /// Вывод тестового сообщения 
    /// </summary>
    public static void ResponseText(this HttpContext context, int status, string text)
    {
        using (var streamWriter = new StreamWriter(context.Response.Body, System.Text.Encoding.UTF8))
        {
            context.Response.ContentType = "text/html";
            context.Response.StatusCode = status;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(text);
            context.Response.Body.Write(data, 0, data.Length);
        }
    }


    /// <summary>
    /// Считывание аргументов выполнения метода API из http контекста
    /// </summary>
    public static Dictionary<string, object> GetArgumentsForAction(this HttpContext http, MethodInfo action)
    {
        var result = new Dictionary<string, object>();
        foreach (var par in action.GetParameters())
        {
            var validations = par.GetParameterAttributes().Where(kv => kv.Key.ToType().IsExtends(typeof(ValidationAttribute)));
            var pair = par.GetParameterAttributes().Where(kv => kv.Key.ToType().IsImplements(typeof(IBindingSourceMetadata)));
            if (pair.Count() > 1)
            {
                throw new System.Exception($"Параметр {par.Name} определяет несколько атрибутов связывания");
            }
            else if (pair.Count() == 1)
            {
                var attr = pair.First().Key.New(pair.First().Value);
                switch (attr.GetTypeName())
                {
                    case nameof(FromQueryAttribute):
                        if (http.Request.Query.ContainsKey(par.Name))
                        {
                            var value = http.Request.Query[par.Name].ToString();
                            result[par.Name] = value;
                        }
                        else
                        {
                            result[par.Name] = null;
                            var required = par.GetParameterAttributes().Select(p => p.Key).FirstOrDefault(kv => kv.ToType().IsExtends(typeof(RequiredAttribute)));
                            if (required is not null)
                            {
                                throw new ArgumentNullException(par.Name, $"Не задано значение параметра {par.Name}");
                            }
                        }
                        break;
                    case nameof(FromRouteAttribute):
                        if (http.Request.RouteValues.ContainsKey(par.Name))
                        {
                            var value = http.Request.RouteValues[par.Name].ToString();
                            result[par.Name] = value;
                        }
                        else
                        {
                            result[par.Name] = null;
                            var required = par.GetParameterAttributes().Select(p => p.Key).FirstOrDefault(kv => kv.ToType().IsExtends(typeof(RequiredAttribute)));
                            if (required is not null)
                            {
                                throw new ArgumentNullException(par.Name, $"Не задано значение параметра {par.Name}");
                            }
                        }
                        break;
                    case nameof(FromBodyAttribute):
                        throw new NotImplementedException(nameof(FromBodyAttribute));
                    case nameof(FromFormAttribute):
                        throw new NotImplementedException(nameof(FromFormAttribute));
                    case nameof(FromServicesAttribute):
                        result[par.Name] = http.RequestServices.GetService(par.ParameterType);
                        break;
                    default:
                        throw new NotSupportedException(par.Name);
                }
            }
            else
            {
                if (http.Request.Query.ContainsKey(par.Name))
                {
                    var value = http.Request.Query[par.Name].ToString();
                    result[par.Name] = value;
                }
                else
                {
                    result[par.Name] = null;
                    var required = par.GetParameterAttributes().Select(p => p.Key).FirstOrDefault(kv => kv.ToType().IsExtends(typeof(RequiredAttribute)));
                    if (required is not null)
                    {
                        throw new ArgumentNullException(par.Name, $"Не задано значение параметра {par.Name}");
                    }
                }
            }
        }
        return result;

    }

    /// <summary>
    /// Вывод сообщения в формате JSON
    /// </summary>
    public static void ResponseJson(this HttpContext context, int status, object target)
    {
        using (var streamWriter = new StreamWriter(context.Response.Body, System.Text.Encoding.UTF8))
        {
            context.Response.ContentType = "text/json";
            context.Response.StatusCode = status;

            byte[] data = System.Text.Encoding.UTF8.GetBytes(target.ToJsonOnScreen());
            context.Response.Body.Write(data, 0, data.Length);
        }
    }
}

