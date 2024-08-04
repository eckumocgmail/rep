using System;

[Label("Массив простых значений")]
public class InputPrimitiveCollectionAttribute : BaseInputAttribute
{


    public InputPrimitiveCollectionAttribute(): base(InputTypes.PrimitiveCollection){
        
    }
    public override bool IsValidValue(object value)
    {
        throw new System.NotImplementedException();
    }

    public override string OnValidate(object model, string property, object value)
    {
        if (IsValidValue(value) == false)
        {
            return GetMessage(model, property, value);
        }
        else
        {
            return null;
        }
    }

    public override string OnGetMessage(object model, string property, object value)
    {
        return $"Валидация свойства {property} модели {model.GetType().GetTypeName()} завершена с ошибкой для значения {value}";
    }
}