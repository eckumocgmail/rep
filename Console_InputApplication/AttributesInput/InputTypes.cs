using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class InputTypes
{
    public static List<string> GetAll() => new List<string>() 
    {
        InputTypes.Color,
        InputTypes.PrimitiveCollection,
        InputTypes.Email,
        InputTypes.File,
        InputTypes.Month,
        InputTypes.Password,
        InputTypes.Phone,
        InputTypes.Week,
        InputTypes.Url,
        InputTypes.StructureCollection,
        InputTypes.Percent,
        InputTypes.Number,
        InputTypes.Custom,
        InputTypes.Image,
        InputTypes.Xml,
        InputTypes.Time,
        InputTypes.DateTime,
        InputTypes.Date,
        InputTypes.PostalCode,
        InputTypes.CreditCard,
        InputTypes.Icon,
        InputTypes.Currency,
        InputTypes.Year,
        InputTypes.Text,
        InputTypes.MultilineText
    };
    public const string Color = "Color";
    public const string PrimitiveCollection = "PrimitiveCollection";
    public const string Email = "Email";
    public const string File = "File";
    public const string Month = "Month";
    public const string Password = "Password";
    public const string Phone = "Phone";
    public const string Week = "Week";
    public const string Url = "Url";
    public const string StructureCollection = "StructureCollection";
    public const string Percent = "Percent";
    public const string Number = "Number";
    public const string Custom = "Custom";
    public const string Date = "Date";
    public const string DateTime = "DateTime";
    public const string Time = "Time";
    public const string Duration = "Duration";
    public const string Xml = "Xml";
    public const string Image = "Image";
    public const string PostalCode = "PostalCode";
    public const string CreditCard = "CreditCard";
    public const string Currency = "Currency";
    public const string Icon = "Icon";
    
    public const string Year = "Year";
    public const string Text = "Text";
    public const string MultilineText = "MultilineText";
}