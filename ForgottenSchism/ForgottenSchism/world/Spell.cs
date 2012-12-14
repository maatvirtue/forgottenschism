using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Spell
    {
        String name;
        int damage;
        int manaCost;
        int minLvl;
        int maxLvl;

        public Spell(String fname, int fdamage, int fmanaCost, int fminLvl, int fmaxLvl)
        {
            name = fname;
            damage = fdamage;
            manaCost = fmanaCost;
            minLvl = fminLvl;
            maxLvl = fmaxLvl;
        }

        public String Name
        {
            get { return name; }
        }

        public int ManaCost
        {
            get { return manaCost; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public int MinLvl
        {
            get { return minLvl; }
        }

        public int MaxLvl
        {
            get { return maxLvl; }
        }

        public Spell clone()
        {
            return (Spell)base.MemberwiseClone();
        }
    }
}
