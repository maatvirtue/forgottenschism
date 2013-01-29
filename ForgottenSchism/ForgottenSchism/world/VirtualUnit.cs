using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    class VirtualUnit
    {
        /// <summary>
        /// Number of Characters in the Unit (factor)
        /// </summary>
        int num;

        /// <summary>
        /// Level of the Characters in the Unit (factor)
        /// </summary>
        int lvl;

        /// <summary>
        /// What class is going to be the dominant class
        /// </summary>
        Character.Class_Type ctype;

        /// <summary>
        /// Organisation of the unit
        /// </summary>
        String org;

        public VirtualUnit(int fnum, int flvl, Character.Class_Type fctype, String forg)
        {
            org = forg;
            num = fnum;
            lvl = flvl;
            ctype = fctype;
        }

        public int NumberOfChar
        {
            get { return num; }
            set { num = value; }
        }

        public String Organization
        {
            get { return org; }
            set { org = value; }
        }

        public int Level
        {
            get { return lvl; }
            set { lvl = value; }
        }

        public Character.Class_Type ClassType
        {
            get { return ctype; }
            set { ctype = value; }
        }

        public Unit gen()
        {
            Character leader=Character.genClass(ctype, Character.genName(ctype));
            leader.Organization = org;

            if (num == 1)
                leader.toLvl(lvl);
            else if(num==2)
                leader.toLvl(lvl + 1);
            else
                leader.toLvl(lvl + 2);

            Unit unit=new Unit(leader);
            unit.Organization = org;

            unit.set(0, 0, null);
            placePref(unit, leader);

            if (num == 2)
            {
                Character c = Character.genClass(ctype, Character.genName(ctype));
                c.toLvl(lvl);
                placePref(unit, c);
            }
            else
            {
                int mc = (int)Math.Ceiling(num*0.60);
                int ld = -1;
                Character c;
                Character.Class_Type curtype;

                Array ctype_val = Enum.GetValues(typeof(Character.Class_Type));
                Random rand = new Random();

                for (int i = 0; i < num; i++)
                {
                    if (i < mc)
                        curtype = ctype;
                    else
                        curtype = (Character.Class_Type)ctype_val.GetValue(rand.Next(ctype_val.Length));

                    c = Character.genClass(curtype, Character.genName(curtype));
                    c.Organization = org;
                    c.toLvl(lvl+ld);

                    placePref(unit, c);

                    if (ld == 1)
                        ld = -1;
                    else
                        ld++;
                }
            }

            return unit;
        }


        /// <summary>
        /// Try to place a generated character at its best place according to class
        /// </summary>
        /// <param name="u">Unit to add the Character to</param>
        /// <param name="c">Character to add</param>
        private void placePref(Unit u, Character c)
        {
            int[,] prefPos;

            int[,] archer={{0,0}, {3,0}, {1,0}, {2,0}};
            int[,] caster = { { 1, 2 }, { 2, 2 }, { 0, 2 }, { 3, 2 } };
            int[,] fighter = { { 1, 3 }, { 2, 3 }, { 0, 3 }, { 3, 3 } };
            int[,] healer = { { 1, 1 }, { 2, 1 }, { 1, 2 }, { 2, 2 } };
            int[,] scout = { { 0, 1 }, { 3, 1 }};
            int[,] none={{0,0}};

            if (ctype == Character.Class_Type.ARCHER)
                prefPos=archer;
            else if (ctype == Character.Class_Type.CASTER)
                prefPos = caster;
            else if (ctype == Character.Class_Type.FIGHTER)
                prefPos = fighter;
            else if (ctype == Character.Class_Type.HEALER)
                prefPos = healer;
            else if (ctype == Character.Class_Type.SCOUT)
                prefPos = scout;
            else
                prefPos=none;

            place(u, c, prefPos);
        }

        /// <summary>
        /// Tries to place a Character in the Unit in the preferred spot in order provided. Otherwise it
        /// places the Character in the first available spot.
        /// </summary>
        /// <param name="u">Unit to place the Character in</param>
        /// <param name="prefPos">Prefered positions in order</param>
        /// <param name="c">Character to place</param>
        /// <returns>true if the unit was placed, false if there is no spot left</returns>
        private bool place(Unit u, Character c, int[,] prefPos)
        {
            for(int i=0; i<prefPos.GetLength(0); i++)
                if(place(u, c, prefPos[i,0], prefPos[i,1]))
                    return true;

            return place(u, c);
        }

        /// <summary>
        /// Tries to place a Character in the Unit in the first available spot
        /// </summary>
        /// <param name="u">Unit to place the Character in</param>
        /// <param name="c">Character to place</param>
        /// <returns>true if the unit was placed, false if there is no spot left</returns>
        private bool place(Unit u, Character c)
        {
            for (int i=0; i<4; i++)
                for (int e = 0; e < 4; e++)
                    if (u.get(i, e) == null)
                    {
                        u.set(i, e, c);
                        return true;
                    }

            return false;
        }

        /// <summary>
        /// Tries to place a Character at the given place in a unit
        /// </summary>
        /// <param name="u">Unit to place the Character in</param>
        /// <param name="c">Character to place</param>
        /// <param name="x">Position in x</param>
        /// <param name="y">Position in y</param>
        /// <returns>true if the unit was placed, false if there was already someon in the spot</returns>
        private bool place(Unit u, Character c, int x, int y)
        {
            if (u.get(x, y) != null)
                return false;

            u.set(x, y, c);

            return true;
        }
    }
}
