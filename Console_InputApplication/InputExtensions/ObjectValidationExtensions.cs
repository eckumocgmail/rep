using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System;
using Console_InputApplication;

using static InputConsole;


public static class ObjectValidationExtensions
{



    public static void EnsureIsValid(this MethodBase methodInfo, object argument)
        => methodInfo.EnsureIsValid(new object[1] { argument });
    public static void EnsureIsValid(this MethodBase methodInfo, object[] arguments)
    {
        var validationResults = methodInfo.Validate(arguments);
        if(validationResults != null && validationResults.Count() > 0 && validationResults.Any(res => res.Value.Count() > 0 ))
        {
            throw new ArgumentException(
                $"Фактические аргументы метода {methodInfo.Name} " +
                $"не валидны\n{validationResults.ToJsonOnScreen()}\n");
        }
    }
    public static IDictionary<string, IEnumerable<string>> Validate(this MethodBase methodInfo, object[] arguments)
    {
        AppProviderService.GetInstance().Info($"Валидация аргументов метода {methodInfo.Name} {arguments.ToJson()}");
        IDictionary<string, IEnumerable<string>> result = new Dictionary<string, IEnumerable<string>>();

        int argumentIndex = 0;
        foreach (var parameter in methodInfo.GetParameters())
        {

            var validationAttributesData = parameter.GetCustomAttributesData()
                    .Where(data => data.AttributeType.IsExtends(typeof(ValidationAttribute)));
            IEnumerable<ValidationAttribute> validationAttributes = validationAttributesData
                    .Select(data =>
                    {
                        var types = data.ConstructorArguments.Select(a => a.ArgumentType).ToArray();

                        Type attributeType = data.AttributeType;
                        var constructor = attributeType.GetConstructor(types.ToArray());
                        object instance = constructor.Invoke(data.ConstructorArguments.Select(a => a.Value).ToArray());
                        return (ValidationAttribute)instance;

                    });

            string name = parameter.Name;
            string type = parameter.ParameterType.GetTypeName();
            object value = arguments[argumentIndex++];

            var results = new List<string>();
            foreach (ValidationAttribute attribute in validationAttributes)
            {
                
                if (attribute.IsValid(value) == false)
                    results.Add(attribute.GetType().Name);
            }
            result[parameter.Name] = results;
        }

        return result;

    }



    /// <summary>
    /// true, если тип обьекта наследуется от заданного типа по имени
    /// </summary>
    /// <param name="baseType"></param>
    /// <returns></returns>
    public static bool IsExtendedFrom(this object target, string baseType)
    {
        Type typeOfObject = new object().GetType();
        Type p = target.GetType();
        while (p != typeOfObject)
        {
            if (p.Name == baseType)
            {
                return true;
            }
            p = p.BaseType;
        }
        return false;
    }




   
    public static Dictionary<string, List<string>> Validate(this object target, string[] keys)
    {

        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        foreach (var property in target.GetType().GetProperties())
        {
            string key = property.Name;

            if (IsPrimitive(property.PropertyType))
            {
                List<string> errors = target.Validate(key);
                if (errors.Count > 0)
                {
                    result[key] = errors;
                }

            }
        }
        var optional = target.ValidateOptional();
        foreach (var p in optional)
        {
            if (result.ContainsKey(p.Key))
            {
                result[p.Key].AddRange(optional[p.Key]);
            }
            else
            {
                result[p.Key] = optional[p.Key];
            }
        }
        return result;
    }



    /// <summary>
    /// Валидация свойства объявленного заданным ключем
    /// </summary>
    /// <param name="key">ключ свойства</param>
    /// <returns></returns>
    public static List<string> Validate(this object target, string key)
    {

        List<string> errors = new List<string>();
        var attributes = target.GetType().GetPropertyAttributes(key);

        foreach (var data in target.GetType().GetProperty(key).GetCustomAttributesData())
        {
            if (data.AttributeType.GetInterfaces().Contains(typeof(MyValidation)))
            {
                List<object> args = new List<object>();
                foreach (var a in data.ConstructorArguments)
                {
                    args.Add(a.Value);
                }
                MyValidation validation =
                    data.AttributeType.Create<MyValidation>( args.ToArray());
                object value = GetValue(target, key);
                string validationResult =
                    validation.Validate(target, key, value);
                if (validationResult != null)
                {
                    errors.Add(validationResult);
                }
            }
        }
        return errors;
    }

   


    /// <summary>
    /// Проверка данных порождает исключение при не соответвии требованиям
    /// </summary>
    public static void EnsureIsValide(this object target)
    {
        var r = target.Validate();
        if (r.Count() > 0)
        {
            string message = $"Обьект " + target.GetType().Name + " не валидный: \n" + r.ToJson();
            throw new ValidationException(message);
        }
    }
     
    public static IEnumerable<ValidationResult> Validate(this object target, ValidationContext validationContext)
    {
        List<ValidationResult> results = new List<ValidationResult>();
        Dictionary<string, List<string>> errors = target.Validate();
        foreach (var errorEntry in errors)
        {
            string propertyName = errorEntry.Key;
            List<string> propertyErrors = errorEntry.Value;
            foreach (string propertyError in propertyErrors)
            {
                ValidationResult result = new ValidationResult(propertyError, new List<string>() { propertyName });
                results.Add(result);
            }
        }
        return results;
    }


    /// <summary>
    /// Получение значения свойства заданным ключем
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static object GetValue(this object target, string name)
    {
        PropertyInfo propertyInfo = target.GetType().GetProperty(name);
        FieldInfo fieldInfo = target.GetType().GetField(name);
        return
            fieldInfo != null ? fieldInfo.GetValue(target) :
            propertyInfo != null ? propertyInfo.GetValue(target) :
            throw new Exception($"Свойство {name} Не найдено в {target.GetType()}");
    }

    /// <summary>
    /// Установка значения свойства
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetValue(this object target, string key, object value)
    {
        PropertyInfo prop = target.GetType().GetProperty(key);
        if (prop != null)
        {
            prop.SetValue(target, value);
        }
        FieldInfo field = target.GetType().GetField(key);
        if (field != null)
        {
            field.SetValue(target, value);
        }
    }
    /// <summary>
    /// Валидация модели по правилам определённым через атрибуты
    /// </summary>
    /// <returns></returns>
    /// GetInputProperties(ptype)
    public static Dictionary<string, List<string>> Validate(this object p, IEnumerable<string> properties)
    {
        //p.Info("Приступаю к валидации: " + p.GetType().GetProperties().Select(p => p.Name).ToJsonOnScreen());
        object target = p;

        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        foreach (var property in properties.Select( name => p.GetType().GetProperty(name)))
        {
            string key = property.Name;

            //p.Info($"Валидация свойства: {key}");
            if (IsPrimitive(property.PropertyType))
            {
                List<string> errors = p.Validate(key);
                if (errors.Count > 0)
                {
                    result[key] = errors;
                }

            }
        }
        var optional = target.ValidateOptional();
        foreach (var pkv in optional)
        {
            if (result.ContainsKey(pkv.Key))
            {
                result[pkv.Key].AddRange(optional[pkv.Key]);
            }
            else
            {
                result[pkv.Key] = optional[pkv.Key];
            }
        }


        return result;
    }
    public static Dictionary<string, List<string>> Validate(this object p)
    {
        //p.Info("Приступаю к валидации: " + p.GetType().GetProperties().Select(p => p.Name).ToJsonOnScreen());
        object target = p;

        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        foreach (var property in target.GetType().GetProperties())
        {
            string key = property.Name;

            //p.Info($"Валидация свойства: {key}");
            if (IsPrimitive(property.PropertyType))
            {
                List<string> errors = p.Validate(key);
                if (errors.Count > 0)
                {
                    result[key] = errors;
                }

            }
        }
        var optional = target.ValidateOptional();
        foreach (var pkv in optional)
        {
            if (result.ContainsKey(pkv.Key))
            {
                result[pkv.Key].AddRange(optional[pkv.Key]);
            }
            else
            {
                result[pkv.Key] = optional[pkv.Key];
            }
        }


        return result;
    }


    private static bool IsPrimitive(Type propertyType)
        => Typing.IsPrimitive(propertyType);


    /// <summary>
    /// Процедура дополнительной валидации можеди
    /// </summary>
    /// <returns></returns>
    public static Dictionary<string, List<string>> ValidateOptional(this object target)
    {
        return new Dictionary<string, List<string>>();
    }
}
