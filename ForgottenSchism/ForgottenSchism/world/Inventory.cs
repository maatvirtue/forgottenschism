using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class Inventory
    {
        List<Item> itemls;

        public Inventory()
        {
            itemls = new List<Item>();
        }

        public List<Item> Items
        {
            get { return itemls; }
            set { itemls = value; }
        }

        /// <summary>
        /// Gives an Item to another Inventory (removes it from this Inventory and adds it to the parameter Inventory)
        /// </summary>
        /// <param name="i">Item to give</param>
        /// <param name="inv">Inventory that will reveive the item</param>
        public void give(Item i, Inventory inv)
        {
            if (!itemls.Contains(i))
                return;

            itemls.Remove(i);
            inv.Items.Add(i);
        }

        /// <summary>
        /// Gives an Item to another Inventory (removes it from this Inventory and adds it to the parameter Inventory)
        /// </summary>
        /// <param name="i">Index of the Item to give</param>
        /// <param name="inv">Inventory that will reveive the item</param>
        public void give(int i, Inventory inv)
        {
            if (itemls.Count<i+1)
                return;

            Item it = itemls[i];

            itemls.Remove(it);
            inv.Items.Add(it);
        }
    }
}
