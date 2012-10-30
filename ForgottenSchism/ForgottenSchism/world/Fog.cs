using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Fog
    {
        String name;
        bool[,] map;

        public Fog(int fw, int fh)
        {
            map=new bool[fw,fh];
        }

        public int NumX
        {
            get { return map.GetLength(0); }
        }

        public int NumY
        {
            get { return map.GetLength(1); }
        }

        public byte[] toByteArray()
        {
            Gen.BitPacker bp = new Gen.BitPacker();

            for(int i=0; i<map.GetLength(0); i++)
                for(int e=0; e<map.GetLength(1); e++)
                    bp.add(map[i, e]);

            return bp.toByteArray();
        }

        public void clear()
        {
            int nx=map.GetLength(0);
            int ny=map.GetLength(1);
            
            for (int i = 0; i < nx; i++)
                for (int e = 0; e < ny; e++)
                    map[i, e] = false;
        }

        public void dark()
        {
            int nx = map.GetLength(0);
            int ny = map.GetLength(1);

            for (int i = 0; i < nx; i++)
                for (int e = 0; e < ny; e++)
                    map[i, e] = true;
        }

        public bool get(int x, int y)
        {
            return map[x, y];
        }

        public void set(int x, int y, bool b)
        {
            map[x, y] = b;
        }
    }
}
