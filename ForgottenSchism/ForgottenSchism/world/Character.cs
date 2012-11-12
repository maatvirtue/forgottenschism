using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Character
    {
        public enum Class_Type {FIGHTER, CASTER, HEALER, SCOUT, ARCHER, CHAR};

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

        protected String name;
        protected int level;
        protected int exp;
        protected Stats stats;
        protected Class_Type type;
        private String org;

        bool moved;

        public Character(String fname)
        {
            type = Class_Type.CHAR;
            name = fname;
            level = 0;
            exp = 0;
            stats = new Stats();
            org = "";
            moved = false;
        }

        public Character(String fname, Stats fstats)
        {
            type = Class_Type.CHAR;
            name = fname;
            level = 0;
            exp = 0;
            stats = fstats;
            org = "";
            moved = false;
        }

        public String Organization
        {
            get { return org; }
            set { org = value; }
        }

        public Class_Type Type
        {
            get { return type; }
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

        public bool Moved
        {
            get { return moved; }
            set { moved = value; }
        }
    }
}
