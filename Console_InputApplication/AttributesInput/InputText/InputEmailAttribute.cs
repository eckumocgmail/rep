﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[Label("Электронная почта")]
[Icon("email")]
public class InputEmailAttribute : BaseInputAttribute, MyValidation
{
    protected string _message;
    public override bool IsValidValue(object value)
    {
        string email = value.ToString();
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            if (Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public InputEmailAttribute() : base(InputTypes.Email) { }
    public InputEmailAttribute(string message) : base(InputTypes.Email)
    {
        _message = message;
         
    }

    public override string Validate(object value)
    {
        if (value is null)
            return null;
        var mes = $"Значение {value} не является допустимым для адреса электронной почты";
        return IsValidValue(value) ? null : mes ;
    }

    public override string OnValidate(object model, string property, object value)
    {
        if(value == null)
        {
            return GetMessage(model,property,value);
        }
        string email = value.ToString();
        if (string.IsNullOrWhiteSpace(email))
            return GetMessage(model, property, value);

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return GetMessage(model, property, value)+"\n"+e.Message+"\n\n"+e.ToString();
        }
        catch (ArgumentException e)
        {
            return GetMessage(model, property, value) + "\n" + e.Message + "\n\n" + e.ToString();
        }

        try
        {
            if(Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                return null;
            }
            else
            {
                return GetMessage(model, property, value);
            }
        }
        catch (RegexMatchTimeoutException)
        {
            return GetMessage(model, property, value);
        }
    }

 
    public override string OnGetMessage(object model, string property, object value)
    {
        if (_message == null)
        {
            return "Не правильно укаазан адрес электронной почты";
        }
        else
        {
            return _message;
        }
    }


}

