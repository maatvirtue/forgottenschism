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
            //
        }

        public SpellList getCastableSpells()
        {
            return Content.Instance.spellList.getSpells(Lvl);
        }

        private int attackDmg(Spell sp)
        {
            if (stats.mana < sp.ManaCost)
                return 0;

            stats.mana -= sp.ManaCost;

            return sp.Damage + stats.traits.intel;
        }

        public String attack(Character c, Spell sp)
        {
            int dmg = attackDmg(sp);

            return c.recMagicDmg(dmg).ToString();
        }
    }
}
