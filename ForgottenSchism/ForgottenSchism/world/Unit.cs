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
        Content.Graphics.CachedImage us;
        String org;
        public int movement;
        int spd;
        bool dead;

        public Unit(Character fleader)
        {
            deployed = false;

            map = new Character[4,4];
            leader = fleader;
            name = leader.Name;

            org = "main";

            map[0, 0] = leader;

            us = Graphic.getSprite(leader);

            dead = false;

            resetMovement();
        }

        public Unit(Character fleader, int x, int y)
        {
            deployed = false;

            map = new Character[4, 4];
            leader = fleader;
            name = leader.Name;

            dead = false;
            org = "main";

            map[x, y] = leader;

            us = Graphic.getSprite(leader);

            resetMovement();
        }
        
        public Unit(Character fleader, String n)
        {
            deployed = false;

            map = new Character[4, 4];
            leader = fleader;
            name = n;
            org = "main";


            dead = false;
            map[0, 0] = leader;

            resetMovement();
        }

        public Unit(Character fleader, String n, String forg)
        {
            deployed = false;

            map = new Character[5, 5];
            leader = fleader;
            name = n;
            dead = false;

            org = forg;

            map[0, 0] = leader;

            us = Graphic.getSprite(leader);

            resetMovement();
        }

        public String Organization
        {
            get { return org; }
            set { org=value; }
        }

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }

        public int Speed
        {
            get { return spd; }
        }

        public bool hasLeader()
        {
            return leader != null;
        }

        private void updateMovement()
        {
            int m = int.MaxValue;

            foreach (Character c in map)
                if (c!=null && c.stats.traits.spd < m)
                    m = c.stats.traits.spd;

            spd = m;
        }

        public void resetMovement()
        {
            updateMovement();
            movement = spd / 10;
        }

        public bool isMainUnit()
        {
            return leader==GameState.CurrentState.mainChar;
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

        public Content.Graphics.CachedImage UnitSprite
        {
            get { return us; }
        }

        public void setLeader(int x, int y)
        {
            Character c=map[x, y];
            
            if (c != null)
                leader = c;

            us = Graphic.getSprite(leader);
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
