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

        public Spell(String fname)
        {
            name = fname;
            dmg = 5;
        }

        public String Name
        {
            get { return name; }
        }

        public int Damage
        {
            get { return dmg; }
        }
    }
}
