using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForgottenSchism.world
{
    public class CityMap
    {
        City[,] cmap;

        public CityMap(int w, int h)
        {
            cmap=new City[w,h];
        }

        public CityMap(Tilemap tm)
        {
            cmap=new City[tm.NumX, tm.NumY];
        }

        public bool isCity(int x, int y)
        {
            return cmap[x, y]!=null;
        }

        public City get(int x, int y)
        {
            return cmap[x, y];
        }

        public void set(int x, int y, City c)
        {
            cmap[x, y] = c;
        }

        public City[] toArray()
        {
            List<City> cls=new List<City>();

            for(int i=0; i<cmap.GetLength(0); i++)
                for(int e=0; e<cmap.GetLength(1); e++)
                    if(cmap[i, e]!=null)
                        cls.Add(cmap[i, e]);

            return cls.ToArray();
        }
    }
}
