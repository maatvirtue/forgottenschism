using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    class Character
    {
        public struct Stats
        {
            public struct Traits
            {
                public int force;
                public int dex;
                public int intel;
                public int sag;
                public int spd;
                public int con;
            };

            public enum State {NORM, SLEEP, PARALYSED, STONE, SILIENCED, POISONED};

            public int maxHp;
            public int hp;
            public int maxMana;
            public int mana;
            public Traits traits;
            public State state;
        };

        String name;
        //Class
        int level;
        int exp;

        Stats stats;

        int movement;

        public Character(String fname)
        {
            name = fname;
            level = 0;
            exp = 0;
            stats = new Stats();
        }

        public Character(String fname, Stats fstats)
        {
            name = fname;
            level = 0;
            exp = 0;
            stats = fstats;
        }

        public String Name
        {
            get {return name;}
        }

        //Class

        public int Lvl
        {
            get { return level; }
            set { level = value; }
        }

        public int Exp
        {
            get { return exp; }
            set { exp = value; }
        }

        public Stats Stat
        {
            get { return stats; }
            set { stats = value; }
        }

        public int Move
        {
            get { return movement; }
            set { movement = value; }
        }
    }
}
