using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Item
    {
        public enum Item_Type {WEAPON, ARMOR, ACCESORY};

        /// <summary>
        /// Item Type
        /// </summary>
        Item_Type type;

        /// <summary>
        /// Cost in money
        /// </summary>
        int cost;

        /// <summary>
        /// Wether it can be equiped (false) or consumed (true)
        /// </summary>
        bool uses;

        /// <summary>
        /// Traits modifications
        /// </summary>
        Character.Stats.Traits mod;

        /// <summary>
        /// Name of the Item
        /// </summary>
        String name;

        public Item(String fname, Item_Type ftype, int fcost, bool fuses, Character.Stats.Traits fmod)
        {
            cost = fcost;
            uses = fuses;
            mod = fmod;
            name = fname;
            type = ftype;
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
        /// Wether it can be equiped (false) or consumed (true)
        /// </summary>
        public bool Uses
        {
            get { return uses; }
        }

        /// <summary>
        /// Traits modifications
        /// </summary>
        public Character.Stats.Traits Modifications
        {
            get { return mod; }
        }
    }
}
