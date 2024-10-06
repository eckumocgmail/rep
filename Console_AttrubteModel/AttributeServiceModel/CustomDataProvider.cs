using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Предоставляет данные для хранилища
/// </summary>
public class CustomDataProvider: IDisposable
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

    /// <summary>
    /// Получение списка типов, сведения по которым зарегистрированы
    /// </summary>
    public List<string> GetTypes()
    {
        return this.customDbContext.CustomAttributes.Select(p => p.Qualificator.IndexOf(".") == -1 ? p.Qualificator : p.Qualificator.Substring(0, p.Qualificator.IndexOf("."))).Distinct().ToList();
    }

     
    /// <summary>
    /// Обновление сведений по аттрибутам типа
    /// Выполняется отчистка имеющихся
    /// </summary>    
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

    /// <summary>
    /// Добавление динамического аттрибута
    /// </summary>
    /// <param name="ptype">тип</param>
    /// <param name="attrType">тип аттрибута</param>
    /// <param name="attrValue">значение переданное в конструтор</param>
    public void AddAttribute(Type ptype, string attrType, string attrValue)
    {
        ToType(typeHelper.GetTypeName(ptype),attrType, attrValue);
    }

    /// <summary>
    /// Удаление динамического аттрибута
    /// </summary>
    public void RemoveAttribute(string q, string attrType )
    {
        var pa = customDbContext.CustomAttributes.FirstOrDefault(a => a.Qualificator == q && a.Type == attrType);
        customDbContext.CustomAttributes.Remove(pa);
        customDbContext.SaveChanges();
    }

    /// <summary>
    /// Добавление атрибута по квалификатору
    /// </summary>
    public void AddAttribute(string qualificator, string attrType, string attrValue)
    {
        ToType(qualificator, attrType, attrValue);

    }

    /// <summary>
    /// Удаление динамического аттрибута
    /// </summary>
    public void RemoveAttribute(Type ptype, string attrType)
    {
        var pa = this.customDbContext.CustomAttributes.FirstOrDefault(a => a.Type==attrType && a.Qualificator == typeHelper.GetTypeName(ptype));
        if (pa is null)
            throw new ArgumentException();
        customDbContext.CustomAttributes.Remove(pa);
        customDbContext.SaveChanges();
    }

    
    /// <summary>
    /// Регистрация значения аттрибута
    /// </summary>
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

    /// <summary>
    /// Регистрация значения аттрибута
    /// </summary>
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

    /// <summary>
    /// Регистрация значения аттрибута
    /// </summary>
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

    /// <summary>
    /// Регистрация значения аттрибута
    /// </summary>
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

    /// <summary>
    /// Удаление всех аттрибутов для заданного пита
    /// </summary>
    public void ClearType(Type ptype)
    {
        foreach (var p in customDbContext.CustomAttributes.Where(a => a.Qualificator.StartsWith(ptype + ".")).ToList())
        {
            customDbContext.CustomAttributes.Remove(p);
        }
    }


    public virtual void Dispose()
    {
        customDbContext.Dispose();
    }
}
