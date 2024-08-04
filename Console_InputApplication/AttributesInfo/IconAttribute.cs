using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Icon("help")]
[Label("Иконка")] 
[Description("Иконка будет отображаться в пользовательском интерфейсе")]
[HelpMessage("Иконка будет отображаться в пользовательском интерфейсе")]
public class IconAttribute : Attribute
{
    [InputIcon()]
    public string Icon { get; }

    public IconAttribute(string Icon)
    {
        this.Icon = Icon;
    }

}