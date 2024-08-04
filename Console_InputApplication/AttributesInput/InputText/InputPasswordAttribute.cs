using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Label("Пароль")]
public class InputPasswordAttribute : BaseInputAttribute
{
    public override bool IsValidValue(object value)
    {
        return value != null && value.ToString().Length >= 8;
    }



    public InputPasswordAttribute(  ) : base(InputTypes.Password)
    {

        
    }

  
}

