using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Предоставляет данные для хранилища
/// </summary>
public class CustomDataProvider
{
    private readonly CustomDbContext customDbContext;
    private readonly TypeHelper typeHelper = new TypeHelper();

    public CustomDataProvider(): this(new CustomDbContext())
    {

    }
    public CustomDataProvider(CustomDbContext customDbContext)
    {
        this.customDbContext = customDbContext;
    }

    public List<string> GetTypes()
    {
        return this.customDbContext.Attributes.Select(p => p.Qualificator.IndexOf(".") == -1 ? p.Qualificator : p.Qualificator.Substring(0, p.Qualificator.IndexOf("."))).Distinct().ToList();
    }

    public void ToNamespace(IEnumerable<Type> types)
    {
        foreach(var ptype in types)
        {
            ToType(ptype);
        }
    }

    public void ToType(Type ptype)
    {
        ClearType(ptype);
        foreach (var attr in typeHelper.GetAttributes(ptype))
        {
            ToType(typeHelper.GetTypeName(ptype), attr.Key, attr.Value);
        }
        foreach (var prop in typeHelper.GetOwnPropertyNames(ptype))
        {
            foreach (var attr in typeHelper.GetPropertyAttributes(ptype,prop))
            {
                ToProperty(typeHelper.GetTypeName(ptype), prop, attr.Key, attr.Value);
            }
        }
        foreach (var met in typeHelper.GetOwnMethodNames(ptype))
        {
            foreach (var attr in typeHelper.GetMethodAttributes(ptype,met))
            {
                ToMethod(typeHelper.GetTypeName(ptype), met, attr.Key, attr.Value);
            }
            foreach (var par in ptype.GetMethods().First(m => m.Name == met).GetParameters())
            {
                foreach (var attr in typeHelper.GetArgumentAttributes(ptype,met, par.Name))
                {
                    ToParameter(typeHelper.GetTypeName(ptype), met, par.Name, attr.Key, attr.Value);
                }
            }
        }
        this.customDbContext.SaveChanges();
    }



    public void ToType(string model, string type, string value)
    {
        this.customDbContext.Add(new CustomAttribute()
        {
            Qualificator = model,
            Type = type,
            Value = value
        });
        this.customDbContext.SaveChanges();
    }
    public void ToProperty(string model, string property, string type, string value)
    {
        this.customDbContext.Add(new CustomAttribute()
        {
            Qualificator = $"{model}.{property}",
            Type = type,
            Value = value
        });
        this.customDbContext.SaveChanges();
    }

    public void ToMethod(string model, string method, string type, string value)
    {
        this.customDbContext.Add(new CustomAttribute()
        {
            Qualificator = $"{model}.{method}",
            Type = type,
            Value = value
        });
        this.customDbContext.SaveChanges();
    }
    public void ToParameter(string model, string method, string par, string type, string value)
    {
        this.customDbContext.Add(new CustomAttribute()
        {
            Qualificator = $"{model}.{method}.{par}",
            Type = type,
            Value = value
        });
        this.customDbContext.SaveChanges();
    }


    

    private void ClearType(Type ptype)
    {
        foreach (var p in customDbContext.Attributes.Where(a => a.Qualificator.StartsWith(ptype + ".")).ToList())
        {
            customDbContext.Attributes.Remove(p);
        }
    }

    
}
