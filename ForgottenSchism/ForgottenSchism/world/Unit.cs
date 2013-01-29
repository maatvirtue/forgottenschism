using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;
using System.Drawing;

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

        /// <summary>
        /// Unit's Inventory
        /// </summary>
        Inventory inv;

        public Unit(Character fleader)
        {
            deployed = false;

            map = new Character[4,4];
            leader = fleader;
            name = leader.Name;

            org = "main";

            map[0, 0] = leader;
            leader.Position = new Point(0, 0);

            us = Graphic.getSprite(leader);

            dead = false;

            inv = new Inventory();

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
            leader.Position = new Point(x, y);

            us = Graphic.getSprite(leader);
            inv = new Inventory();

            resetMovement();
        }
        
        public Unit(Character fleader, String n)
        {
            deployed = false;

            map = new Character[4, 4];
            leader = fleader;
            name = n;
            org = "main";
            inv = new Inventory();


            dead = false;
            map[0, 0] = leader;
            leader.Position = new Point(0, 0);

            resetMovement();
        }

        public Unit(Character fleader, String n, String forg)
        {
            deployed = false;

            map = new Character[5, 5];
            leader = fleader;
            name = n;
            dead = false;
            inv = new Inventory();

            org = forg;

            map[0, 0] = leader;
            leader.Position = new Point(0, 0);

            us = Graphic.getSprite(leader);

            resetMovement();
        }

        public Inventory Inventory
        {
            get { return inv; }
            set { inv = value; }
        }

        public String Organization
        {
            get { return org; }
            set { org=value; }
        }

        public bool Dead
        {
            get
            {
                foreach (Character c in map)
                    if (c != null && c.isAlive())
                        return false;
                
                return true;
            }
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

        public Unit clone()
        {
            /*
             Character[,]  map;
             */

            Character l = leader.clone();
            Unit u = new Unit(l);

            u.name = name;
            u.org = org;
            u.deployed = deployed;
            u.movement = movement;
            u.spd = spd;
            u.dead = dead;
            u.us = us;

            u.map=new Character[4,4];
            Character c;

            for (int i = 0; i < map.GetLength(0); i++)
                for (int e = 0; e < map.GetLength(1); e++)
                    if (map[i,e] != null)
                    {
                        c=map[i,e].clone();
                        u.map[i,e]=c;

                        if (map[i, e] == leader)
                            u.leader = c;
                    }

            return u;
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

            if(c!=null)
                c.Position = new Point(x, y);
        }

        public bool Deployed
        {
            get { return deployed; }
            set { deployed = value; }
        }

        public void delete(int x, int y)
        {
            map[x, y].Position = new Point(-1, -1);
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
