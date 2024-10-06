
using Newtonsoft.Json;

public class NLink : ViewItem 
{
    public string Icon { get; set; }
    public string Label { get; set; }
    public string Href { get; set; }
    public bool IsActive { get; set; } = false;
    public string Tooltip { get; set; }
    public List<NLink> ChildLinks { get; set; }


    [JsonIgnore()]
    public object Item { get; set; }
}