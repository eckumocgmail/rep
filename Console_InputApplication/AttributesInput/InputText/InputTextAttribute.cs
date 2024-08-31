﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

 
public class InputTextAttribute : BaseInputAttribute, MyValidation
{
    private string _message;
    public override bool IsValidValue(object value)
    {
        return true;
    }

    public InputTextAttribute() : base(InputTypes.MultilineText) { }
    public InputTextAttribute(string message =null): base(InputTypes.MultilineText)
    {
        _message = message;
    }

    public override string OnGetMessage(object model, string property, object value)
    {
        if(string.IsNullOrEmpty(_message))
        {
            return "Значение свойство не является тестовым значением";
        }
        else
        {
            return _message;
        }

    }

    public override string OnValidate(object model, string property, object value)
    {
        if( value != null)
        {
            if(value is String == false)
            {
                return GetMessage(model, property, value);
            }
        }
        return null;
    }

}

