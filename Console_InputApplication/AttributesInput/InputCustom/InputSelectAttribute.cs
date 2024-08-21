
using AngleSharp.Text;

using System.Collections;
using System.Collections.Generic;

[Icon("home")]
[Label("Переменная содержит одно из значений заданных через атрибут")]
[Description("Конструктор форм использует этот аттрибут для формирования выпадающего списка с выбором значения."+
    "Приложение использует его в процессе выполнения валидации модели для проверки значений переменной. ")]
public class InputSelectAttribute : BaseInputAttribute {
    public string[] Options { get; set;}
    public InputSelectAttribute( ) : base(InputTypes.Custom){}
    public InputSelectAttribute(string exp) : base(InputTypes.Custom)
    {
    }
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public InputSelectAttribute(int IndexIsActive, params string[] OptionsForSelection) : base(InputTypes.Custom) {
        if (IndexIsActive < (OptionsForSelection.Length - 1) || IndexIsActive > (OptionsForSelection.Length - 1))
            throw new System.ArgumentException("IndexIsActive");
        if (OptionsForSelection == null || OptionsForSelection.Length == 1)
            throw new System.ArgumentException("IndexIsActive");
        this.Options = OptionsForSelection;
    }
    public override string OnValidate(object model, string property, object value)
    {
        return (value != null && Options is not null && Options.Contains(value.ToString()) == false) ? 
            "Значение не определено": null;
    }
    public override string OnGetMessage(object model, string property, object value)
    {
        return $"{nameof(InputSelectAttribute)} утверждает что свойству {property} может быть задано только одно из следующих значений {this.Options.ToJson()}.";
    }

    
}