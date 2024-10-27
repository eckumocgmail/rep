using NetCoreConstructorAngular.Data.DataAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class InputPositiveAttribute : InputNumberAttribute
{
    protected string _message;

    public InputPositiveAttribute() : base(InputTypes.Time) { }
    public InputPositiveAttribute(string message )
    {
        _message = message;
    }
    public override string GetMessage(object model, string property, object value)
    {
        if (string.IsNullOrEmpty(_message))
        {
            return "Значение должно быть положительным";
        }
        else
        {
            return _message;
        }
    }
    //Validate(new object (), "", value);
 
    public string Validate(object value) => ValidateValue(value);
    public override string Validate(object model, string property, object value)
    {
        if (value != null)
        {            
            return value.ToString().IsNumber()? null: value.ToString().ToFloat()>0? null: "Значения не является числовых";
        }
        else
        {
            return null;
        }
    }

    public string ValidateValue(object value) => Validate(new object(), "", value);
}