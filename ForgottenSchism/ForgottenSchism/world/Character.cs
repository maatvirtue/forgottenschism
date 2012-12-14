using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;
using System.Drawing;

namespace ForgottenSchism.world
{
    public abstract class Character
    {
        public enum Class_Type {FIGHTER, CASTER, HEALER, SCOUT, ARCHER};

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
        public Stats stats;
        protected Class_Type type;
        private String org;
        protected Content.Class_info cinfo;
        protected Point pos;
        protected static int XPFACTOR=10;

        public Character(String fname, Content.Class_info fcinfo, Class_Type ftype)
        {
            stats=new Stats();

            stats.traits = fcinfo.start;

            init(fname, fcinfo, ftype);

            calcStat();

            stats.hp = stats.maxHp;
            stats.mana = stats.maxMana;

            pos = new Point(-1, -1);
        }

        public Character(String fname, Stats fstats, Content.Class_info fcinfo, Class_Type ftype)
        {
            stats = fstats;

            init(fname, fcinfo, ftype);

            pos = new Point(-1, -1);
        }

        private void init(String fname, Content.Class_info fcinfo, Class_Type ftype)
        {
            type = ftype;
            name = fname;
            level = 1;
            exp = 0;
            org = "";
            cinfo = fcinfo;
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

        public Point Position
        {
            get { return pos; }
            set { pos = value; }
        }

        protected int hit(Character c)
        {
            return Gen.d(1, 20) + (stats.traits.dex-c.stats.traits.dex);
        }

        public Character clone()
        {
            return (Character)base.MemberwiseClone();
        }

        public bool isMainChar()
        {
            return this == GameState.CurrentState.mainChar;
        }

        public bool gainExp(Character c)
        {
            int gexp = XPFACTOR;

            if(c.level<level)
                gexp /= (level - c.level);
            else if (c.level > level)
                gexp *= (c.level - level);

            return gainExp(gexp);
        }

        public bool gainExp(int fexp)
        {
            exp += fexp;

            bool ret = false;

            while (exp >= level * cinfo.lvl_exp)
            {
                ret = true;

                levelUp();
            }

            return ret;
        }

        private void calcStat()
        {
            int nmhp=stats.traits.con * 10;

            stats.hp+=(nmhp-stats.maxHp);
            stats.maxHp = nmhp;

            int cs;

            if (type == Class_Type.CASTER)
                cs = stats.traits.intel;
            else if (type == Class_Type.HEALER)
                cs = stats.traits.wis;
            else
                cs = 0;

            int nmmana = cs * 10;

            stats.mana += (nmmana - stats.maxMana);
            stats.maxMana = nmmana;

            stats.movement = stats.traits.spd/10;
        }

        public void levelUp()
        {
            if(exp<level*cinfo.lvl_exp)
                exp=level*cinfo.lvl_exp;

            level++;

            stats.traits.str=cinfo.start.str+(cinfo.levelup.str*level);
            stats.traits.dex = cinfo.start.dex + (cinfo.levelup.dex * level);
            stats.traits.con = cinfo.start.con + (cinfo.levelup.con * level);
            stats.traits.wis = cinfo.start.wis + (cinfo.levelup.wis * level);
            stats.traits.intel = cinfo.start.intel + (cinfo.levelup.intel * level);
            stats.traits.spd = cinfo.start.spd + (cinfo.levelup.spd * level);

            calcStat();
        }

        public bool isAlive()
        {
            return stats.hp > 0;
        }

        public int getHp(int hp)
        {
            stats.hp += hp;

            if (stats.hp > stats.maxHp)
            {
                hp -= stats.hp - stats.maxHp;
                stats.hp = stats.maxHp;
            }

            return hp;
        }

        public int recMagicDmg(int dmg)
        {
            int fd = dmg - stats.traits.wis;

            if (fd < 0)
                fd = 0;

            stats.hp -= fd;

            if (stats.hp < 0)
                stats.hp = 0;

            return fd;
        }

        public int recPhyDmg(int dmg)
        {
            int fd = dmg - stats.traits.con;

            if (fd < 0)
                fd = 0;

            stats.hp -= fd;

            if (stats.hp < 0)
                stats.hp = 0;

            return fd;
        }
    }
}
