using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Label("Год")]
public class InputYearAttribute : BaseInputAttribute
{
    public InputYearAttribute() : base(
        InputTypes.Year)
    {
    }
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public override string OnValidate(object model, string property, object value)
    {
        return null;
    }

    public override string OnGetMessage(object model, string property, object value)
    {
        return null;
    }
}

