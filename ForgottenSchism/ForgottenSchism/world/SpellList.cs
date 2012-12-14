using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class SpellList
    {
        List<Spell> spellls;

        public SpellList(List<Spell> fspellls)
        {
            spellls = fspellls;
        }

        public List<Spell> getAll()
        {
            List<Spell> ret = new List<Spell>();

            foreach (Spell sp in spellls)
                ret.Add(sp.clone());

            return ret;
        }

        public List<Spell> getSpells(int level)
        {
            List<Spell> ret = new List<Spell>();

            foreach (Spell sp in spellls)
                if (sp.MinLvl <= level && sp.MaxLvl >= level)
                    ret.Add(sp);

            return ret;
        }
    }
}
