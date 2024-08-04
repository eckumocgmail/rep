using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_InputApplication.ViewEvents
{
    public class ListChangedEvent : BaseEventMessage
    {
        public ListChangedEvent(ListChangesMessage Target) : base(Target)
        {
        }

        public ListChangesMessage GetChanges()
        {
            return (ListChangesMessage)Target;
        }
    }
}
