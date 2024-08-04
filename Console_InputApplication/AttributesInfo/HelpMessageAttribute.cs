using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Icon("help")]
[Label("Вспомогательное сообщение")]
[Description("Вспомогательное сообщение содержит информацию об использовании свойства, метода или типа")]
[HelpMessage("Сообщение будет отображено на формах как справочная инфомрация")]
public class HelpMessageAttribute : Attribute
{
    [Label("Текст сообщения")]    
    public string Message { get; }

    public HelpMessageAttribute(string Message)
    {
        this.Message = Message;
    }
}