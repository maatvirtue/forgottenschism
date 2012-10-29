using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Archer : Character
    {
        public Archer(String name): base(name)
        {
            type=Class_Type.ARCHER;
        }
    }
}
