using NetCoreConstructorAngular.Data.DataAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Console_InputApplication;
public class MyValidatableObject<TContext> : MyValidatableObject
{

}
public class MyValidatableObject : BaseEntity, IValidatableObject
{
    IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
    {
        List<ValidationResult> results = new List<ValidationResult>();
        Dictionary<string, List<string>> errors = this.Validate();
        foreach (var errorEntry in errors)
        {
            string propertyName = errorEntry.Key;
            List<string> propertyErrors = errorEntry.Value;
            foreach (string propertyError in propertyErrors)
            {
                ValidationResult result = new ValidationResult(propertyError, new List<string>() { propertyName });
                results.Add(result);
            }
        }
        return results;
    }
}