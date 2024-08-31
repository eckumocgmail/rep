
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NetCoreConstructorAngular.Data.DataAttributes;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using UserAuthorization.DataAttributes.AttributeDisplay;

public class Attrs: Utils
{

     

    /// <summary>
    /// Возвращает значение атрибута установленного для типа обьекта
    /// </summary>
    /// <param name="target"></param>
    /// <param name="attrName"></param>
    /// <returns></returns>
    public static string GetTypeAttrValue(object target, string attrName)
    {
        var attrs = ForType(((target is Type)? (Type)target: target.GetType()));
        if( attrs.ContainsKey(attrName)==false)
        {
            return null;
        }
        else
        {
            return attrs[attrName];
        }
    }



    public static bool HasInputImage(Type type)
    {
        string prop = GetInputImagePropertyName(type);
        if( prop == null)
        {
            foreach (var nav in GetNavigation(type))
            {
                if(Typing.IsCollectionType(nav.GetType()) == false)
                {
                    prop = GetInputImagePropertyName(nav.GetType());
                    if (prop != null)
                    {
                        break;
                    }
                }
            }
        }
        return prop == null ? false : true;
    }

    public static string GetInputImageUrlExpression()
    {     
        return @"/api/Resource/Image?entity={{GetType().Name}}&id={{Id}}";
    }

    public static string GetInputImageUrl(object target)
    {
        string prop = GetInputImagePropertyName(target.GetType());
        if (prop != null)
        {
            string entity = target.GetType().Name;
            int id = int.Parse(ReflectionService.GetValueFor(target, "Id").ToString());
            return $"/api/Resource/Image?entity={entity}&id={id}";
        }
        else 
        {
            foreach (var nav in GetNavigation(target.GetType()))
            {
                if (Typing.IsCollectionType(nav.GetType()) == false)
                {
                    prop = GetInputImagePropertyName(nav.GetType());
                    if (prop != null)
                    {
                        target = ReflectionService.GetValueFor(target, nav.Name);
                        if(target != null)
                        {
                            string entity = target.GetType().Name;
                            int id = int.Parse(ReflectionService.GetValueFor(target, "Id").ToString());
                            return $"/api/Resource/Image?entity={entity}&id={id}";
                        }
                        else
                        {
                            return $"/api/Resource/Image";
                        }
                        
                    }
                }
            }
        }
        throw new Exception("Не удалось найти изображение");

    }

    public static Dictionary<string, Dictionary<string, string>> GetNavigationProperties(Type type) {
        Dictionary<string, Dictionary<string, string>> result = new Dictionary<string, Dictionary<string, string>>();
        GetNavigation(type).Select(s => s.Name).ToList().ForEach(key=> {
            result[key] = Attrs.ForProperty(type, key);
        });
        return result;
    }

    public static string ForHelp(object target)
    {
        return GetTypeAttrValue(target, nameof(HelpMessageAttribute));
    }

    public static string ForDescription(object target)
    {
        return GetTypeAttrValue(target, nameof(DescriptionAttribute));
    }

    public static INavigation GetNavigationKeyFor(string instance, Type targetEntityType)
    {
        var navs = Attrs.GetNavigation(ReflectionService.TypeForName(instance));
        foreach(var nav in navs)
        {
            if(nav.TargetEntityType.Name == targetEntityType.FullName)
            {
                return nav;
            }
        }
        throw new Exception("Не найдено свойство навигации");
    }

    public static string GetInputImagePropertyName(Type type)
    {
        return GetInputImagePropertyName(ForAllPropertiesInType(type));
    }

    public static string GetInputImagePropertyName(Dictionary<string, Dictionary<string, string>> attrs)
    {
        foreach(var p in attrs)
        {
            if (attrs[p.Key].ContainsKey(nameof(InputImageAttribute)))
            {
                return p.Key;
            }
        }
        return null;
    }

    public static bool IsManyToManyRelation(Type type, string propertyName) {
        return Attrs.ForProperty(type, propertyName).ContainsKey(nameof(ManyToMany));
    }
    public static bool HasManyToManyRelation(Type type, string propertyName) {
        return Attrs.ForProperty(type, propertyName).ContainsKey(nameof(ManyToMany));
    }

    public static Dictionary<string, string> GetEntityContrainsts(Type type)
    {
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        foreach (var data in type.GetCustomAttributesData())
        {
            string key = data.AttributeType.Name;
            foreach (var arg in data.ConstructorArguments)
            {
                string value = arg.Value.ToString();
                attrs[key] = value;
            }

        }
        return attrs;
    }



    public static IEnumerable<INavigation> GetNavigation(Type type)
    {
        IEnumerable<INavigation> result = null;
        var ctx = Assembly.GetExecutingAssembly().Get<DbContext>().Select(ptype => ptype.New<DbContext>()).First();
        using (var _context = ctx)
        {
            result = _context.GetNavigationPropertiesForType(type);
        }
        if (result == null)
        {
            return new List<INavigation>();
        }
        else
        {
            return result;
        }
        throw new Exception();
    }

    public static List<string> GetVisibleProperties(Type type)
    {
        List<string> props = new List<string>();
        foreach (string propertyName in ReflectionService.GetPropertyNames(type))
        {
            if( IsVisible(type,propertyName))
            {
                props.Add(propertyName);
            }
        }
        return props;
    }

    public static object BindingsFor(string entity)
    {
        return BindingsFor(ReflectionService.TypeForName(entity));
        
    }
    public static object BindingsFor(Type type)
    {
        var attrs = ForType(type);
        if(attrs.ContainsKey(nameof(ViewBindingsAttribute)))
        {
            return attrs[nameof(ViewBindingsAttribute)];
        }
        else
        {
            return Expressions.GetDefaultBindingsFor(type.Name);
        }
        

    }
    

    public static string[] GetCollectionTypePropertyNames(Type type, string propName)
    {
        return (from p in new List<PropertyInfo>(GetCollectionTypeProperties(type, propName)) select p.Name).ToArray();
    }
    public static PropertyInfo[] GetCollectionTypeProperties(Type type, string propName)
    {
        return ReflectionService.TypeForShortName(GetCollectionType(type, propName)).GetProperties();
    }
    public static Type GetCollectionSystemType(Type type, string propName)
    {
        return ReflectionService.TypeForShortName(GetCollectionType(type, propName));
    }

    public static bool IsCollectionType(Type type, string propName)
    {
        var property = type.GetProperty(propName);
        string TypeName = property.PropertyType.Name;          
        bool res = property.PropertyType.Name.StartsWith("List");
        if( res == false )
        {
            Type p = property.PropertyType;
            while (p != typeof(Object) && p != null)
            {
                if((from pinterface in new List<Type>(p.GetInterfaces()) where pinterface.Name.StartsWith("ICollection") select p).Count() > 0)
                {
                    return true;
                }
                p = p.BaseType;
            }
        }
        return res;
    }



    public static string GetCollectionType(Type type, string propName)
    {
        var property = type.GetProperty(propName);
        string TypeName = property.PropertyType.Name;        
        if (property.PropertyType.Name.StartsWith("List"))
        {
             
            string text = property.PropertyType.AssemblyQualifiedName;
            text = text.Substring(text.IndexOf("[[") + 2);
            text = text.Substring(0, text.IndexOf(","));
            TypeName = text.Substring(text.LastIndexOf(".") + 1);
            AppContext.BaseDirectory.Info(property.Name + " " + text);
        }
        return TypeName;
    }

    public static bool IsCollection(Type type, string propName)
    {        
        var property = type.GetProperty(propName);
        string TypeName = property.PropertyType.Name;
        bool IsCollection = false;
        if (property.PropertyType.Name.StartsWith("List"))
        {
            IsCollection = true;
            string text = property.PropertyType.AssemblyQualifiedName;
            text = text.Substring(text.IndexOf("[[") + 2);
            text = text.Substring(0, text.IndexOf(","));
            TypeName = text.Substring(text.LastIndexOf(".") + 1);
            AppContext.BaseDirectory.Info(property.Name + " " + text);
        }
        return IsCollection;
    }

/*
    /// <summary>
    /// Подпись элемента визуализации ассоциированного со заданным свойством 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string GetInputType(Type type, string name)
    {
        Dictionary<string, string> attrs = Attrs.ForProperty(type, name);
        return GetInputType(attrs);
    }




    /// <summary>
    /// Получение атрибута типа поля ввода
    /// </summary>
    /// <param name="attrs"></param>
    /// <returns></returns>
    public static string GetInputType(Dictionary<string, string> attrs)
    {
        if(attrs.ContainsKey("Key")|| attrs.ContainsKey("KeyAttribute"))
        {
            return "hidden";
        }
        string key = null;
        List<string> keys = new List<string>(attrs.Keys);
        InputTypeAttribute.GetInputTypes().ForEach((string name) =>
        {
            if (keys.Contains(name))
            {
                key = name;
            }
        });
        if( key != null )
        {
            return key.Replace("Attribute", "").Replace("Input", "");
        }
        else
        {

            return null;
        }
    }

*/






    public static Dictionary<string, string> ForType(string p)
    {

        if (AttrsByType.ContainsKey(p))
        {
            return AttrsByType[p];
        }
        else
        {
            return AttrsByType[p] = ForType(ReflectionService.TypeForName(p));
        }

    }


    public static bool IsTrueValue(string v)
    {
        return v.ToLower() == "true";
    }

    public static string HelpFor(Type type, string property)
    {
        Dictionary<string, string> attrs = ForProperty(type, property);
        return attrs.ContainsKey(nameof(HelpMessageAttribute)) ? attrs[nameof(HelpMessageAttribute)] : "";

    }

   




    private static List<string> CONTROL_TYPES =
        null;// 


    public static List<string> GetControlTypes()
    {
        if(CONTROL_TYPES == null)
        {
            CONTROL_TYPES = Assembly.GetExecutingAssembly().GetTypes().Where(t => Typing.IsExtendedFrom(t, nameof(ControlAttribute))).ToList().Select(t=>t.Name).ToList();
        }
        return CONTROL_TYPES;
    }

    private static object ControlTypeAttribute()
    {
        return null;
    }

    public static string GetControlType(Type type, string property)
    {
        var attrs = Attrs.ForProperty(type, property);
        return (from p in attrs.Keys where GetControlTypes().Contains(p) select p).SingleOrDefault();
    }






    /// <summary>
    /// Получить значения всех атрибутов заданных для свойства
    /// </summary>
    /// <param name="type"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    private static Dictionary<string, Dictionary<string, Dictionary<string, string>>> AttrsForProperty =
        new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
    /*public static Dictionary<string,string> ForProperty(Type type, String property)
    {
        
        if(type == null)
        {
            throw new ArgumentNullException();
        }
        if (AttrsForProperty.ContainsKey(type.FullName) == false)
        {
            AttrsForProperty[type.FullName] = new Dictionary<string, Dictionary<string, string>>();
        }
        if (AttrsForProperty[type.FullName].ContainsKey(property) == true)
        {
            return AttrsForProperty[type.FullName][property];
        }
        else
        {
            AttrsForProperty[type.FullName][property] = new Dictionary<string, string>();
            Dictionary<string, string> attrs = AttrsForProperty[type.FullName][property];
            PropertyInfo info = null;

            try
            {              
                info = type.GetProperty(property);
            }
            catch (AmbiguousMatchException ex)
            {
                AppContext.BaseDirectory.Info(ex.Message);
            }

            if (info == null)
            {
                throw new Exception($"Свойство {property} не найдено в обьекте типа {type.Name}");
            }
            var datas = info.GetCustomAttributesData();
            if (datas != null)
                foreach (var data in datas)
                {

                    string key = data.AttributeType.Name;
                    //ParameterInfo[] pars = data.AttributeType.GetConstructors()[0].GetParameters();
                    if (data.ConstructorArguments == null || data.ConstructorArguments.Count == 0)
                    {
                        attrs[key] = "";
                    }
                    else
                    {
                        foreach (var arg in data.ConstructorArguments)
                        {

                            string value = arg.Value == null ? "" : arg.Value.ToString();
                            attrs[key] = value;
                        }
                    }

                    //model.Attributes[data.AttributeType] = null;

                }

            if (attrs == null)
            {
                throw new AttributeNotParsedException($"Не удалось получить атрибуты свойсва {property} класса {type.Name}");
            }
            return AttrsForProperty[type.FullName][property];
        }
         
    }*/







    /// <summary>
    /// Выбор значения атрибута DataType
    /// </summary>
    /// <param name="attributes"></param>
    /// <returns></returns>
    public static string GetDataType(Dictionary<string, string> attributes)
    {
        foreach (var p in attributes)
        {
            switch (p.Key)
            {
                case "DataTypeAttribute":
                    switch (p.Value)
                    {
                        case "0":   return "custom";
                        case "1":   return "datetime";
                        case "2":   return "date";
                        case "3":   return "time";
                        case "4":   return "duration";
                        case "5":   return "phone";
                        case "6":   return "currency";
                        case "7":   return "text";
                        case "8":   return "html";
                        case "9":   return "textarea";
                        case "10":  return "email";
                        case "11":  return "password";
                        case "12":  return "url";
                        case "13":  return "image";
                        case "14":  return "creditCard";
                        case "15":  return "postalCode";
                        case "16":  return "upload";
                        default: throw new Exception("Неизвестный тип данных");
                    }

            }
        }
        return null;
    }






    public static string ForManyToMany(Type type, string bindingGroup)
    {
        return Attrs.ForProperty(type, bindingGroup)[nameof(ManyToMany)];
    }
}

