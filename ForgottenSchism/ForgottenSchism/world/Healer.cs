using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Healer : Character
    {
        public Healer(String name): base(name)
        {
            type=Class_Type.HEALER;
        }
    }
}
