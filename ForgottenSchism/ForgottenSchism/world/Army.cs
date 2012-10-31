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

        public Unit MainCharUnit
        {
            get
            {
                foreach (Unit u in units)
                    if (u.isMainUnit)
                        return u;

                return null;
            }
        }

        public List<Unit> Units
        {
            get {return units;}
            set { units = value; }
        }

        public List<Character> Standby
        {
            get { return standbyChar; }
            set { standbyChar = value; }
        }
    }
}
