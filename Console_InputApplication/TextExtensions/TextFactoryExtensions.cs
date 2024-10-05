using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Расширения к строкового типа
/// </summary>
public static class TextFactoryExtensions
{

    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static void ForEach(this object target, Func<string, object, bool> todo)
    {
        target.GetType().GetProperties().ToList().ForEach(p => todo(  p.Name, target.GetValue(p.Name)  )  );
    }
    public static Dictionary<string, string> ToTextDictionary(this object target)
    {
        var result = new Dictionary<string, string>();
        if(target.IsExtends(typeof(Dictionary<string,object>)))
        {
            Dictionary<string, object> dict = (Dictionary<string, object>)target;
            foreach(var p in dict.Select(kv => new KeyValuePair<string, string>(kv.Key, kv.Value?.ToString())))
            {
                result[p.Key] = p.Value;
            }
            return result;
        }
        else
        {
            target.ForEach((k, v) => String.IsNullOrEmpty(result[k] = v.ToString()));
            return result;
        }
        
    }

    public static string ParseSubstring(this string text, string start, string end)
    {
        if (text == null)
            throw new ArgumentNullException("text");
        if (text.IndexOf(start) != -1)
        {
            var substr = text.Substring(text.IndexOf(start) + start.Length, text.IndexOf(end) - text.IndexOf(start) - start.Length);
            return substr;
        }
        throw new ArgumentException($"Строка: {text} не содержит подстроки {start}");
    }

    public static Type ToType(this string text)
    {                      
        if(text == null)
            throw new ArgumentNullException("text");
        if (text.IndexOf("<") != -1)
        {
            var genericArgs = text.Substring(text.IndexOf("<")+1, text.IndexOf(">")- text.IndexOf("<")-1).Split(",");
            
            var mainType = text.Substring(0, text.IndexOf("<"));

            mainType += $"`{genericArgs.Count()}";
            if(genericArgs.All(a => a.IsType()))
            {
                var genericTypes = genericArgs.Select(parg => parg.ToType()).ToArray();
                var result = mainType.ToType().MakeGenericType(genericTypes);
                return result;
            }
            else
            {
                return mainType.ToType();
            }
        }
        Type ptype = text.Contains(".")? ReflectionService.TypeForName(text): ReflectionService.TypeForShortName(text);
        
        if(ptype == null)
        {
            ptype = text.Contains(".") ? ServiceFactory.Get().TypeForName(text) : ServiceFactory.Get().TypeForShortName(text);  
        }
        if(ptype is null)
        {
            throw new ArgumentException($"Не удалось найти тип: {text} исп. ReflectionService/");
        }
        return ptype;
    }
    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static T New<T>(this string text)
    {
        if (text == null)
            throw new ArgumentNullException("text");
        return (T)ReflectionService.CreateWithDefaultConstructor<object>(text.ToType());
    }
    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object New(this string text)
    {
        try
        {
            if (text == null)
                throw new ArgumentNullException("text");
            var ptype = text.ToType();
            return ReflectionService.CreateWithDefaultConstructor<object>(ptype);
        }
        catch(Exception ex) 
        {
            text.Error("Не удалось создать экземпляр типа "+text+" "+ex.Message);
            throw;
        }
    }


    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object New(this Type type)
    {
        if (type == null)
            throw new ArgumentNullException("type");
        
        return ReflectionService.CreateWithDefaultConstructor<object>(type);
    }

    public static T New<T>(this Type type) where T: class
    {
        if (type == null)
            throw new ArgumentNullException("type");

        return ReflectionService.CreateWithDefaultConstructor<T>(type);
    }


    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object New(this Type type, object[] args)
    {
        if (type == null)
            throw new ArgumentNullException("type");
        return ReflectionService.Create<object>(type, args);
    }


    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object Copy(this object type, IDictionary<string,object> keyValues)
    {
        var names = type.GetType().GetProperties().Select(p => p.Name);
        foreach (var kv in keyValues)
        {
            if (names.Contains(kv.Key))
            {
                Setter.SetValue(type,kv.Key, kv.Value); 

            }
        }
        return type;
    }

    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object CopyObject(this object type, object keyValues)
    {
        var names = type.GetType().GetProperties().Select(p => p.Name);
        foreach (var kv in keyValues.ToJson().FromJson<Dictionary<string,object>>())        {
            if (names.Contains(kv.Key))
            {
                try
                {
                    if (Typing.IsCollectionType(type.GetType().GetProperty(kv.Key).PropertyType) == false)
                        Setter.SetValue(type, kv.Key, kv.Value);
                }
                catch(Exception ex)
                {
                    AppProviderService.GetInstance().Info($"Исключение при копироавнии данных в поле с ключом {kv.Key} => {ex.Message}");
                }

            }
        }
        return type;
    }

    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object Create(this Type type, object[] parameters)
    {
        return ReflectionService.Create<object>(type, parameters);
    }

    public static object Call(this object p, string action,  Dictionary<string,object> parameters)
    {
        object ActionResult = null;
        Type type = p.GetType();
        var method = type.GetMethods().Where(m => m.Name == action).FirstOrDefault();        
        try
        {
            ActionResult = method.Invoke(p, parameters.Values.ToArray());
        }
        catch (Exception ex)
        {
            throw new Exception($"Исключение проброшено из метода {TypeExtensions2.GetTypeName(type)}.{action}", ex);
        }
        return ActionResult;
    }
    public static T Create<T>(this Type type, object[] parameters)
    {
        return (T)ReflectionService.Create<T>(type, parameters);
    }
    public static T Create<T>(this Type type)
    {
        return ReflectionService.Create<T>(type, new object[0]);
    }
    public static T Invoke<T>(this Type type, string action, Dictionary<string,string> args)
    {
        return ReflectionService.Create<T>(type, new object[0]);
    }

    /// <summary>
    /// Поиск типа по имени
    /// </summary>
    public static object New(this string text, string deps)
    {
        var constructor = text.ToType().GetConstructors()[0];
        return constructor.Invoke(new object[] { deps });
    }

}