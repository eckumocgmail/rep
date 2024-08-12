using System.Collections;
using System.Collections.Generic;

[Icon("home")]
[Label("Переменная содержит одно из значений заданных через атрибут")]
[Description("Конструктор форм использует этот аттрибут для формирования выпадающего списка с выбором значения." +
    "Приложение использует его в процессе выполнения валидации модели для проверки значений переменной. ")]
public class InputDictionaryAttribute : ControlAttribute
{
    public object Options { get; private set; }

    public InputDictionaryAttribute() : base()
    {
    }
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public InputDictionaryAttribute(string exp) : base()
    {
    }
     
    public override string OnValidate(object model, string property, object value)
    {
        return (value != null && value is bool) ? null :
            "Тип данных свойства ввода задан некорректно";
    }
    public override string OnGetMessage(object model, string property, object value)
    {
        return $"{nameof(InputSelectAttribute)} утверждает что свойству {property} может быть задано только одно из следующих значений {this.Options.ToJson()}.";
    }

    public override ViewItem CreateControl(InputFormField field)
    {
        throw new System.NotImplementedException();
    }

    public override ViewItem CreateControl(FormField field)
    {
        throw new System.NotImplementedException();
    }
}