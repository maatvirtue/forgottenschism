using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
