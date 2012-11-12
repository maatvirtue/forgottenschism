using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Healer : Character
    {
        public Healer(String name)
            : base(name, Content.Instance.cinfo.healer, Class_Type.HEALER)
        {
            type=Class_Type.HEALER;
        }

        public int healP()
        {
            return Gen.d(stats.traits.wis - 10, stats.traits.wis + 10);
        }

        public int heal(Character c)
        {
            return c.getHp(healP());
        }
    }
}
