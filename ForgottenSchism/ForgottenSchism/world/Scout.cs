using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Scout : Character
    {
        public Scout(String name): base(name)
        {
            type=Class_Type.SCOUT;
        }
    }
}
