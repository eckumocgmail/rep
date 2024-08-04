using System;


[AttributeUsage(AttributeTargets.All)]
public class CanActivateAttribute: BaseValidationAttribute
{
    private readonly string url;
    private readonly string[] keys;

    public CanActivateAttribute(
        string  controllerActionUrl, 
        [NotNullNotEmpty]string queryParamsCsv
    ){

        this.url = url;
        this.keys = queryParamsCsv.Split(',');
    }


    public void OnValidationResult()
    {

    }

    public override string Validate(object model, string property, object value)
    {
        throw new NotImplementedException();
    }

    public override string GetMessage(object model, string property, object value)
    {
        throw new NotImplementedException();
    }

    public override string ValidateValue(object value)
    {
        throw new NotImplementedException();
    }
}

