using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Character
    {
        public struct Stats
        {
            public struct Traits
            {
                public int str;
                public int dex;
                public int intel;
                public int wis;
                public int spd;
                public int con;
            };

            public enum State {NORMAL, SLEEP, PARALYZED, STONE, SILENCED, POISONED};

            public int maxHp;
            public int hp;
            public int maxMana;
            public int mana;
            public int movement;
            public Traits traits;
            public State state;
        };

        String name;
        int level;
        int exp;
        Stats stats;

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
    }
}
