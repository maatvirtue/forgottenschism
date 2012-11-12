using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Spell
    {
        String name;
        int dmg;
        int mc;

        public Spell(String fname)
        {
            name = fname;
            dmg = 5;
            mc = 10;
        }

        public String Name
        {
            get { return name; }
        }

        public int ManaCost
        {
            get { return mc; }
        }

        public int Damage
        {
            get { return dmg; }
        }
    }
}
