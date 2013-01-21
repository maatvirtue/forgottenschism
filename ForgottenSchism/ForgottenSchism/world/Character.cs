﻿using System;
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

        public struct Equipment
        {
            public Item weapon;
            public Item armor;
            public Item accesory;
        }

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

                public static Traits operator +(Traits a, Traits b)
                {
                    Traits r = new Traits();

                    r.str = a.str + b.str;
                    r.dex = a.dex + b.dex;
                    r.intel = a.intel + b.intel;
                    r.wis = a.wis + b.wis;
                    r.spd = a.spd + b.spd;
                    r.con = a.con + b.con;

                    return r;
                }

                public static Traits operator -(Traits a, Traits b)
                {
                    Traits r = new Traits();

                    r.str = a.str - b.str;
                    r.dex = a.dex - b.dex;
                    r.intel = a.intel - b.intel;
                    r.wis = a.wis - b.wis;
                    r.spd = a.spd - b.spd;
                    r.con = a.con - b.con;

                    return r;
                }
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
        protected Equipment equipment;

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

        private int xpForLvl(int level)
        {
            int xpprev = cinfo.lvl_exp;

            for (int curlvl = 2; curlvl < level; curlvl++)
                xpprev = (int)Math.Ceiling((double)xpprev*1.5);

            return xpprev;
        }

        private void init(String fname, Content.Class_info fcinfo, Class_Type ftype)
        {
            type = ftype;
            name = fname;
            level = 1;
            exp = 0;
            org = "";
            cinfo = fcinfo;
            equipment = new Equipment();
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
            //return Gen.d(1, 20) + (stats.traits.dex-c.stats.traits.dex);
            int rng = Gen.d(1, 100);
            int hitRate = 0;
            int dexDiff = stats.traits.dex - c.stats.traits.dex;

            if(dexDiff > 0)
            {
                hitRate = 90 + (int)(Math.Pow(0.0004 * (double)dexDiff, 2.0) + 0.0487 * (double)dexDiff);
            }
            else if(dexDiff < 0)
            {
                hitRate = 90 - (int)(Math.Pow(0.0052 * (double)(dexDiff), 2.0) + 0.1382 * (double)dexDiff);
            }
            else
            {
                hitRate = 90;
            }
            Console.WriteLine("RNG: " + rng + "    Hit Rate: " + hitRate);
            return hitRate - rng;
        }

        /// <summary>
        /// Creates a clone of this Character
        /// </summary>
        /// <returns>Clone of this Character</returns>
        public Character clone()
        {
            return (Character)base.MemberwiseClone();
        }

        /// <summary>
        /// Equip a new equipment and disequip the old one
        /// </summary>
        /// <param name="e">Equipment to equip</param>
        public void equip(Equipment e)
        {
            equipWeapon(e.weapon);
            equipArmor(e.armor);
            equipAccesory(e.accesory);
        }

        /// <summary>
        /// Equip a new weapon and disequip the old one
        /// </summary>
        /// <param name="i">weapon Item</param>
        public void equipWeapon(Item i)
        {
            if(equipment.weapon!=null)
                stats.traits -= equipment.weapon.Modifications;

            if (i != null)
                stats.traits += i.Modifications;

            equipment.weapon = i;
        }

        /// <summary>
        /// Equip a new armor and disequip the old one
        /// </summary>
        /// <param name="i">armor Item</param>
        public void equipArmor(Item i)
        {
            if(equipment.armor!=null)
                stats.traits -= equipment.armor.Modifications;

            if (i != null)
                stats.traits += i.Modifications;

            equipment.armor = i;
        }

        /// <summary>
        /// Equip a new accesory and disequip the old one
        /// </summary>
        /// <param name="i">accesory Item</param>
        public void equipAccesory(Item i)
        {
            if(equipment.accesory!=null)
                stats.traits -= equipment.accesory.Modifications;

            if (i != null)
                stats.traits += i.Modifications;

            equipment.accesory = i;
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

            while (exp >= xpForLvl(level+1))
            {
                ret = true;

                levelUp();
            }

            return ret;
        }

        private void calcStat()
        {
            int nmhp=stats.traits.con * (level + 1);

            stats.hp+=(nmhp-stats.maxHp);
            stats.maxHp = nmhp;

            //int cs;

            //if (type == Class_Type.CASTER)
            //    cs = stats.traits.intel;
            //else if (type == Class_Type.HEALER)
            //    cs = stats.traits.wis;
            //else
            //    cs = 0;

            int nmmana = stats.traits.wis * 5;

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

            if (fd <= 0)
                fd = 1;

            stats.hp -= fd;

            if (stats.hp < 0)
                stats.hp = 0;

            return fd;
        }

        public int recPhyDmg(int dmg)
        {
            int fd = dmg - stats.traits.con;

            if (fd <= 0)
                fd = 1;

            stats.hp -= fd;

            if (stats.hp < 0)
                stats.hp = 0;

            return fd;
        }
    }
}
