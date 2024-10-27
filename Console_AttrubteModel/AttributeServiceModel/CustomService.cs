public interface ICustomService
{
    void Dispose();
    Dictionary<string, string> GetAttributes(string typeName);
    Dictionary<string, Dictionary<string, string>> GetMembersAttributes(string typeName);
    Dictionary<string, string> GetMethodAttributes(string typeName, string method);
    Dictionary<string, string> GetParameterAttributes(string typeName, string method, string parameter);
    Dictionary<string, string> GetPropertyAttributes(string typeName, string property);
}



/// <summary>
/// Реализует методы получения атрибутов в понятном виде
/// </summary>
public class CustomService : CustomDataProvider, ICustomService
{
    private readonly CustomDbContext customAttributesDbContext;
    private readonly TypeHelper typeHelper = new TypeHelper();
    private readonly bool cachingAttributes = true;

    public CustomService() : this(new CustomDbContext())
    {
    }

    public CustomService(CustomDbContext customAttributesDbContext)
    {
        this.customAttributesDbContext = customAttributesDbContext;
    }

    public bool HasType(Type type) => GetTypes().Contains(typeHelper.GetTypeName(type));


    // = type.GetAttributes()
    public Dictionary<string, string> GetAttributes(Type type)
    {
        if(!HasType(type)) ToType(type);
        return GetAttributes(typeHelper.GetTypeName(type));
    }

    public Dictionary<string, string> GetAttributes(string typeName)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.CustomAttributes.Where(a => a.Qualificator == typeName).ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }

    // = type.GetMethodAttributes()
    public Dictionary<string, string> GetMethodAttributes(string typeName, string method)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.CustomAttributes.Where(a => a.Qualificator == $"{typeName}.{method}").ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }

    // = type.GetArgumentAttributes()
    public Dictionary<string, string> GetParameterAttributes(string typeName, string method, string parameter)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.CustomAttributes.Where(a => a.Qualificator == $"{typeName}.{method}.{parameter}").ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }

    // = type.GetPropertyAttributes()
    public Dictionary<string, string> GetPropertyAttributes(string typeName, string property)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.CustomAttributes.Where(a => a.Qualificator == $"{typeName}.{property}").ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }


    // = type.GetAllPropertiesAttributes()
    public Dictionary<string, Dictionary<string, string>> GetMembersAttributes(string typeName)
    {
        Dictionary<string, Dictionary<string, string>> result = new();

        foreach (var attr in customAttributesDbContext.CustomAttributes.Where(a => a.Qualificator.StartsWith($"{typeName}.")).ToList())
        {
            var arr = attr.Qualificator.Split(".");
            if (arr.Length != 2)
                continue;
            var type = arr[0];
            var member = arr[1];
            if (!result.ContainsKey(member))
            {
                result[member] = new();
            }
            result[member][attr.Type] = attr.Value;
        }
        return result;
    }

    public override void Dispose()
    {
        ((IDisposable)customAttributesDbContext).Dispose();
    }


}