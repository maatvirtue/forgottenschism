using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    class Fighter: Character
    {
        public Fighter(String name): base(name)
        {
            type=Class_Type.FIGHTER;
        }
    }
}
