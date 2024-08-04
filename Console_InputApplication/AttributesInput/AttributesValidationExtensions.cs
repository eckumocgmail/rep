using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Console_InputApplication;

public static class AttributesValidationExtensions
{
    
    public static bool IsPropertyUniq(this Type ptype, string property)
    {
        return ptype.GetPropertyAttribute<UniqValueAttribute>(property, null) != null;
    }
}