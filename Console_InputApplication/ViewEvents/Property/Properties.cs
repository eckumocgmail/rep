using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_InputApplication.ViewEvents.Property
{
    public class Properties : ConcurrentDictionary<string, Property<PropertyValue>>
    {
    }
}
