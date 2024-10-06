using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Расширения для настройки типов
/// Методы получения,добавления,удаления аттрибутов
/// </summary>
public static class TypeCustomizationExtensions
{

    /// <summary>
    /// Воостановление значений аттрибутов к значениям по умолчанию
    /// </summary>
    public static void @Restore_(this Type type)
    {
        using (var dp = new CustomService())
        {
            dp.ToType(type);
        }
    }

    /// <summary>
    /// Получение аттрибутов
    /// </summary>
    public static Dictionary<string, string> GetAttributes(this Type type)
    {
        using (var dp = new CustomService())
        {
            return dp.GetAttributes(type);
        }        
    }

    /// <summary>
    /// Получение аттрибутов
    /// </summary>
    public static Dictionary<string, Dictionary<string, string>> @GetMembersAttributes_(this Type type)
    {
        using (var dp = new CustomService())
        {
            return dp.GetMembersAttributes(type.Name);
        }
    }

    /// <summary>
    /// Получение аттрибутов
    /// </summary>
    public static Dictionary<string, string> GetMethodAttributes(this Type type, string method)
    {
        using (var dp = new CustomService())
        {
            return dp.GetMethodAttributes(type.Name, method);
        }
    }

    /// <summary>
    /// Получение аттрибутов
    /// </summary>
    public static Dictionary<string, string> @GetParameterAttributes_(this Type type, string method, string parameter)
    {
        using (var dp = new CustomService())
        {
            return dp.GetParameterAttributes(type.Name, method, parameter);
        }
    }

    /// <summary>
    /// Получение аттрибутов
    /// </summary>
    public static Dictionary<string, string> @GetPropertyAttributes(this Type type, string property)
    {
        using (var dp = new CustomService())
        {
            return dp.GetPropertyAttributes(type.Name, property);
        }
    }

    /// <summary>
    /// Добавление динамического аттрибута
    /// </summary>
    public static void AddAttribute(this Type ptype, string attrType, string attrValue)
    {
        using (var db = new CustomService())
        {
            db.AddAttribute(ptype, attrType, attrValue);
        }
    }


    /// <summary>
    /// Добавление динамического аттрибута
    /// </summary>
    public static void @AddPropertyAttribute(this Type ptype, string property, string attrType, string attrValue)
    {
        using (var db = new CustomService())
        {
            db.AddAttribute($"{ptype}.{property}", attrType, attrValue);
        }
    }

    /// <summary>
    /// Добавление динамического аттрибута
    /// </summary>
    public static void @AddMethodAttribute(this Type ptype, string method, string attrType, string attrValue)
    {
        using (var db = new CustomService())
        {
            db.AddAttribute($"{ptype}.{method}", attrType, attrValue);
        }
    }


    /// <summary>
    /// Удаление динамического аттрибута
    /// </summary>
    public static void @RemoveAttribute(this Type ptype, string attrType)
    {
        using (var db = new CustomService())
        {
            db.RemoveAttribute(ptype, attrType);
        }
    }

    /// <summary>
    /// Удаление динамического аттрибута
    /// </summary>
    public static void @RemovePropertyAttribute(this Type ptype, string property, string attrType)
    {
        using (var db = new CustomService())
        {
            db.RemoveAttribute($"{ptype}.{property}", attrType);
        }
    }

    /// <summary>
    /// Удаление динамического аттрибута
    /// </summary>
    public static void @RemoveMethodAttribute(this Type ptype, string method, string attrType)
    {
        using (var db = new CustomService())
        {
            db.RemoveAttribute($"{ptype}.{method}", attrType);
        }
    }

    /// <summary>
    /// Добавление динамического аттрибута
    /// </summary>
    public static void @AddParameterAttribute(this Type ptype, string method, string parameter, string attrType, string attrValue)
    {
        using (var db = new CustomService())
        {
            db.AddAttribute($"{ptype}.{method}.{parameter}", attrType, attrValue);
        }
    }

    

}
 