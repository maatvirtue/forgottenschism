using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    class Unit
    {
        Dictionary<int[], Character>characters;
        String name;

        public Unit(Character leader)
        {
            characters = new Dictionary<int[], Character>();
            characters.Add(new int[2]{1, 1}, leader);
            name = leader.Name;
        }

        public String Name
        {
            get { return name; }
        }
    }
}
