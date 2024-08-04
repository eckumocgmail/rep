using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Событие распространяющее сообщение о изменении значения свойства обьекта
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public class PropertyChangeEvent<TProperty>: CommonEventMessage<PropertyChangedMessage>
{


    public PropertyChangeEvent(PropertyChangedMessage message) : base(message)
    { 
    }
}
/// <summary>
/// Событие распространяющее сообщение о изменении значения свойства обьекта
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public class PropertyGetEvent  : CommonEventMessage<PropertyGetMessage>
{


    public PropertyGetEvent(PropertyGetMessage message) : base(message)
    {
    }
}
/// <summary>
/// Событие распространяющее сообщение о изменении значения свойства обьекта
/// </summary>
/// <typeparam name="TProperty"></typeparam>
public class PropertySetEvent  : CommonEventMessage<PropertySetMessage>
{


    public PropertySetEvent(PropertySetMessage message) : base(message)
    {
    }
}