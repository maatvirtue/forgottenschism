using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Unit
    {
        Character[,]  map;
        String name;

        public Unit(Character leader)
        {
            map = new Character[3,3];
            name = leader.Name;

            map[0, 0] = leader;
        }

        public bool isChar(int x, int y)
        {
            return map[x, y] != null;
        }

        public Character get(int x, int y)
        {
            return map[x, y];
        }

        public void set(int x, int y, Character c)
        {
            map[x, y] = c;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Character> Characters
        {
            get 
            {
                List<Character> ret = new List<Character>();

                foreach (Character c in map)
                    ret.Add(c);

                return ret;
            }
        }
    }
}
