using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Сообщение о изменения свойства обьекта
/// </summary>
public class PropertyChangedMessage: MyEvent
{
    public object Source { get; set; }
    public string Property { get; set; }
    public object Before { get; set; }
    public object After { get; set; }


    public PropertyChangedMessage() { 
        Type = "change";
    }

    public string GetBindedActionName()
    {
        return "On" + Property + "Changed";
    }
}
public class PropertyGetMessage : MyEvent
{
    public object Source { get; set; }
    public string Property { get; set; }
    public object Value { get; set; }
  


    public PropertyGetMessage()
    {
        Type = "get";
    }

    public string GetBindedActionName()
    {
        return "On" + Property + "Get";
    }
}
public class PropertySetMessage : MyEvent
{
    public object Source { get; set; }
    public string Property { get; set; }
    public object Before { get; set; }
    public object After { get; set; }


    public PropertySetMessage()
    {
        Type = "set";
    }

    public string GetBindedActionName()
    {
        return "On" + Property + "Set";
    }
}