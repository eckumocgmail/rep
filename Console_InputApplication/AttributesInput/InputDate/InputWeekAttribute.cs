using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Label("Отчётная неделя")]
public class InputWeekAttribute: BaseInputAttribute
{
    public InputWeekAttribute( ) : base(InputTypes.Week) { }
    public InputWeekAttribute(string exts) : base(InputTypes.Week)
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

