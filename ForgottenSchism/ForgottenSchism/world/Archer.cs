using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Archer : Character
    {
        public Archer(String name): base(name, Content.Instance.cinfo.archer, Class_Type.ARCHER)
        {
            //
        }

        private int attackDmg()
        {
            return Gen.d(stats.traits.dex - 10, stats.traits.dex + 10);
        }

        public String attack(Character c)
        {
            int dmg = attackDmg();
            int h = hit(c);

            if (h < 10)
                return "MISS";

            if (h >= 20)
                dmg = (int)Math.Ceiling((double)dmg*1.5);

            return c.recPhyDmg(dmg).ToString();
        }
    }
}
