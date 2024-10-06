using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Console_InputApplication;

[Icon("")]
[Label("")]
[Description("Контроллер предназначен для .")]
public class FromAttributes: MyValidatableObject
{
    /// <summary>
    /// Вывод сообщения об ошибке
    /// </summary>
    /// <param name="ex">исключение</param>
    public virtual void LogError(Exception ex)
    {
        this.Info($"[{GetType().GetNameOfType()}]: {ex.Message}");
        this.Info($"[{GetType().GetNameOfType()}]: {ex.StackTrace}");
    }

    /// <summary>
    /// Вывод информационного сообщения
    /// </summary>
    public virtual void Info(object item)
    {
        this.Info($"[{GetType().GetNameOfType()}]: {item}");
    }
    
    public void Init(Type type)
    {
        var attrs = TypeAttributesExtensions.GetTypeAttributes(type);
        GetType().GetOwnPropertyNames().ToList().ForEach((name) => {
            string key = name + "Attribute";
            if ( attrs.ContainsKey(key))
            { 
                SetValue(this, name, attrs[key]);
            }
        });
    }

    
    public void Init(Type type, PropertyInfo prop)
    {
        var attrs = ForProperty(type,prop.Name);
        GetType().GetOwnPropertyNames().ToList().ForEach((name) => {
            string key = name + "Attribute";
            if (attrs.ContainsKey(key))
            {
                SetValue(this, name, attrs[key]);
            }
        });
        
    }

    private IDictionary<string, string> ForProperty(Type type, string name)
    {
        var p = type.GetProperty(name);
        Dictionary<string, string> attrs = new Dictionary<string, string>();
        if (p == null)
        {
            this.Info($"Вам слендует передать ссылку на Type в метод Utils.GetEntityContrainsts() вместо null");
            this.Info($"{new ArgumentNullException("p")}");
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

    ///
    public void Init(Type type, MethodInfo prop)
    {
        var attrs = ForMethod(type, prop.Name);
        GetType().GetOwnPropertyNames().ToList().ForEach((name) => {
            string key = name + "Attribute";
            if (attrs.ContainsKey(key))
            {
                SetValue(this, name, attrs[key]);
            }
        });

    }

    private IDictionary<string,string> ForMethod(Type type, string name)
    {
        return type.GetMethodAttributes(name);
    }

    private void SetValue(FromAttributes fromAttributes, string name, object p)
    {
        
        fromAttributes.SetProperty( name, p);
    }
}