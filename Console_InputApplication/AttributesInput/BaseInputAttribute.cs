using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class InputAttribute<T> : BaseInputAttribute, MyValidation
{
    public InputAttribute() : base(InputTypes.Custom) { }
    public InputAttribute(string InputType) : base(InputType)
    {
    }

    public override bool IsValidValue(object value)
    {
        return true;
    }
}
public class BaseInputAttribute : DataTypeAttribute, MyValidation
{


    public int Order { get; set; } = 0;
    public override bool IsValid(object value)
        => base.IsValid(value) && this.IsValidValue(value);
    public virtual bool IsValidValue(object? value) => true;
    public static DataType GetDataType( string type)
    {
        switch (type)
        {                    
            case "Date": return DataType.Date;
            case "DateTime": return DataType.DateTime;
            case "Time": return DataType.Time;
            case "Duration": return DataType.Duration;
            case "Xml": return DataType.Html;
            case "Icon": return DataType.Text;
            case "Phone": return DataType.PhoneNumber;
            case "Currency": return DataType.Currency;
            case "MultilineText": return DataType.MultilineText;
            case "Email": return DataType.EmailAddress;
            case "Password": return DataType.Password;
            case "Url": return DataType.Url;
            case "Image": return DataType.Upload;
            case "CreditCard": return DataType.CreditCard;
            case "PostalCode": return DataType.PostalCode;
            case "File": return DataType.Upload;
            default: 
                return DataType.Text;
        }
    }
    public virtual string Validate(object model, string property, object value)
    {
        if( value == null)
        {
            return this.GetMessage(model, property, value); 
        }
        else
        {
            return this.OnValidate(model, property, value);
        }
        
    }
    public virtual string OnValidate(object model, string property, object value)
    {
        if (IsValidValue(value) == false)
        {
            return GetMessage(model, property, value);
        }
        else
        {
            return null;
        }
    }

    public virtual string OnGetMessage(object model, string property, object value)
    {
        if (model is null)
            return $"Значение {value} не является допустимым для адреса электронной почты";
        return $"Валидация свойства {property} модели {model?.GetType()?.GetTypeName()} завершена с ошибкой для значения {value}";
    }
    public virtual string GetMessage(object model, string property, object value)
        => OnGetMessage(model, property, value);

  
    private static List<string> INPUT_TYPES = null;
    private string _InputType;

    public static List<string> GetInputTypes()
    {
        if(INPUT_TYPES == null)
        {
            INPUT_TYPES = new List<string>();
            typeof(BaseInputAttribute).Assembly.GetTypes().Where(t=>IsExtendedFrom(t,typeof(BaseInputAttribute).Name) && IsExtendedFrom(t, typeof(InputStructureCollectionAttribute).Name)==false && false == IsExtendedFrom(t, typeof(InputPrimitiveCollectionAttribute).Name)).ToList().ForEach((Type t) => {
                INPUT_TYPES.Add(t.Name);
            });
        }        
        return INPUT_TYPES;
    }

    private static bool IsExtendedFrom(Type t, string name)
    {
        return t.IsExtends(name.ToType());
    }

    public BaseInputAttribute(string InputType): base(GetDataType(InputType))
    {

        _InputType = InputType;
    }

    public BaseInputAttribute(): base(InputTypes.Custom)
    {
    }

    public string GetCSTypeName()
    {
       
        if (_InputType == InputTypes.Color)
        {
            return "string";
        }
        else if (_InputType == InputTypes.CreditCard)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Currency)
        {
            return "float";
        }
        else if (_InputType == InputTypes.Custom)
        {
            return "string";
        }
        else if (_InputType == InputTypes.PostalCode)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Text)
        {
            return "string";
        }
     
        else if (_InputType == InputTypes.Time)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.Url)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Week)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.Xml)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Year)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.Date)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.DateTime)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.Month)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.Duration)
        {
            return "long";
        }
        else if (_InputType == InputTypes.Email)
        {
            return "string";
        }
        else if (_InputType == InputTypes.File)
        {
            return "byte[]";
        }
        else if (_InputType == InputTypes.Image)
        {
            return "byte[]";
        }
        else if (_InputType == InputTypes.Icon)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Percent)
        {
            return "int";
        }
        else if (_InputType == InputTypes.Number)
        {
            return "float";
        }
        else if (_InputType == InputTypes.Phone)
        {
            return "string";
        }
        else if (_InputType == InputTypes.MultilineText)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Password)
        {
            return "string";
        }
        else if (_InputType == InputTypes.Text)
        {
            return "string";
        }       
        else if (_InputType == InputTypes.Icon)
        {
            return "string";
        }
        else if (_InputType == InputTypes.PrimitiveCollection)
        {
            return "object[]";
        }
        else if (_InputType == InputTypes.StructureCollection)
        {
            return "object[]";
        }
        else
        {
            throw new Exception("Не удалось определить тип свойства CSharp "+_InputType);
        }
        throw new Exception("Не удалось определить тип свойства CSharp"+_InputType);

    }

    public string GetOracleDataType()
    {
        return GetSqlServerDataType();
    }

    public string GetPostgreDataType()
    {
        return GetSqlServerDataType();
    }

    public string GetMySQLDataType()
    {
        return GetSqlServerDataType();
    }

    public string GetSqlServerDataType()
    {
        if (_InputType == InputTypes.Color)
        {
            return "varchar(20)";
        }
        else if (_InputType == InputTypes.CreditCard)
        {
            return "varchar(40)";
        }
        else if (_InputType == InputTypes.Currency)
        {
            return "float";
        }
        else if (_InputType == InputTypes.Custom)
        {
            return "varchar(max)";
        }
        else if (_InputType == InputTypes.PostalCode)
        {
            return "varchar(80)";
        }
        else if (_InputType == InputTypes.Text)
        {
            return "varchar(max)";
        }

        else if (_InputType == InputTypes.Time)
        {
            return "Time";
        }
        else if (_InputType == InputTypes.Url)
        {
            return "nvarchar(255)";
        }
        else if (_InputType == InputTypes.Week)
        {
            return "Date";
        }
        else if (_InputType == InputTypes.Xml)
        {
            return "nvarchar(max)";
        }
        else if (_InputType == InputTypes.Year)
        {
            return "Date";
        }
        else if (_InputType == InputTypes.Date)
        {
            return "Date";
        }
        else if (_InputType == InputTypes.DateTime)
        {
            return "DateTime";
        }
        else if (_InputType == InputTypes.Month)
        {
            return "Date";
        }
        else if (_InputType == InputTypes.Duration)
        {
            return "long";
        }
        else if (_InputType == InputTypes.Email)
        {
            return "nvarchar(40)";
        }
        else if (_InputType == InputTypes.File)
        {
            return "varbinary(max)";
        }
        else if (_InputType == InputTypes.Image)
        {
            return "varbinary(max)";
        }
        else if (_InputType == InputTypes.Icon)
        {
            return "nvarchar(40)";
        }
        else if (_InputType == InputTypes.Percent)
        {
            return "int";
        }
        else if (_InputType == InputTypes.Number)
        {
            return "float";
        }
        else if (_InputType == InputTypes.Phone)
        {
            return "nvarchar(20)";
        }
        else if (_InputType == InputTypes.MultilineText)
        {
            return "nvarchar(max)";
        }
        else if (_InputType == InputTypes.Text)
        {
            return "nvarchar(80)";
        }
        else    if (_InputType == InputTypes.Password)
        {
            return "nvarchar(80)";
        }
        else if (_InputType == InputTypes.Icon)
        {
            return "nvarchar(40)";
        }
        else if (_InputType == InputTypes.PrimitiveCollection)
        {
            return "blob";
        }
        else if (_InputType == InputTypes.StructureCollection)
        {
            return "blob";
        }
        throw new Exception("Не удалось определить тип данных SQL Server "+_InputType        );
    }

    public virtual string Validate(object value)
    {
        return Validate(null,null,value);
    }
}