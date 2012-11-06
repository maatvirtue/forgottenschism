using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class CityMap
    {
        static VersionSys.VersionIdentity vi = new VersionSys.VersionIdentity(new byte[] { 0, 0, 0, 1 }, new byte[] { 0, 2 }, new byte[] { 1, 0 });

        City[,] cmap;

        public CityMap(int w, int h)
        {
            cmap=new City[w,h];
        }

        public CityMap(Tilemap tm)
        {
            cmap=new City[tm.NumX, tm.NumY];
        }

        public int NumX
        {
            get { return cmap.GetLength(0); }
        }

        public int NumY
        {
            get { return cmap.GetLength(1); }
        }

        public static String[] ownlist(String path)
        {
            if (!VersionSys.match(path, vi))
                throw new Exception("File is not a Forgotten Schism Citymap file v1.0");

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(12, SeekOrigin.Begin);

            String[] ret=Gen.rstra(fin);

            fin.Close();

            return ret;
        }

        public void load(String path)
        {
            if (!VersionSys.match(path, vi))
                throw new Exception("File is not a Forgotten Schism Citymap file v1.0");

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(12, SeekOrigin.Begin);

            String[] ownls = Gen.rstra(fin);

            int w=fin.ReadByte();
            int h=fin.ReadByte();

            cmap=new City[w, h];

            int x;
            int y;
            String n;
            int on;
            int s;
            int f;

            while(fin.Position<fin.Length-1)
            {
                x = fin.ReadByte();
                y = fin.ReadByte();
                n = Gen.rstr(fin);
                on = fin.ReadByte();
                s = fin.ReadByte();
                f = fin.ReadByte();

                if(on==0)
                    cmap[x, y] = new City(n, (City.CitySide)s, "", f);
                else
                    cmap[x, y] = new City(n, (City.CitySide)s, ownls[on - 1], f);
            }

            fin.Close();
        }

        public void save(String path)
        {
            FileStream fout = new FileStream(path, FileMode.Create);

            VersionSys.writeHeader(fout, vi);

            List<String> ownls = new List<String>();

            foreach (City c in cmap)
                if (c!=null&&c.Owner != "" && !ownls.Contains(c.Owner))
                    ownls.Add(c.Owner);

            Gen.wstra(fout, ownls.ToArray());

            fout.WriteByte((byte)cmap.GetLength(0));
            fout.WriteByte((byte)cmap.GetLength(1));

            City cc;

            for(int e=0; e<cmap.GetLength(1); e++)
                for (int i = 0; i < cmap.GetLength(0); i++)
                {
                    cc = cmap[i, e];

                    if (cc != null)
                    {
                        fout.WriteByte((byte)i);
                        fout.WriteByte((byte)e);
                        Gen.wstr(fout, cc.Name);

                        if (cc.Owner == "")
                            fout.WriteByte(0);
                        else
                            fout.WriteByte((byte)(ownls.FindIndex(delegate(String s) { if (s == cc.Owner) return true; else return false; })+1));

                        fout.WriteByte((byte)cc.Side);
                        fout.WriteByte((byte)cc.EnnemyFactor);
                    }
                }

            fout.Close();
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
