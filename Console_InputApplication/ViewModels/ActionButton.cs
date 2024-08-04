public class Button : ViewItem
{
    public string Label { get; set; }
    public string Icon { get; set; }
    public System.Action<object> OnClick { get; set; }
    public bool IconOrText { get; set; }
    public bool Enabled { get; set; }
}