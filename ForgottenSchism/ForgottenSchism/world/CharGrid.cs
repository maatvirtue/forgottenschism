using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    class CharGrid
    {
        Character[,] cmap;

        public CharGrid(int w, int h)
        {
            cmap=new Character[w, h];
        }

        public CharGrid(Tilemap tm)
        {
            cmap = new Character[tm.NumX, tm.NumY];
        }

        public bool isChar(int x, int y)
        {
            return cmap[x, y] == null;
        }

        public 
    }
}
