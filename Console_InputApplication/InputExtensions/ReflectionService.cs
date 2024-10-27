﻿
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Console_InputApplication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public static class O1 {


    
  
    public static List<BaseValidationAttribute> GetPropertyValidations(this Type target, string property)
    {
        var res = new List<BaseValidationAttribute>() { };
        
        target.GetType().GetPropertyAttributes(property).Where(kv => kv.Key.ToType().IsExtendedFrom(nameof(BaseValidationAttribute))).Select(kv =>
            kv.Key.ToType().Create<BaseValidationAttribute>(kv.Value.Select(p => (object)p).ToArray())).ToList().ForEach(res.Add);
        return res;
   
            
    }
    public static List<string> GetInputProperties(this object target)
    {
        var result = new List<string>();
        Type ptype = target is Type? ((Type)target): target.GetType();
        foreach (var property in ptype.GetProperties())
        {
            try
            {
                var attrs = ptype.GetPropertyAttributes(property.Name);
                if(attrs.ContainsKey(nameof(InputHiddenAttribute))==false && attrs.ContainsKey(nameof(InputHiddenAttribute).Replace("Attribute", ""))==false &&
                    attrs.ContainsKey(nameof(NotInputAttribute)) == false && attrs.ContainsKey(nameof(NotInputAttribute).Replace("Attribute", "")) == false)
                {
                    result.Add(property.Name);
                }
            }
            catch(Exception ex)
            {
                target.Error (ex);
            }
        }
        return result;
    }
    public static List<string> GetOwnMethodNames(Type type)
    {
        return (from p in new List<MethodInfo>((type).GetMethods()) where p.DeclaringType == type select p.Name).ToList();
    }
    public static List<string> GetOwnPropertyNames(object type)
    {
        if (type is Type)
            return (from p in new List<PropertyInfo>(((Type)type).GetProperties()) where p.DeclaringType == ((Type)type) select p.Name).ToList();
        else
            return (from p in new List<PropertyInfo>(type.GetType().GetProperties()) where p.DeclaringType == type.GetType() select p.Name).ToList();
    }

 
    



    public static List<string> GetPropertyNames(Type type)
    {
        var list = (from p in new List<PropertyInfo>(type.GetProperties()) select p.Name).ToList();
        list.Reverse();
        return list;
    }
    public static List<string> GetFieldNames(Type type)
    {
        return (from p in new List<FieldInfo>(type.GetFields()) select p.Name).ToList();
    }
}
public class ReflectionService    
{
    private static HashSet<string> PrimitiveTypeNames = Typing.PRIMITIVE_TYPES;
    public static ConcurrentDictionary<string, Type> SHORT_NAME_TYPE_DICTIONARY = new ConcurrentDictionary<string, Type>();
    public static List<string> GetOwnMethodNames(Type type)
    {
        return (from p in new List<MethodInfo>((type).GetMethods()) where p.DeclaringType == type select p.Name).ToList();
    }
    public static List<string> GetOwnPropertyNames(object type)
    {
        if (type is Type)
            return (from p in new List<PropertyInfo>(((Type)type).GetProperties()) where p.DeclaringType == ((Type)type) select p.Name).ToList();
        else
            return (from p in new List<PropertyInfo>(type.GetType().GetProperties()) where p.DeclaringType == type.GetType() select p.Name).ToList();
    }






    public static List<string> GetPropertyNames(Type type)
    {
        var list = (from p in new List<PropertyInfo>(type.GetProperties()) select p.Name).ToList();
        list.Reverse();
        return list;
    }
    
    public static List<System.Reflection.PropertyInfo> GetPropertiesList(Type target)
    {
        return new List<System.Reflection.PropertyInfo>(target.GetProperties());
    }


    public static bool IsPrimitive(Type type)
    {
        return IsPrimitive(Typing.ParsePropertyType(type));
    }

    public static bool IsPrimitive(string typeName)
    {
        return PrimitiveTypeNames.Contains(typeName);
    }

    public static List<string> GetPublicStaticFieldNames(Type type)
    {
        List<string> fieldNames = new List<string>();
        foreach (var field in type.GetFields())
        {
            if (field.IsPublic && field.IsStatic)
            {
                fieldNames.Add(field.Name);
            }
        }
        return fieldNames;
    }

   

    public static object GetOwnProperties(object p, string type = "")
    {
        Dictionary<string, object> options = new Dictionary<string, object>();
        
        GetOwnPropertyNames("ViewOptions").ForEach(n => {
            options[n] = p.GetType().GetProperty(n).GetValue(p);
        });
        return options.ToJson();
    }

    public static object CopyValuesFromDictionary(object searchRequest, Dictionary<string, object> dictionary)
    {
        ReflectionService.GetOwnPropertyNames(searchRequest).ForEach(p => {
            if (dictionary.ContainsKey(p))
                TextDataSetter.SetValue(searchRequest, p, dictionary[p]);

        });
        return searchRequest;
    }

    private static HashSet<string> ObjectMethods = new HashSet<string>() {
            "GetHashCode", "Equals", "ToString", "GetType", "ReferenceEquals" };

    public static List<object> Values(dynamic item, List<string> columns)
    {
        int ctn = 0;
        object[] values = new object[columns.Count()];
        foreach (string col in columns)
        {
            values[ctn++] = new ReflectionService().GetValue(item, col);
        }
        return new List<object>(values);
    }


    /// <summary>
    /// Копирование свойств обьекта
    /// </summary>
    /// <param name="item"></param>
    /// <param name="target"></param>
    public void Copy(object item, object target)
    {
        Type type = target.GetType();
        while (type != null)
        {
            foreach (FieldInfo field in type.GetFields())
            {
                if (field.GetValue(item) !=
                    target.GetType().GetField(field.Name).GetValue(target))
                {
                    object current,
                            prev = target.GetType().GetField(field.Name);
                    target.GetType().GetField(field.Name).SetValue(target, current = field.GetValue(item));
                    object evt = new
                    {
                        prev = prev,
                        current = current

                    };
                }
            }
            type = type.BaseType;
        }
    }

    

    public static void CopyValues(object item, object target)
    {

        foreach (string propertyName in GetPropertyNames(target.GetType()))
        {
            var itemProperty = item.GetType().GetProperty(propertyName);
            if (itemProperty != null)
            {
                object value = itemProperty.GetValue(item);
                target.GetType().GetProperty(propertyName).SetValue(target, value);
            }
        }
        foreach (string fieldName in target.GetType().GetFieldNames())
        {
            var itemField = item.GetType().GetField(fieldName);
            if (itemField != null)
            {
                object value = itemField.GetValue(item);
                target.GetType().GetField(fieldName).SetValue(target, value);
            }
        }
    }



    /// <summary>
    /// Список аргументов вызова метода
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public static List<string> GetArguments(MethodInfo method)
    {
        List<string> args = new List<string>();
        foreach (ParameterInfo pinfo in method.GetParameters())
        {
            args.Add(pinfo.Name);
        }
        return args;
    }

    public static Type TypeForName(string typeName)
    {
        if (typeName == "string") return typeof(String);
        if (typeName == "String") return typeof(String);
        if (typeName == "Int32") return typeof(int);
        if (typeName == "Int64") return typeof(long);
        if (typeName == "Boolean") return typeof(Boolean);
        if (typeName == "DateTime") return typeof(DateTime);
        if (typeName == "Decimal") return typeof(float);
        if (typeName == "String[]") return typeof(String[]);
        if (typeName == "Int32[]") return typeof(int[]);
        if (typeName == "Int64[]") return typeof(long[]);
        if (typeName == "Boolean[]") return typeof(Boolean[]);
        if (typeName == "DateTime[]") return typeof(DateTime[]);
        if (typeName == "Decimal[]") return typeof(float[]);
        if (typeName == "JObject") return typeof(JObject);
        if (typeName == "JArray") return typeof(JArray);
        if (typeName == "JToken") return typeof(JToken);
        if (typeName == "JValue") return typeof(JValue);
        if (typeName.Contains(".") == false)
        {
            return TypeForShortName(typeName);
        }
        Type t = (from p in Assembly.GetExecutingAssembly().GetTypes() where p.FullName == typeName select p).FirstOrDefault();
        if (t == null)
        {
            t = (from p in Assembly.GetCallingAssembly().GetTypes() where p.FullName == typeName select p).FirstOrDefault();
        }
        if (t == null)
        {
            throw new Exception("Не найден тип " + typeName);
        }
        return t;
    }

    public static Type TypeForShortName(string type)
    {
        if (SHORT_NAME_TYPE_DICTIONARY.ContainsKey(type))
        {
            return SHORT_NAME_TYPE_DICTIONARY[type];
        }
        else
        {
            Type t;
            try
            {
                t = (from p in Assembly.GetExecutingAssembly().GetTypes() where p.Name == type select p).FirstOrDefault();
                if (t == null)
                {
                    t = (from p in Assembly.GetCallingAssembly().GetTypes() where p.Name == type select p).FirstOrDefault();
                }
               
            }
            catch (Exception)
            {
                AppProviderService.GetInstance().Info($"Обнаружено несколько типов с именем {type}");
                AppProviderService.GetInstance().Info(Assembly.GetExecutingAssembly().GetTypes().Where(p => p.Name == type).Select(t => t.FullName));
                throw new Exception($"Обнаружено несколько типов с именем {type}");
            }
            if (t == null)
            {
                return ServiceFactory.Get().TypeForName(type);
                //throw new Exception("Не удалось найти тип " + type);
            }
            SHORT_NAME_TYPE_DICTIONARY[type] = t;
            return t;
        }
    }

    public void copyFromDictionary(object model, Dictionary<string, object> dictionaries)
    {
        foreach (var prop in model.GetType().GetProperties())
        {
            if (dictionaries.ContainsKey(prop.Name))
            {
                prop.SetValue(model, dictionaries[prop.Name]);
            }
        }
    }

    public static T Create<T>(string typeName, object[] vs)
    {
        Type type = null;
        if (typeName.Contains("."))
        {
            type = ReflectionService.TypeForName(typeName);
        }
        else
        {
            type = ReflectionService.TypeForShortName(typeName);
        }
        return Create<T>(type, vs);
    }


    public static T Create<T>(Type type, object[] vs)
    {
        ConstructorInfo constructor = (from c in new List<ConstructorInfo>(type.GetConstructors()) where c.GetParameters().Length == vs.Length select c).FirstOrDefault();
        
        return (T)(constructor==null? type.GetConstructors().First().Invoke(new object[type.GetConstructors().First().GetParameters().Count()]):constructor.Invoke(vs));
    }

    /// <summary>
    /// Создание новоги экземпляра класса конструктором по-умолчанию
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static T CreateWithDefaultConstructor<T>(string typeName) where T : class
    {
        Type type = null;
        if (typeName.Contains("."))
        {
            type = ReflectionService.TypeForName(typeName);
        }
        else
        {
            type = ReflectionService.TypeForShortName(typeName);
        }
        return CreateWithDefaultConstructor<T>(type);
    }


    /// <summary>
    /// Создание новоги экземпляра класса конструктором по-умолчанию
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public static T CreateWithDefaultConstructor<T>(Type type) where T : class
    {
        //Api.Utils.Info($"CreateWithDefaultConstructor<{typeof(T).GetTypeName()}>");
        try
        {
            T result = null;
            if (type == null)
                throw new ArgumentNullException($"{typeof(ReflectionService).GetNameOfType()}.CreateWithDefaultConstructor() => Не удалось создать объект, так как аргумент type содержит ссылку на null.");
            ConstructorInfo constructor = GetDefaultConstructor(type);
            if (constructor == null)
            {
                throw new Exception($"Тип {type.Name} не обьявляет контруктор по-умолчанию");
            }
            else
            {
                try
                {
                    
                    result = (T)constructor.Invoke(new object[0]);
                    return result;
                }
                catch (Exception ex)
                {
                    constructor.Error(ex);
                    throw new Exception("Ошибка при создании объекта " + type.GetNameOfType(), ex);
                }


            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Не удалось создать отражение объекта типа {type.GetNameOfType()}", ex);
        }

    }


    /*/// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>s
    /// <returns></returns>
    public List<MessageAttribute > GetProperties( object item )
    {
        List<MessageAttribute > props = new List<MessageAttribute >();
        using (ApplicationDbContext db = new ApplicationDbContext())
        {             
            foreach (var prop in db.GetEntityProperties(item.GetType()))
            {                 
                string type = null;
                object val = new ReflectionService().GetValue(item, prop.Name);

                if(val!=null)
                switch (val.GetType().Name.ToLower())
                {
                    case "bool":
                    case "boolean":
                        type = "checkbox";
                        break;
                    case "string":
                    case "text":
                        type = "text";
                        break;
                    case "int":
                    case "float":
                    case "double":
                    case "decimal":
                    case "int32":
                    case "int64":
                        type = "text";
                        break;
                    case "date":
                        type = "date";
                        break;
                    case "datetime":
                        type = "datetime";
                        break;
                }

                var attributes = Utils.GetForProperty(item.GetType(), prop.Name);
                var dataType = Utils.GetDataType(attributes);
                if (dataType != null)
                {
                    type = dataType.ToString().ToLower();
                }
                    
                props.Add(new MessageAttribute (attributes)
                {
                 
                    Label = db.GetDisplayName(item.GetType(), prop.Name),
                    Name = prop.Name,
                    Value = new ReflectionService().GetValue(item, prop.Name),
                    State = "valid",
                    Type = type
                });
                 
            }
            return props;
        }
    }*/


    /// <summary>
    /// Копирование свойств обьекта
    /// </summary>
    /// <param name="item"></param>
    /// <param name="target"></param>
    public void copy(object item, object target)
    {
        foreach (FieldInfo field in target.GetType().GetFields())
        {
            if (field.GetValue(item) !=
                target.GetType().GetField(field.Name).GetValue(target))
            {
                object current,
                        prev = target.GetType().GetField(field.Name);
                target.GetType().GetField(field.Name).SetValue(target, current = field.GetValue(item));
                object evt = new
                {
                    prev = prev,
                    current = current

                };
            }
        }
        foreach (PropertyInfo field in target.GetType().GetProperties())
        {
            if (field.GetValue(item) !=
                target.GetType().GetProperty(field.Name).GetValue(target))
            {
                object current,
                        prev = target.GetType().GetField(field.Name);
                target.GetType().GetProperty(field.Name).SetValue(target, current = field.GetValue(item));
                object evt = new
                {
                    prev = prev,
                    current = current

                };
            }
        }
    }

    public static object GetValueFor(object i, string v)
    {

        try
        {
            if (i is IDictionary<string, object>)
            {
                var dictionary = ((IDictionary<string, object>)i);
                return dictionary.ContainsKey(v) ? dictionary[v] : null;
            }
            if (i is IDictionary<string, string>)
            {
                var dictionary = ((IDictionary<string, string>)i);
                return dictionary.ContainsKey(v) ? dictionary[v] : null;
            }

            PropertyInfo propertyInfo = i.GetType().GetProperty(v);
            FieldInfo fieldInfo = i.GetType().GetField(v);
            return
                fieldInfo != null ? fieldInfo.GetValue(i) :
                propertyInfo != null ? propertyInfo.GetValue(i) :
                null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при получении значения свойства {v} из объекта типа {i.GetType().Name} ", ex);
        }
    }

    public object GetValue(object i, string v)
    {
        try
        {
            PropertyInfo propertyInfo = i.GetType().GetProperty(v);
            FieldInfo fieldInfo = i.GetType().GetField(v);
            return
                fieldInfo != null ? fieldInfo.GetValue(i) :
                propertyInfo != null ? propertyInfo.GetValue(i) :
                null;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при получении значения свойства {v} из объекта типа {i.GetType().Name} ", ex);
        }
    }

    public static Dictionary<string, object> GetSkeleton(object api)
    {
        return GetSkeleton(api, new List<string>());
    }

    /**
     * Метод получения семантики public-методов обьекта
     */
    public static Dictionary<string, object> GetSkeleton(object subject, List<string> path)
    {

        Dictionary<string, object> actionMetadata = new Dictionary<string, object>();
        if (subject == null || subject.GetType().IsPrimitive || PrimitiveTypeNames.Contains(subject.GetType().Name))
        {
            return actionMetadata;
        }
        else
        {
            if (subject is Dictionary<string, object>)
            {
                foreach (var kv in ((Dictionary<string, object>)subject))
                {
                    actionMetadata[kv.Key] = kv.Value;
                    if (!kv.Value.GetType().IsPrimitive && !PrimitiveTypeNames.Contains(kv.Value.GetType().Name))
                    {

                        List<string> childPath = new List<string>(path);
                        childPath.Add(kv.Key);
                        actionMetadata[kv.Key] = GetSkeleton(kv.Value, childPath);
                    }
                };
            }
            else
            {
                //Debug.WriteLine(JObject.FromObject(subject));
                Type type = subject.GetType();
                //Debug.WriteLine(type.Name, path);
                foreach (MethodInfo info in type.GetMethods())
                {
                    if (info.IsPublic && !ObjectMethods.Contains(info.Name))
                    {
                        Dictionary<string, object> args = new Dictionary<string, object>();
                        foreach (ParameterInfo pinfo in info.GetParameters())
                        {
                            args[pinfo.Name] = new
                            {
                                type = pinfo.ParameterType.Name,
                                optional = pinfo.IsOptional,
                                name = pinfo.Name
                            };
                        }
                        List<string> actionPath = new List<string>(path);
                        actionPath.Add(info.Name);
                        actionMetadata[info.Name] = new
                        {
                            type = "method",
                            path = actionPath,
                            args = args
                        };
                    }
                }
                foreach (FieldInfo info in type.GetFields())
                {
                    if (info.IsPublic)
                    {
                        if (!info.GetType().IsPrimitive && !PrimitiveTypeNames.Contains(info.GetType().Name))
                        {
                            List<string> childPath = new List<string>(path);
                            childPath.Add(info.Name);
                            actionMetadata[info.Name] = GetSkeleton(info.GetValue(subject), childPath);
                        }
                    }
                }
            }
        }

        return actionMetadata;
    }


    public static ConstructorInfo GetDefaultConstructor(Type type)
    {
        //возвращаем null, чтобы исключение привело куда надо
        if (type == null)
            return null;
        return (from c in new List<ConstructorInfo>(type.GetConstructors()) where c.GetParameters().Length == 0 select c).FirstOrDefault();
    }


    public Dictionary<string, object> GetStaticMethods(Type type)
    {
        Dictionary<string, object> actionMetadata = new Dictionary<string, object>();
        foreach (MethodInfo info in type.GetMethods())
        {
            if (info.IsPublic && info.IsStatic)
            {
                Dictionary<string, object> args = new Dictionary<string, object>();
                foreach (ParameterInfo pinfo in info.GetParameters())
                {
                    args[pinfo.Name] = new
                    {
                        type = pinfo.ParameterType.Name,
                        optional = pinfo.IsOptional,
                        name = pinfo.Name
                    };
                }
            }
        }
        return actionMetadata;
    }

    /// <summary>
    /// <button>ok</button>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<string> GetOwnPublicMethodsNames(Type type)
    {
        return (from m in new List<MethodInfo>(type.GetMethods())
                where m.IsPublic &&
                        !m.IsStatic &&
                        m.DeclaringType.FullName == type.FullName
                select m.Name).ToList<string>();
    }


    /// <summary>
    /// <button>ok</button>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<MethodInfo> GetOwnPublicMethods(Type type)
    {
        return (from m in new List<MethodInfo>(type.GetMethods())
                where m.IsPublic &&
                        !m.IsStatic &&
                        m.DeclaringType.FullName == type.FullName
                select m).ToList<MethodInfo>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public Dictionary<string, object> GetMethodParameters(MethodInfo method)
    {
        Dictionary<string, object> args = new Dictionary<string, object>();
        foreach (ParameterInfo pinfo in method.GetParameters())
        {
            args[pinfo.Name] = new
            {
                type = pinfo.ParameterType.Name,
                optional = pinfo.IsOptional,
                name = pinfo.Name
            };
        }
        return args;
    }


    public static object Invoke(MethodInfo method, object target, JObject args)
    {
        Dictionary<string, string> pars = JsonConvert.DeserializeObject<Dictionary<string, string>>(args.ToString());
        return Execute(method, target, pars);
    }

    public static object Execute(MethodInfo method, object target, Dictionary<string, string> pars)
    {
        string state = "Поиск обьекта: ";
        List<object> invArgs = null;
        try
        {
            invArgs = new List<object>();
            var attrs = method.DeclaringType.GetArgumentsAttributes(method.Name);
            
            foreach (ParameterInfo pinfo in method.GetParameters())
            {
                if (pinfo.IsOptional == false && pars.ContainsKey(pinfo.Name) == false)
                {
                    throw new Exception("require argument " + pinfo.Name);
                }
                string parameterName = pinfo.ParameterType.Name;
                if( !attrs.ContainsKey(parameterName) )
                {
                    throw new Exception("Не получены атрибуты для параметры " + parameterName);
                }
                object p = null;
                var parameters = attrs[parameterName];
                if (parameterName.StartsWith("Dictionary"))
                {
                    Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(pars[pinfo.Name].ToString());
                    invArgs.Add(p = dictionary);
                }
                else
                {
                    invArgs.Add(p = pars[pinfo.Name]);
                }
                attrs.Validate(parameterName, p, parameters);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("ArgumentsException: " + ex.Message, ex);
        }


        try
        {

            object result = method.Invoke(target, invArgs.ToArray());
            state = state.Substring(0, state.Length - 7) + "успех;";
            return result;
        }
        catch (Exception ex)
        {
            AppProviderService.GetInstance().Info("Error in controller function: " + ex.Message);
            throw;
        }
    }




    /// <summary>
    /// Поиск метода 
    /// </summary>
    /// <param name="subject"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Dictionary<string, Object> Find(object subject, string path)
    {
        object p = subject;
        string[] ids = path.Split('.');
        for (int i = 0; i < (ids.Length - 1); i++)
        {
            string id = ids[i];
            if (p is Dictionary<string, object>)
            {
                p = ((Dictionary<string, object>)p)[id];
            }
            else if (p is ConcurrentDictionary<string, object>)
            {
                p = ((ConcurrentDictionary<string, object>)p)[id];
            }
            else
            {
                p = p.GetType().GetField(id).GetValue(p);
            }
        }

        MethodInfo info = null;
        string methodName = ids[ids.Length - 1];

        foreach (var method in p.GetType().GetMethods())
        {
            if (String.Equals(methodName, method.Name))
            {
                info = method;
                break;
            }
        }
        Dictionary<string, Object> res = new Dictionary<string, Object>();
        res["method"] = info;
        res["target"] = p;
        res["path"] = path;


        return res;
    }





    public string GetMethodParametersBlock(MethodInfo method)
    {
        string s = "{";
        bool needTrim = false;
        foreach (var pair in GetMethodParameters(method))
        {
            needTrim = true;
            s += pair.Key + ':' + pair.Key + ",";
        }
        if (needTrim == true)
            return s.Substring(0, s.Length - 1) + "}";
        else
        {
            return s + "}";
        }
    }


    public string GetMethodParametersString(MethodInfo method)
    {
        bool needTrim = false;
        string s = "";
        foreach (var p in GetMethodParameters(method))
        {
            needTrim = true;
            s += p.Key + ",";// +":"+ p.Value + ",";
        }
        return needTrim == true ? s.Substring(0, s.Length - 1) : s;
    }

 


}