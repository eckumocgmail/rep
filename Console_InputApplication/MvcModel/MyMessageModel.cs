﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class MyMessageProperty 
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();
    public bool IsCollection { get; 
        set; }
}




public class MyMessageModel
{
    public string Name { get; set; }
    public List<MyMessageProperty> Properties { get; set; }
    public MyMessageProperty GetProperty(string name)
    {
        return (from p in Properties where p.Name == name select p).FirstOrDefault();
    }

    public MyMessageModel(Type type)
    {
 
        this.Name = type.Name;
        this.Properties = new List<MyMessageProperty>();
        foreach (var property in type.GetProperties())
        {
            string TypeName = property.PropertyType.Name;
            bool IsCollection = false;

            if (property.PropertyType.Name.StartsWith("List"))
            {
                IsCollection = true;
                string text = property.PropertyType.AssemblyQualifiedName;
                text = text.Substring(text.IndexOf("[[") + 2);
                text = text.Substring(0, text.IndexOf(","));
                TypeName = text.Substring(text.LastIndexOf(".") + 1);
                //this.Info(property.Name + " " +text);
            }
            this.Properties.Add(new MyMessageProperty {
                Name = property.Name,
                IsCollection = IsCollection,
                Type = TypeName,
                Attributes = ForProperty(type, property.Name)
            });
        }      
    }

    private Dictionary<string, string> ForProperty(Type type, string name)
    {
        throw new NotImplementedException();
    }
}



