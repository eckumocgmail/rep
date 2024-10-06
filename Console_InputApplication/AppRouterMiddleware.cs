﻿using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Concurrent;

[Label("Маршрутизатор http-запросов")]
[Description(
    "Использует динамически созданные объекты в " +
    "качестве контроллеров приложения")]
public class AppRouterMiddleware: TypeNode<AppRouterMiddleware>, IMiddleware
{
    /// <summary>
    /// Параметры маршрутизации 
    /// </summary>
    public ConcurrentDictionary<string, string> routing { get; set; } = new(){};
   
    public AppRouterMiddleware(   )
    {
    }
    
    public void AddControllerAction( [SelectType]string controller, string action, [InputSelect("Basic,Bearer,KeyCloak,GateKeeper")]string authorization="no", string provider="Контей")
    {
        var route = $"/api/{controller}/{action}";
        var ctrl = controller.ToType();
        object target = null;
        var method = controller.ToType().GetMethod(action);
        if (ctrl is null)
        {
            throw new ArgumentException("controller", $"Не найден тип {controller}");
        }
        if (ctrl is null)
        {
            throw new ArgumentException("controller", $"Не метод {action} в типе {controller}");
        }
        try
        {
            switch(provider)
            {               
                case "Контейнер приложения": target=AppProviderService.GetInstance().GetService(ctrl); break;
                case "Файловая система":
                    var fctrl = new FileController(ctrl.GetTypeName());

                    target= fctrl.ReadText().FromJson(ctrl); break;
                case "Конструктор по-умолчанию": target = ctrl.New(); break;
                default: throw new ArgumentException("provider");
            }
            
        }
        catch(Exception) 
        {
            throw new ArgumentException("controller", $"Не удалось создать объект типа {controller}");
        }
        routing[route] = $"{controller}.{action}";
    }

    public void AddController(string controller)
    {
        var ctrl = controller.ToType();
        foreach (var actionName in ctrl.GetOwnMethodNames())
        {
            AddControllerAction(controller, actionName);
        }
    }

    public object Call(string controller, string action, Dictionary<string, object> arguments)
    {
        try
        {
            var result = controller.New().Call(action, arguments);
            return result;
        }
        catch(Exception ex)
        {
            this.Error(ex.ToDocument());
            throw;
        }

    }


    public string CreateFormFile(string name, InputFormModel model)
    {
        string dir = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Forms");
        if(System.IO.Directory.Exists(dir) == false)
        {
            System.IO.Directory.CreateDirectory(dir);
        }
        for(int i=0; i<100; i++)
        {
            string filename = name;
            if(System.IO.File.Exists(Path.Combine(dir,filename))==false) 
            {
                System.IO.File.WriteAllText(Path.Combine(dir, filename), model.ToJsonOnScreen());
                return filename;
            }
        }
        throw new Exception();
    }


    /// <summary>
    /// 
    /// </summary>
    public async Task InvokeAsync(HttpContext http, RequestDelegate todo)
    {
        await Task.CompletedTask;
        this.Info($"{http.Request.GetDisplayUrl()}");

        try
        {
            bool matched = false;
            var query = http.Request.Path.ToString();
            foreach(var keyValue in routing)
            {
                var route = keyValue.Key;
                if( IsMatches(query, route) )
                {
                    matched = true;

                    if (keyValue.Value.IndexOf(".")==-1)
                        throw new Exception($"Маршрут настроен неверно {keyValue.Key} {keyValue.Value}");
                    var controllerTypeName = keyValue.Value.Split(".")[0];
                    var actionName = keyValue.Value.Split(".")[1];

                    Clear();
                    

                    var controllerType = controllerTypeName.ToType();
                    if (controllerType is null)
                        throw new ArgumentNullException("controllerType", $"Не найден с тип с именем {controllerTypeName}");
                    var controller = http.RequestServices.GetService(controllerType);
                    if (controller is null)
                        throw new ArgumentNullException("controller", $"Не найден объект в контейнере с типом {controllerType}");
                    var action = controllerType.GetMethods().FirstOrDefault(method => method.Name == actionName);
                    if (action is null)
                        throw new ArgumentNullException("action", $"Не найден метод действия {keyValue.Value}");
                    switch(http.Request.Method.ToUpper())
                    {
                        case "GET":
                            {
                                var form = controllerType.GetInputForm(actionName);
                                string filepath = CreateFormFile($"{controllerTypeName}.{actionName}.input", form);                                
                                http.Response.Redirect($"/forms/{controllerTypeName}/{actionName}");                                
                            }
                            break;
                        case "POST":
                            {
                                var argumentsMap = http.GetArgumentsForAction(action);
                                if (argumentsMap is null)
                                    throw new ArgumentNullException("argumentsMap", $"Не получены аргументы вызова {keyValue.Value}");
                                try
                                {
                                    action.EnsureIsValid(argumentsMap);
                                }
                                catch (Exception ex)
                                {
                                    this.Error($"Валидация завершена с ошибкой: {ex}");
                                }
                                object result = null;
                                var arguments = argumentsMap.Values.ToArray();
                                try
                                {
                                    result = action.Invoke(controller, arguments);
                                }
                                catch (Exception ex)
                                {
                                    this.Error($"Ошибка при выполнении метода {controllerTypeName}.{actionName}({arguments.ToJson()}) => {ex.Message}");
                                }
                                http.ResponseJson(200, result);
                            }
                            break;
                        default: throw new NotSupportedException();
                    }

                    break;
                    
                }
                
            }
            if (!matched)
                await todo.Invoke(http);
        }
        catch (Exception ex)
        {
            this.Error($"Ошибка при обработки запроса {http.Request.GetDisplayUrl()} : {ex.Message}\n {ex.StackTrace}");
            await todo.Invoke(http);
        }
        
    }

    private bool IsMatches(string query, string route)
    {        
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
        return query == route;
    }

    public ConcurrentDictionary<string, string> GetRoutes() => routing;


}


