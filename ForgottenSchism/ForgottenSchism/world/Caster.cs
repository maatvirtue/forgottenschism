using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Caster : Character
    {
        public Caster(String name)
            : base(name, Content.Instance.cinfo.caster, Class_Type.CASTER)
        {
            type=Class_Type.CASTER;
        }

        private int attackDmg(Spell sp)
        {
            int td = sp.Damage + stats.traits.intel;

            return Gen.d(td - 10, td + 10);
        }

        public int attack(Character c, Spell sp)
        {
            int dmg = attackDmg(sp);

            return c.recMagicDmg(dmg);
        }
    }
}
