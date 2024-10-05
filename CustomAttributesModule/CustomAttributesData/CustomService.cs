using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public class CustomService
{
    private readonly CustomDbContext customAttributesDbContext;
    private readonly TypeHelper typeHelper = new TypeHelper();


    public CustomService() : this(new CustomDbContext())
    {

    }

    public CustomService(CustomDbContext customAttributesDbContext)
    {
        this.customAttributesDbContext = customAttributesDbContext;
    }



    // = type.GetAttributes()
    public Dictionary<string, string> GetAttributes(Type type)
    {
        return GetAttributes(typeHelper.GetTypeName(type));
    }
    public Dictionary<string, string> GetAttributes(string typeName)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.Attributes.Where(a => a.Qualificator == typeName).ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }

    // = type.GetMethodAttributes()
    public Dictionary<string, string> GetMethodAttributes(string typeName, string method)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.Attributes.Where(a => a.Qualificator == $"{typeName}.{method}").ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }

    // = type.GetArgumentAttributes()
    public Dictionary<string, string> GetParameterAttributes(string typeName, string method, string parameter)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.Attributes.Where(a => a.Qualificator == $"{typeName}.{method}.{parameter}").ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }

    // = type.GetPropertyAttributes()
    public Dictionary<string, string> GetPropertyAttributes(string typeName, string property)
    {
        Dictionary<string, string> result = new();
        foreach (var attr in customAttributesDbContext.Attributes.Where(a => a.Qualificator == $"{typeName}.{property}").ToList())
        {
            result[attr.Type] = attr.Value;
        }
        return result;
    }


    // = type.GetAllPropertiesAttributes()
    public Dictionary<string, Dictionary<string, string>> GetMembersAttributes(string typeName)
    {
        Dictionary<string, Dictionary<string, string>> result = new();

        foreach (var attr in customAttributesDbContext.Attributes.Where(a => a.Qualificator.StartsWith($"{typeName}.")).ToList())
        {
            var arr = attr.Qualificator.Split(".");
            if (arr.Length != 2)
                continue;
            var type = arr[0];
            var member = arr[1];
            if(!result.ContainsKey(member))
            {
                result[member] = new();
            }
            result[member][attr.Type] = attr.Value;
        }
        return result;
    }
}