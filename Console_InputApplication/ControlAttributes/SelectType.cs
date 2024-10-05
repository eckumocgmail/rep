public class SelectTypeAttribute : ControlAttribute
{
    public HashSet<string> Options { get; set; }
    public SelectTypeAttribute(string exp = ""): base(ControlTypes.Select) 
    {
        if (String.IsNullOrWhiteSpace(exp))
            return;
        Options = ServiceFactory.Get().GetTypeNames<object>().ToHashSet();
    }
}
