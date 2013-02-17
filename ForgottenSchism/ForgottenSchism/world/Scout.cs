using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Scout : Character
    {
        public Scout(String name)
            : base(name, Content.Instance.cinfo.scout, Class_Type.SCOUT)
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
