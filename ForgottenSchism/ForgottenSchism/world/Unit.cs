using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Unit
    {
        Character[,]  map;
        String name;
        Character leader;
        bool deployed;

        public Unit(Character fleader)
        {
            deployed = false;

            map = new Character[5,5];
            leader = fleader;
            name = leader.Name;

            map[0, 0] = leader;
        }

        public Unit(Character fleader, int x, int y)
        {
            deployed = false;

            map = new Character[5, 5];
            leader = fleader;
            name = leader.Name;

            map[x, y] = leader;
        }

        public bool isMainUnit
        {
            get { return leader==GameState.CurrentState.mainChar; }
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

        public bool Deployed
        {
            get { return deployed; }
            set { deployed = value; }
        }

        public void delete(int x, int y)
        {
            map[x, y] = null;
        }

        public Character Leader
        {
            get { return leader; }
        }

        public void setLeader(int x, int y)
        {
            Character c=map[x, y];
            
            if (c != null)
                leader = c; 
        }

        public bool isLeader(int x, int y)
        {
            return map[x, y]==leader;
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
                    if(c!=null)
                        ret.Add(c);

                return ret;
            }
        }
    }
}
