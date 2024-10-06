using System;
using System.Collections.Generic;
using System.Linq;
/// <summary>
/// 
/// </summary>
/// 
[Label("Документация по типу")]
public class TypeDocumentation : FromAttributes
{



    [Label("Сведения о свойства")]
    public class SummaryOfProperty : FromAttributes
    {
        [NotNullNotEmpty]
        [InputEngWord]
        public string Name { get; set; }

        [NotNullNotEmpty]
        [InputEngWord]
        public string Icon { get; set; } = "home";

        [InputRusText]
        [NotNullNotEmpty]
        public string Label { get; set; } = "";

        [InputRusText]
        public string Description { get; set; } = "";

        [NotNullNotEmpty]
        [InputRusText]
        public string Help { get; set; } = "";

        public SummaryOfProperty(Type t, string prop)
        {
            Init(t);
            this.Name = prop;
        }
        
    }


    [NotNullNotEmpty]
    public string Name { get; set; }

    [InputIcon]
    public string Icon { get; set; } = "home";

    [InputText]
    public string Label { get; set; } = "";

    [InputText]
    public string Help { get; set; } = "";

    [InputMultilineText]
    public string Description { get; set; } = "";






    public Dictionary<string, SummaryOfProperty> PropertiesDictionary { get; set; } 
        = new Dictionary<string, SummaryOfProperty>();
    public Dictionary<string, SummaryOfProperty> ActionDictionary { get; set; }
        = new Dictionary<string, SummaryOfProperty>();


    public TypeDocumentation(Type type)
    {
        Init(type);
        var attrs = TypeAttributesExtensions.GetTypeAttributes(type);
        GetType().GetOwnPropertyNames().ToList().ForEach((name) => {
            string key = name ;
            if (attrs.ContainsKey(key))
            {
                this.SetProperty( name, attrs[key]);
            }
        });
        Name = type.Name;
        
        type.GetOwnPropertyNames().ToList().ForEach(name =>
            PropertiesDictionary[name] = new SummaryOfProperty(type, name)
        );
        type.GetOwnMethodNames().ToList().ForEach(name =>
            ActionDictionary[name] = new SummaryOfProperty(type, name)
        );
    }

 
}