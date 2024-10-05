
using Microsoft.AspNetCore.Components.Forms;

public class InputComponentsUnit : TestingUnit
{
    public InputComponentsUnit()
    {
        Append(new InputInputTypeAttributeTest());
        Append(new InputControlTypeAttributeTest());
       
    }
}
