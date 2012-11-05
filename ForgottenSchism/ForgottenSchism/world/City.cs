using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForgottenSchism.world
{
    public class City
    {
        public enum CitySide { NONE, TOP, RIGHT, BOTTOM, LEFT };

        String name;
        String owner;
        CitySide side;

        public City(String fname)
        {
            name = fname;
            side = CitySide.NONE;
            owner = "";
        }

        public City(String fname, CitySide fside, String fowner)
        {
            name = fname;
            side = fside;
            owner = fowner;
        }

        public CitySide Side
        {
            get { return side; }
            set { side = value; }
        }

        public String Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public static CitySide move2side(Point src, Point dest)
        {
            if (src.Y > dest.Y)
                return CitySide.TOP;
            else if (src.X < dest.X)
                return CitySide.RIGHT;
            else if (src.Y < dest.Y)
                return CitySide.BOTTOM;
            else if (src.X > dest.X)
                return CitySide.LEFT;
            else
                return CitySide.NONE;
        }

        public static CitySide opposed(CitySide side)
        {
            if (side == CitySide.TOP)
                return CitySide.BOTTOM;
            else if (side == CitySide.RIGHT)
                return CitySide.LEFT;
            else if (side == CitySide.BOTTOM)
                return CitySide.TOP;
            else if (side == CitySide.LEFT)
                return CitySide.RIGHT;
            else
                return CitySide.NONE;
        }
    }
}
