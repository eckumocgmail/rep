﻿
[Label("Продолжительность")]
public class InputDurationAttribute : BaseInputAttribute
{
    public InputDurationAttribute() : base(InputTypes.Duration) { }

    public override string OnValidate(object model, string property, object value)
    {
        if (value.ToString().IsInt())
        {
            return null;
        }
        else
        {
            return "Продолжительность задаётся в миличекундах";
        }
    }
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public override string OnGetMessage(object model, string property, object value)
    {
        return "";
    }
} 