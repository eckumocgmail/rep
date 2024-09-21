
using AngleSharp.Text;

using Microsoft.AspNetCore.Components;

using System.Collections;
using System.Collections.Generic;

[Icon("home")]
[Label("Переменная содержит одно из значений заданных через атрибут")]
[Description("Конструктор форм использует этот аттрибут для формирования выпадающего списка с выбором значения."+
    "Приложение использует его в процессе выполнения валидации модели для проверки значений переменной. ")]
public class InputSelectAttribute : ControlAttribute {

    [Parameter]
    public List<string> Options { get; set; } = new List<string>();
    public InputSelectAttribute( )  {}
    public InputSelectAttribute(string exp)  
    {
        //Options = (IEnumerable<string>)Expression.Compile(exp, this);
        if(String.IsNullOrWhiteSpace(exp) == false)
            this.Options = exp.Split(",").Select(x => x.Trim()).ToList();
    }
    public static IEnumerable<string> GetControlTypes() => AttrsUtil.GetControlTypes();
    public static IEnumerable<string> GetInputTypes() => BaseInputAttribute.GetInputTypes();
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public InputSelectAttribute(int IndexIsActive, params string[] OptionsForSelection)  {
        if (IndexIsActive < (OptionsForSelection.Length - 1) || IndexIsActive > (OptionsForSelection.Length - 1))
            throw new System.ArgumentException("IndexIsActive");
        if (OptionsForSelection == null || OptionsForSelection.Length == 1)
            throw new System.ArgumentException("IndexIsActive");
        this.Options = OptionsForSelection.ToList();
    }
    public override string OnValidate(object model, string property, object value)
    {
        if (value is null)
            return null;
        if (Options.Contains(value.ToString()) == false)
            return $"Значение может быть только одним из заданных значений";
        return null;
    }
    public override string OnGetMessage(object model, string property, object value)
    {
        return $"{nameof(InputSelectAttribute)} утверждает что свойству {property} может быть задано только одно из заданных значений.";
    }

    
}