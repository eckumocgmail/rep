using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ComponentRegistry
{
    //public static int Counter = StartupApplication.AddSingletonConfiguration<ComponentRegistry>();
    public static List<string> VIEW_COMPONENTS = new List<string>();


    public ComponentRegistry()
    {               
        VIEW_COMPONENTS = GetTypeExtendsFrom(Assembly.GetCallingAssembly(),"ViewComponent").AsQueryable().Select(item=>item.Name).ToList();
    }

    public static void AddViewAssembly(Assembly a)
    {
        VIEW_COMPONENTS = GetTypeExtendsFrom(a, "ViewComponent").AsQueryable().Select(item => item.Name).ToList();
    }

    /// <summary>
    /// Метод поиска типов в сборке, наследованных от типа, заданного именем
    /// </summary>
    /// <param name="assembly"> сборка </param>
    /// <param name="baseType"> имя типа </param>
    /// <returns> множество типов </returns>
    public static IEnumerable<Type> GetTypeExtendsFrom(Assembly assembly, string baseType) {

        HashSet<Type> types = new HashSet<Type>();
        Type typeOfObject = new object().GetType();
        foreach (Type type in assembly.GetTypes())
        {
            //Writing.ToConsole(type.FullName);
            Type p = type.BaseType;
            while (p != typeOfObject && p != null)
            {
                if (p.Name == baseType)
                {
                    types.Add(type);
                    break;
                }
                p = p.BaseType;
            }
        }
        return types;
    }

    public List<string> GetViewComponentTypes()
    {
        return VIEW_COMPONENTS;
    }


    public string FindViewComponentFor(object target)
    {
        //Writing.ToConsole("FindViewComponentFor "+target.GetType().Name+" "+target.GetHashCode());
        if (target.GetType().Name.StartsWith("Highcharts")) return "Highcharts"; 
        List<string> ViewComponentsTypeNames = VIEW_COMPONENTS;
        Type type = target.GetType();
        if (type.Name == "Notifications") return "Notifications";
        HashSet<Type> types = new HashSet<Type>();
        Type typeOfObject = new object().GetType();           
        Type p = type;
        while (p != typeOfObject && p != null)
        {
            if (ViewComponentsTypeNames.Contains(p.Name.Replace("`1","")+"ViewComponent"))
            {
                return p.Name.Replace("`1", "");
            }
            p = p.BaseType;
        }
        throw new Exception(
            "Не удалось определить тип компонента представления для ссылки "+
            target.GetType().Name+"\n"+
            VIEW_COMPONENTS.ToString());
    }
}

 