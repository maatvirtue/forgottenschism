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

        public Spell(String fname)
        {
            name = fname;
            damage = 5;
            manaCost = 10;
            minLvl = 1;
            maxLvl = 100;
        }

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

        //
    }
}
