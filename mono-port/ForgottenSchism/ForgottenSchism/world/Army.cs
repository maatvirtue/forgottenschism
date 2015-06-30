using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Army
    {
        List<Unit> units;
        List<Character> standbyChar;

        /// <summary>
        /// Army's Inventory
        /// </summary>
        Inventory inv;

        int money;

        public Army()
        {
            money = Content.Instance.money_info.start;
            units = new List<Unit>();
            standbyChar = new List<Character>();
            inv = new Inventory();
        }

        public Inventory Inventory
        {
            get { return inv; }
            set { inv = value; }
        }

        public Unit MainCharUnit
        {
            get
            {
                foreach (Unit u in units)
                    if (u.isMainUnit())
                        return u;

                return null;
            }
        }

        public int Money
        {
            get { return money; }
            set { money = value; }
        }

        public List<Unit> Units
        {
            get {return units;}
            set { units = value; }
        }

        public List<Character> Standby
        {
            get { return standbyChar; }
            set { standbyChar = value; }
        }

        public void undeployAll()
        {
            foreach (Unit u in units)
                u.Deployed = false;
        }

        public void setOrgAll(String org)
        {
            for (int i = 0; i < standbyChar.Count; i++)
                standbyChar[i].Organization = org;

            for (int i = 0; i < units.Count; i++)
                for (int e = 0; e < units[i].Characters.Count; e++)
                    units[i].Characters[e].Organization = org;
        }
    }
}
