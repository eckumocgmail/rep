using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Label("Файл")]
[Description("Свойство должно быть типа List<FileItem>")]
public class InputFileItemAttribute: BaseInputAttribute
{
    public InputFileItemAttribute() : base(InputTypes.File) { }
    public InputFileItemAttribute(string exts) : base(InputTypes.File)
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
