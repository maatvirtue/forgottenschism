using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Item
    {
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

        public Item(int fcost, bool fuses, Character.Stats.Traits fmod)
        {
            cost = fcost;
            uses = fuses;
            mod = fmod;
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
