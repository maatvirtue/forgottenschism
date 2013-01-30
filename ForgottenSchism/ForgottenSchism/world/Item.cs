using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Item
    {
        public enum Item_Type {WEAPON, ARMOR, ACCESORY, CONSUMABLE};

        public struct OtherEffect
        {
            /// <summary>
            /// hp modification
            /// </summary>
            public int hp;

            /// <summary>
            /// Mana modification
            /// </summary>
            public int mp;

            public OtherEffect(int fhp, int fmp)
            {
                hp = fhp;
                mp = fmp;
            }
        }

        /// <summary>
        /// Item Type
        /// </summary>
        Item_Type type;

        /// <summary>
        /// Cost in money
        /// </summary>
        int cost;

        /// <summary>
        /// Traits modifications
        /// </summary>
        Character.Stats.Traits mod;

        /// <summary>
        /// Name of the Item
        /// </summary>
        String name;

        OtherEffect effect;

        public Item(String fname, Item_Type ftype, int fcost, Character.Stats.Traits fmod)
        {
            effect = new OtherEffect();
            effect.hp = 0;
            effect.mp = 0;

            cost = fcost;
            mod = fmod;
            name = fname;
            type = ftype;
        }

        public Item(String fname, Item_Type ftype, int fcost, Character.Stats.Traits fmod, OtherEffect fce)
        {
            effect = fce;

            cost = fcost;
            mod = fmod;
            name = fname;
            type = ftype;
        }

        /// <summary>
        /// Effects when Consumed
        /// </summary>
        public OtherEffect Effect
        {
            get { return effect; }
        }

        /// <summary>
        /// Name of the Item
        /// </summary>
        public String Name
        {
            get { return name; }
        }

        /// <summary>
        /// Item Type
        /// </summary>
        public Item_Type Type
        {
            get { return type; }
        }

        /// <summary>
        /// Cost in money
        /// </summary>
        public int Cost
        {
            get { return cost; }
        }

        /// <summary>
        /// Traits modifications
        /// </summary>
        public Character.Stats.Traits Modifications
        {
            get { return mod; }
        }

        public Item clone()
        {
            return (Item)base.MemberwiseClone();
        }
    }
}
