using System;
using System.Collections.Specialized;
using System.Linq;


public class InputSelectEnumAttribute : BaseInputAttribute
{
    public InputSelectEnumAttribute() : base(InputTypes.Custom) { }
    public InputSelectEnumAttribute(Array values) : base(InputTypes.Custom)
    {
        List<string> options = new();
        foreach(var item in values)
        {
            options.Add(item.ToString());   
        }

    }
    public InputSelectEnumAttribute(string enumType) : base(enumType)
    {
        this.EnumType = enumType;
        
    }

    public string EnumType { get; }

    public override bool IsValidValue(object value)
        => value != null && Enum.GetNames(EnumType.ToType()).Contains(value.ToString());
}
