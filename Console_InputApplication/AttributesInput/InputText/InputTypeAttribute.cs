
public class InputTypeAttribute : InputSelectAttribute
{
    public InputTypeAttribute()
    {
        this.Options = ServiceFactory.Get().GetTypeNames();
    }

    public override bool IsValidValue(object? value)
    {
        if(value is null)
        {
            return true;
        }
        else
        {
            return value.ToString().ToType() is not null;
        }            
    }
}

