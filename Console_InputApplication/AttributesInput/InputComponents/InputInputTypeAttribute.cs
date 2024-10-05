
[Label("Укзывает на выбор из списка InputType")]
public class InputInputTypeAttribute : InputSelectAttribute
{
    public static IEnumerable<string> GetInputTypes() => BaseInputAttribute.GetInputTypes();
    public static string GetInputTypesString()
    {
        string text = "";
        foreach (string controlType in BaseInputAttribute.GetInputTypes())
        {
            text += $"{controlType},";
        }
        if (text.EndsWith(","))
            text = text.Substring(0, text.Length - 1);
        return text;
    }
    public InputInputTypeAttribute() : base(GetInputTypesString())
    {
    }
}
