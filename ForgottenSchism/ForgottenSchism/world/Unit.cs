using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Unit
    {
        Dictionary<int[], Character>characters;
        String name;

        public Unit(Character leader)
        {
            characters = new Dictionary<int[], Character>();
            characters.Add(new int[2]{0, 0}, leader);
            name = leader.Name;
        }

        public void AddChar(int[] pos, Character charac)
        {
            characters.Add(pos, charac);
        }

        public String Name
        {
            get { return name; }
        }

        public List<Character> Characters
        {
            get 
            {
                List<Character> chars = new List<Character>();
                foreach (KeyValuePair<int[], Character> entry in characters)
                {
                    chars.Add(entry.Value);
                }
                return chars;
            }
        }
    }
}
