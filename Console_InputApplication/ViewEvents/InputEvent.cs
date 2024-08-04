using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Console_InputApplication.ViewEvents
{
    public class InputEvent
    {

        [NotNullNotEmpty("")]
        public int Source { get; set; }

        [NotNullNotEmpty("")]
        public string Property { get; set; }

        public object Value { get; set; }
    }
}
