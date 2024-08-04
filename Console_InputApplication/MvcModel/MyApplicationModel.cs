using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Console_InputApplication;

/// <summary>
/// Коллекция сетевых сервисов
/// </summary>
//[Label("Модель приложения")]
public class MyApplicationModel 
{
    public Dictionary<string, MyControllerModel> Controllers = new Dictionary<string, MyControllerModel>();
    public MyApplicationModel() { }
    public MyApplicationModel(Assembly ass  )
    {
        var controllers = GetControllers(ass);
        if (controllers == null || controllers.Count == 0)
        {
            Console.WriteLine("Контроллеры не найдены в приложении");
        }    
        foreach (Type controllerType in controllers)
        {
            if (controllerType.IsAbstract) continue;
            var model = CreateModel(controllerType);
            Controllers[controllerType.Name] = model;
        } 
    }

    private List<Type> GetControllers(Assembly assembly)
    {
        return assembly.GetControllers().ToList();
    }

    public static string RoleFor(Type type)
    {
        
        if (ForType(type).ContainsKey("ForRoleAttribute"))
        {
            return ForType(type)["ForRoleAttribute"];
        }
        else
        {
            return null;
        }
    }

    private static IDictionary<string,string> ForType(Type type)
    {
        return type.GetAttributes();
    }

    private string PathForController(Type controllerType)
    {
        string role = RoleFor(controllerType);
        if (role != null)
        {
            return "/" + role + "/" + controllerType.Name.Replace("Controller", "");
        }
        else
        {
            return "/" + controllerType.Name.Replace("Controller", "");
        }
    }
    /// <summary>
    /// <button>ok</button>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<MethodInfo> GetOwnPublicMethods(Type type)
    {
        var parentMethods = type.BaseType != null ?
            type.BaseType.GetMethods().ToList() : new List<MethodInfo>();
        var methods = type.GetMethods().Where(n => parentMethods.Select(m=>m.Name).Contains(n.Name) == false).ToList();
        return methods;
    }
    public MyControllerModel CreateModel(Type controllerType, bool reqursive = false)
    {
        var uri = "/";
        var attrs = ForType(controllerType);
        if (ForType(controllerType).ContainsKey("AreaAttribute")) uri += attrs["AreaAttribute"].ToString() + "/";
        if (attrs.ContainsKey("ForRoleAttribute")) uri += attrs["ForRoleAttribute"].ToString() + "/";
        string path = PathForController(controllerType);
        ToConsole(path);
        MyControllerModel model = new MyControllerModel()
        {
            Name = controllerType.Name.Replace("`1", ""),
            Path = path,
            Actions = new Dictionary<string, MyActionModel>()
        };



        while (controllerType != null)
        {

            var selfMethods = controllerType.GetMethods().Select(m => m.Name);
            var parentMethods = controllerType.BaseType==null? new List<string>():controllerType.BaseType.GetMethods().Select(m => m.Name);
            var methods = selfMethods.Except(parentMethods).ToList();
            foreach (string name in methods)
            {
                Console.WriteLine(name);
                MethodInfo method = controllerType.GetMethods().First(m => name==m.Name);
                if (method.IsPublic && method.Name.StartsWith("get_") == false && method.Name.StartsWith("set_") == false)
                {

                    IDictionary<string, string> attributes = controllerType.GetMethodAttributes( method.Name);
                    Dictionary<string, object> pars = new Dictionary<string, object>();
                    model.Actions[method.Name] = new MyActionModel()
                    {
                        Name = method.Name,
                        Attributes = new Dictionary<string, string>(attributes),
                        Method = ParseHttpMethod(new Dictionary<string, string>(attributes)),
                        Parameters = new Dictionary<string, MyParameterDeclarationModel>(),
                        Path = model.Path + "/" + method.Name
                    };
                    foreach (ParameterInfo par in method.GetParameters())
                    {
                        model.Actions[method.Name].Parameters[par.Name] = new MyParameterDeclarationModel()
                        {
                            Name = par.Name,
                            Type = par.ParameterType.Name,
                            IsOptional = par.IsOptional
                        };
                    }
                }
            }
            if (reqursive == false)
                break;
            controllerType = controllerType.BaseType;
        }
        return model;
    }

    private IDictionary<string, string> ForMethod(Type controllerType, string name)
    {
        return controllerType.GetMethodAttributes(name);
    }

    private string ParseHttpMethod(Dictionary<string, string> attributes)
    {
        return attributes.Keys.Where(k => k.StartsWith("Http")).Select(k => k.Replace("Attribute", "").Replace("Http", "")).FirstOrDefault();
    }

    private void ToConsole(string path) => path.WriteToConsole();

    public void AddAction(MyControllerModel myControllerModel)
    {
        this.Controllers[myControllerModel.Name] = myControllerModel;
    }
}

