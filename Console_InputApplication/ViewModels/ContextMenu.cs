using System.Collections.Generic;

public class ContextMenu
{
    
    public string Icon { get; set; }
    public string Label { get; set; }
    public System.Action<object> OnClick { get; set; }
    public List<ContextMenu> Items { get; set; } = new List<ContextMenu>();
}