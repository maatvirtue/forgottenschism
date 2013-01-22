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
            XPFACTOR = 10;
        }

        public int healP()
        {
            if (stats.mana < 10)
                return 0;

            stats.mana -= 10;

            return Gen.d(stats.traits.intel, stats.traits.intel);
        }

        public int heal(Character c)
        {
            return c.getHp(healP());
        }
    }
}
