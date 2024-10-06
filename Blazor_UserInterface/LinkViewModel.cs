public class LinkViewModel
{
    public string Icon { get; set; }
    public string Label { get; set; }
    public string Href { get; set; }
    public string Tooltip { get; set; }
}


public class NavLinkViewModel: LinkViewModel
{
    public bool IsActive { get; set; }
}