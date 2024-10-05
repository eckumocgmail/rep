
using Microsoft.AspNetCore.Components.Forms;

public class AttributesInputUnit: TestingUnit
{
    public AttributesInputUnit()
    {
        Append(new InputComponentsUnit());
        Append(new InputCollectionUnit());
        Append(new InputTextUnit());
        Append(new InputNumberUnit());
        Append(new InputDateUnit());
        Append(new InputCustomUnit());
    }
}
