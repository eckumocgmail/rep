using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ActionLink : BaseLink, IComparable
{
    public string Icon { get; set; }
    public string Tooltip { get; set; }
    public string Label { get; set; } = "";
    public bool IsActive { get; set; }
    public Action<EventArgs> OnMessage { get; set; }

    public int CompareTo(object obj)
    {
        return 1;
    }

    public override string ToString()
    {
        return Label;
    }
}