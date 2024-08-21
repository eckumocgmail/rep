using NetCoreConstructorAngular.Data.DataAttributes;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
public class BaseValidationAttribute: ValidationAttribute, MyValidation  
{
    public BaseValidationAttribute():base() {
       
    }


    public virtual string ValidateValue(object value) => throw new NotImplementedException();

    public virtual string Validate(object model, string property, object value) => throw new NotImplementedException();
    public virtual string GetMessage(object model, string property, object value) => throw new NotImplementedException();
    protected  override ValidationResult IsValid(object value, ValidationContext validationContext)
    {            
        string result = this.Validate(validationContext.ObjectInstance, validationContext.MemberName, value);
        if (result == null)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(result);
        }

    }

    public string Validate(object value)
    {
        return Validate(null, null, value);
    }
}
 
