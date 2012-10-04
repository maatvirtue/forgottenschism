using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    class Army
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
    }
}
