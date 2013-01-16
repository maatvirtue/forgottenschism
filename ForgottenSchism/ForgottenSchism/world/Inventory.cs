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
    }
}
