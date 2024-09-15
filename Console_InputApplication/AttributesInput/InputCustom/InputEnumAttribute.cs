public class InputEnumAttribute : BaseInputAttribute
{
    public List<string> Options { get; set; } = new();
    public InputEnumAttribute() : base(InputTypes.Custom) { 
        throw new NotImplementedException();
    }
    public InputEnumAttribute(Type enumtype) : base(InputTypes.Custom)
    {
        if (enumtype is null)
            throw new ArgumentNullException("enumtype", "Аргумент конструктора enumtype содержит ссылку на null");
        if (enumtype.IsEnum == false)
            throw new ArgumentException("enumtype", "Аргумент конструктора enumtype содержит ссылку на Type, который не является enum");
        foreach (var item in Enum.GetValues(enumtype))
        {
            Options.Add(item.ToString());
        }
    } 

    public override bool IsValidValue(object value)
        => value != null && Options.Contains(value.ToString());
}