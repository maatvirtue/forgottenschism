using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Caster : Character
    {
        public Caster(String name): base(name)
        {
            type=Class_Type.CASTER;
        }
    }
}
