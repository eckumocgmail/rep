using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Label("Файл")]
[Description("Свойство должно быть типа byte[]")]
public class InputFileDataAttribute: BaseInputAttribute
{
    public InputFileDataAttribute() : base(InputTypes.File) { }
    public InputFileDataAttribute(string exts) : base(InputTypes.File)
    {

    }
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public override string OnValidate(object model, string property, object value)
    {
        return "";
    }

    public override string OnGetMessage(object model, string property, object value)
    {
        return "";
    }
}
