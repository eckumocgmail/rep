
using System.Collections;
using System.Collections.Generic;

[Icon("home")]
[Label("Переменная содержит одно из значений заданных через атрибут")]
[Description("Конструктор форм использует этот аттрибут для формирования выпадающего списка с выбором значения." +
    "Приложение использует его в процессе выполнения валидации модели для проверки значений переменной. ")]
public class InputDictionaryAttribute : BaseInputAttribute
{
    public object Options { get; private set; }

    public InputDictionaryAttribute() : base(InputTypes.Custom)
    {
    }
    public override bool IsValidValue(object value)
    {
        throw new System.NotImplementedException();
    }

    public InputDictionaryAttribute(string exp) : base(InputTypes.Custom)
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


}