using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class City
    {
        String name;

        public City(String fname)
        {
            name = fname;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
