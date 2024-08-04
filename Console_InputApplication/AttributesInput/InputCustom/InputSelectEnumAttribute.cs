using System;
using System.Linq;


public class InputSelectEnumAttribute : BaseInputAttribute
{
    public InputSelectEnumAttribute(string enumType) : base(enumType)
    {
        this.EnumType = enumType;
    }

    public string EnumType { get; }

    public override bool IsValidValue(object value)
        => value != null && Enum.GetNames(EnumType.ToType()).Contains(value.ToString());
}