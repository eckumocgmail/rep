using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

[Label("Метка")]
public class LabelAttribute: Attribute
{
    public string Text { get; set; }
    public LabelAttribute(string text) 
    {
        this.Text = text;
    }
}