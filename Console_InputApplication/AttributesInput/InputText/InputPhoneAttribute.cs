﻿
using Newtonsoft.Json.Linq;

using System;

[Label("Номер телефона")]
public class InputPhoneAttribute : BaseInputAttribute   
{
    private string _message;

    public InputPhoneAttribute() : base(InputTypes.Phone) { }
    public InputPhoneAttribute(string message) : base(InputTypes.Phone)
    {
        _message = message;
    }
    public override bool IsValidValue(object value)
    {
        return true;
    }

    private bool isNumber( char ch )
    {
        return "0123456789".IndexOf(ch) != -1;
    }

    public override string OnValidate(object model, string property, object value)
    {
        
        if (value==null)
        {
            return GetMessage(model,property,value);
        }
        else
        {
            string message = value.ToString();
            if(IsPhoneNumber(message))
            {
                return null;
            }
            else

            {
                return GetMessage(model,property,value);
            }
        }
        return null;
    }

    private bool IsPhoneNumber(string message)
    {
        if (message.Length != "7-904-334-1124".Length)
        {
            return false;
        }
        else
        {

            if (message[1] != '-' || message[5] != '-' || message[9] != '-')
            {
                return false;
            }
            else
            {
                if (isNumber(message[0]) == false ||
                    isNumber(message[2]) == false || isNumber(message[3]) == false || isNumber(message[4]) == false ||
                    isNumber(message[6]) == false || isNumber(message[7]) == false || isNumber(message[8]) == false ||
                    isNumber(message[10]) == false || isNumber(message[11]) == false ||
                    isNumber(message[12]) == false || isNumber(message[13]) == false)
                {
                    return false;
                }
            }
        }
        return true;    

    }

    public override string OnGetMessage(object model, string property, object value)
    {
        if (string.IsNullOrEmpty(this.ErrorMessage))
        {
            return "Номер телефона задаётся в формате X-XXX-XXX-XXXX";
        }
        else
        {
            return ErrorMessage;
        }
    }

   
}

