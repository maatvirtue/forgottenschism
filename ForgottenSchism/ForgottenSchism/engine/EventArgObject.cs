using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.engine
{
    class EventArgObject: EventArgs
    {
        public object o;

        public EventArgObject(object fo)
        {
            o = fo;
        }
    }
}
