
using System;

[Icon("home")]
[Label("Логическая переменная")]
[Description("Атрибут логическая переменной определяет способ ввода через элемент управления checkbox")]
public class InputComboboxAttribute : BaseInputAttribute
 
{
    private readonly string entity;
    private readonly string property;

    public InputComboboxAttribute(string pair): base("Custom")
    {
        /*if( string.IsNullOrEmpty(pair) || pair.IndexOf(",") == -1)
        {
            pair = "{{GetType().Name}},ID";
        }*/
        string[] spices = pair.Split(",");
        if (spices.Length != 2)
        {
            throw new ArgumentException("Атрибут SelectDataDictionary задан не правильно");
        }
        this.entity = spices[0].Trim();
        this.property = spices[1].Trim();
        if (this.property.StartsWith("{{") == false)
        {
            this.property = "{{" + this.property + "}}";
        }
    }
    public override string OnValidate(object model, string property, object value)
    {
        return (value != null && value is bool) ? null:
            "Тип данных свойства ввода задан некорректно";
    }
    public override string OnGetMessage(object model, string property, object value)

    {
        return "Тип данных свойства ввода задан некорректно";
    }

    public override bool IsValidValue(object value)
    {
        return true;
    }

}