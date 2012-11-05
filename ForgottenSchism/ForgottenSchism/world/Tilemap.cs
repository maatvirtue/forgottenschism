using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Tilemap
    {
        Tile[,] map;
        Fog fog;
        String name;
        Point startpos;
        CityMap cmap;

        static byte[] uid={0, 0, 0, 1};
        static byte[] type={0, 1};
        static byte[] ver={1, 0};

        public Tilemap(int w, int h)
        {
            cmap = new CityMap(w, h);
            startpos = new Point();
            map=new Tile[w,h];
            fog = new Fog(w, h);

            for(int i=0; i<w; i++)
                for (int e = 0; e < h; e++)
                {
                    map[i, e] = new Tile(Tile.TileType.PLAIN);
                }
        }

        public Tilemap(int w, int h, Tilemap ft)
        {
            cmap = new CityMap(w, h);
            map = new Tile[w, h];
            fog = new Fog(w, h);
            name = ft.name;

            startpos = new Point(ft.startpos.X, ft.startpos.Y);

            for (int i = 0; i < w; i++)
                for (int e = 0; e < h; e++)
                {
                    if (i < ft.NumX && e < ft.NumY)
                    {
                        map[i, e] = ft.get(i, e);
                        fog.set(i, e, ft.Fog.get(i, e));
                        cmap.set(i, e, ft.CityMap.get(i, e));
                    }
                    else if (i < w && e < h)
                    {
                        map[i, e] = new Tile();
                        fog.set(i, e, false);
                    }
                }
        }

        public CityMap CityMap
        {
            get { return cmap; }
        }

        public Fog Fog
        {
            get { return fog; }
        }

        public Tilemap(String fname)
        {
            name = fname;

            load("./map/"+name+".map");
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public Tile get(int x, int y)
        {
            if(x<0||y<0||x>map.GetLength(0)-1||y>map.GetLength(1)-1)
                return null;

            return map[x, y];
        }

        public int NumX
        {
            get { return map.GetLength(0); }
        }

        public int NumY
        {
            get { return map.GetLength(1); }
        }

        public Point StartingPosition
        {
            get { return startpos; }
            set { startpos = value; }
        }

        public static String[] reflist(String path)
        {
            if (!VersionSys.match(path, uid, type, ver))
                throw new Exception("File is not a Forgotten Schism Map file v1.0");

            String[] ret;

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(14, SeekOrigin.Begin); //versionSys(12) + startpos(2)

            ret=Gen.rstra(fin);

            fin.Close();

            return ret;
        }

        public static String[] ownlist(String path)
        {
            if (!VersionSys.match(path, uid, type, ver))
                throw new Exception("File is not a Forgotten Schism Map file v1.0");

            String[] ret;

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(14, SeekOrigin.Begin); //versionSys(12) + startpos(2)

            Gen.rstra(fin);

            ret = Gen.rstra(fin);

            fin.Close();

            return ret;
        }

        public void load(String path)
        {
            if (!VersionSys.match(path, uid, type, ver))
                throw new Exception("File is not a Forgotten Schism Map file v1.0");

            String[] refls;
            String[] ownls;

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(12, SeekOrigin.Begin);

            startpos.X = fin.ReadByte();
            startpos.Y = fin.ReadByte();

            refls = Gen.rstra(fin);
            ownls = Gen.rstra(fin);

            //map width and height
            int w = fin.ReadByte();
            int h = fin.ReadByte();

            map=new Tile[w,h];
            fog = new Fog(w, h);
            cmap = new CityMap(w, h);

            //map data

            int tt;
            String s;
            int rn;
            int on=0;
            int cs=0;

            for (int e = 0; e < h ; e++)
                for (int i = 0; i < w; i++)
                {
                    tt = fin.ReadByte();
                    rn = fin.ReadByte();
                    fog.set(i, e, Gen.conv((byte)fin.ReadByte()));
                    cs = fin.ReadByte();
                    on = fin.ReadByte();
                    
                    if (rn != 0)
                        s = refls[rn - 1];
                    else
                        s = "";

                    map[i, e] = new Tile((Tile.TileType)tt, s);

                    if (cs != 0)
                    {
                        if(on!=0)
                            cmap.set(i, e, new City(s, (City.CitySide)cs, ownls[on-1]));
                        else
                            cmap.set(i, e, new City(s, (City.CitySide)cs, ""));
                    }
                }

            fin.Close();
        }

        public void save(String path)
        {
            List<String> refls = new List<String>();
            List<String> ownls = new List<String>();

            foreach (Tile t in map)
                if (t.RegionName!="" && !refls.Contains(t.RegionName))
                    refls.Add(t.RegionName);

            foreach (City c in cmap.toArray())
                if (c.Owner!="" && !ownls.Contains(c.Owner))
                    ownls.Add(c.Owner);

            FileStream fout = new FileStream(path, FileMode.Create);

            //version system header (Forgotten Shism/Map File/v1.0)
            VersionSys.writeHeader(fout, uid, type, ver);

            fout.WriteByte((byte)startpos.X);
            fout.WriteByte((byte)startpos.Y);

            Gen.wstra(refls.ToArray(), fout);
            Gen.wstra(ownls.ToArray(), fout);

            //width and height
            int w=map.GetLength(0);
            int h = map.GetLength(1);

            fout.WriteByte((byte)w);
            fout.WriteByte((byte)h);

            //map dump

            Tile tmp;

            for (int e = 0; e < h; e++)
                for (int i = 0; i < w; i++)
                {
                    tmp=map[i, e];

                    fout.WriteByte((byte)tmp.Type);

                    if (tmp.RegionName == "")
                        fout.WriteByte(0);
                    else
                        fout.WriteByte((byte)(refls.FindIndex(delegate(String s) { if (s == tmp.RegionName) return true; else return false; }) + 1));

                    fout.WriteByte(Gen.conv(fog.get(i, e)));

                    if (cmap.isCity(i, e))
                    {
                        fout.WriteByte((byte)cmap.get(i, e).Side);
                        fout.WriteByte((byte)(ownls.FindIndex(delegate(String s) { if (s == cmap.get(i, e).Owner) return true; else return false; }) + 1));
                    }
                    else
                    {
                        fout.WriteByte(0);
                        fout.WriteByte(0);
                    }
                }

            fout.Close();
        }
    }
}
