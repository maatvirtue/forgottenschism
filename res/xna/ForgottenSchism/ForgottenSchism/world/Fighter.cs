using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Fighter: Character
    {
        public Fighter(String name)
            : base(name, Content.Instance.cinfo.fighter, Class_Type.FIGHTER)
        {
            //
        }

        private int attackDmg()
        {
            return Gen.d((int)stats.traits.str, (int)stats.traits.str);
        }

        public String attack(Character c)
        {
            int dmg = attackDmg();
            int h = hit(c);

            if (h <= 0)
                return "MISS";

            //if (h >= 20)
            //    dmg = (int)Math.Ceiling((double)dmg * 1.5);

            return c.recPhyDmg(dmg).ToString();
        }
    }
}
