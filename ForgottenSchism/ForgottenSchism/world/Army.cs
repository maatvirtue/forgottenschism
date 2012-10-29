using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Army
    {
        List<Unit> units;
        List<Character> standbyChar;

        public Army()
        {
            units = new List<Unit>();
            standbyChar = new List<Character>();
        }

        public List<Unit> Units
        {
            get {return units;}
        }

        public List<Character> Standby
        {
            get { return standbyChar; }
        }

        // DUMMY METHOD: For testing purpose only. To be removed eventually.
        /*public void GenerateArmy()
        {
            for (int i = 0; i <= 5; i++)
            {
                units.Add(new Unit(new Character("Dummy" + i)));
                for (int j = 0; j <= 3; j++)
                {
                    units[i].AddChar(new int[2] { i + 1, j }, new Character("Dummy" + i + j));
                }
            }

            for (int i = 0; i <= 10; i++)
            {
                standbyChar.Add(new Character("Derp" + i));
            }
        }*/
    }
}
