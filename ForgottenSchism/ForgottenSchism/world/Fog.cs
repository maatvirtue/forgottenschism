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
        static byte[] uid = { 0, 0, 0, 1 };
        static byte[] type = { 0, 2 };
        static byte[] ver = { 1, 0 };

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
