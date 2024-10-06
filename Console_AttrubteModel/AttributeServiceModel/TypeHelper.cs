using System.Reflection;

/// <summary>
/// Вспомогательные методы для работы с типами
/// </summary>
public class TypeHelper
{
    /// <summary>
    /// Получение аттрибутов
    /// </summary>
    public Dictionary<string, string> GetAttributes(Type p)
    {
        /*var provider = new CustomDataProvider();
        if (provider.GetTypes().Contains(ptype.GetTypeName()) == false)
        {
            provider.ToType(ptype);
        }*/
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        if (p == null)
        {
            Console.WriteLine($"Вам слендует передать ссылку на Type в метод Utils.GetEntityContrainsts() вместо null");
            Console.WriteLine($"{new ArgumentNullException("p")}");
            return attrs;
        }
        foreach (var data in p.GetCustomAttributesData())
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

    /// <summary>
    /// Получение имени типа
    /// </summary>
    public string GetTypeName(object target)
    {
        Type targetType = target is Type ? ((Type)target) : target.GetType();
        string result = GetNameOfType(targetType);
        return result;
    }

    /// <summary>
    /// Получение имени типа
    /// </summary>
    public string GetNameOfType(Type propertyType)
    {
        if (propertyType == null)
            throw new ArgumentNullException("type");
        string name = propertyType.Name;
        if (name == null) return "";
        if (name.IndexOf("`") != -1)
            name = name.Substring(0, name.IndexOf("`"));

        var arr = propertyType.GetGenericArguments();
        if (arr.Length > 0)
        {
            name += '<';
            foreach (var arg in arr)
            {
                name += GetNameOfType(arg) + ",";
            }
            name = name.Substring(0, name.Length - 1);
            name += '>';
        }
        return name;
    }

    /// <summary>
    /// Получение имен свойств 
    /// </summary>
    public List<string> GetOwnPropertyNames(Type type)
    {
        var properties = type.GetProperties();
        var own = properties.Where(p => GetTypeName(p.DeclaringType) == GetTypeName(type)).Select(p => p.Name).ToList();
        return own;
    }

    /// <summary>
    /// Получение аттрибутов свойств 
    /// </summary>
    public Dictionary<string, string> GetPropertyAttributes(Type ptype, string property)
    {
        var res = new Dictionary<string, string>();
        var pproperty = ptype.GetProperties().First(p => p.Name == property);
        if (pproperty == null)
            throw new ArgumentException(nameof(property));
        foreach (var kv in pproperty.GetCustomAttributesData())
        {
            res[GetTypeName(kv.AttributeType)] = kv.ConstructorArguments.Count() > 0 ? kv.ConstructorArguments.First().ToString() : "";
        }
        return res;
    }

    /// <summary>
    /// Получение имени методов
    /// </summary>
    public List<string> GetOwnMethodNames(Type type)
    {
        return (from p in new List<MethodInfo>((type).GetMethods()) where p.DeclaringType == type select p.Name).ToList();
    }

    /// <summary>
    /// Получение аттрибутов параметров 
    /// </summary>
    public IDictionary<string, string> GetParameterAttributes( ParameterInfo par)
    {
        if (par == null)
            throw new ArgumentException(nameof(par));

        var res = new Dictionary<string, string>(par.GetCustomAttributesData().Select(data => new KeyValuePair<string, string>(

            GetTypeName(data.AttributeType),
            data.ConstructorArguments.First().Value.ToString()
        )));

        return res;
    }

    /// <summary>
    /// Получение аттрибутов метода 
    /// </summary>
    public Dictionary<string, string> GetMethodAttributes( Type ptype, string method)

    {
        var res = new Dictionary<string, string>();
        var pproperty = ptype.GetMethods().First(p => p.Name == method);
        if (pproperty == null)
            throw new ArgumentException(nameof(method));
        foreach (var kv in pproperty.GetCustomAttributesData())
        {
            res[GetTypeName(kv.AttributeType)] = kv.ConstructorArguments.Count() > 0 ? kv.ConstructorArguments.First().ToString() : "";
        }
        return res;
    }

    /// <summary>
    /// Получение аттрибутов аргументов 
    /// </summary>
    public Dictionary<string, string> GetArgumentAttributes(
        Type ptype, string method, string parameter)
    {
        var methodInfo = ptype.GetMethods().First(p => p.Name == method);
        if (methodInfo == null)
            throw new ArgumentException(nameof(method));
        var parInfo = methodInfo.GetParameters().FirstOrDefault(p => p.Name == parameter);
        if (parInfo is null)
            throw new ArgumentException("parameter", $"Не найден параметр {parameter} в методе {GetTypeName(ptype)}.{method}");
        var res = new Dictionary<string, string>();
        foreach (var kv in parInfo.ParameterType.GetCustomAttributesData())
        {
            res[GetTypeName(kv.AttributeType)] = kv.ConstructorArguments.Count() > 0 ? kv.ConstructorArguments.First().ToString() : "";
        }


        return res;
    }
}
