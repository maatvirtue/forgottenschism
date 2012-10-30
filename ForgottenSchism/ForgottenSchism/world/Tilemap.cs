using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    public class Tilemap
    {
        Tile[,] map;
        Fog fog;
        String name;

        static byte[] uid={0, 0, 0, 1};
        static byte[] type={0, 1};
        static byte[] ver={1, 0};

        public Tilemap(int w, int h)
        {
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
            map = new Tile[w, h];
            fog = ft.Fog;

            for (int i = 0; i < w; i++)
                for (int e = 0; e < h; e++)
                {
                    if (i < ft.NumX && e < ft.NumY)
                        map[i, e] = ft.get(i, e);
                    else if(i<w&&e<h)
                        map[i, e] = new Tile();
                }
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

        public static String[] reflist(String path)
        {
            if (!VersionSys.match(path, uid, type, ver))
                throw new Exception("File is not a Forgotten Schism Map file v1.0");

            List<String> refls = new List<String>();

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(12, SeekOrigin.Begin);

            int rn = fin.ReadByte();
            int sl;
            char[] sb;
            int e;

            for (int i = 0; i < rn; i++)
            {
                sl = fin.ReadByte();
                sb = new char[sl];

                for (e = 0; e < sl; e++)
                    sb[e] = (char)fin.ReadByte();

                refls.Add(new String(sb));
            }

            fin.Close();

            return refls.ToArray();
        }

        public void load(String path)
        {
            if (!VersionSys.match(path, uid, type, ver))
                throw new Exception("File is not a Forgotten Schism Map file v1.0");

            List<String> refls=new List<String>();

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(12, SeekOrigin.Begin);

            int rn = fin.ReadByte();
            int sl;
            char[] sb;
            int e;

            for (int i = 0; i < rn; i++)
            {
                sl = fin.ReadByte();
                sb=new char[sl];

                for (e = 0; e < sl; e++)
                    sb[e] = (char)fin.ReadByte();

                refls.Add(new String(sb));
            }

            //map width and height
            int w = fin.ReadByte();
            int h = fin.ReadByte();

            map=new Tile[w,h];
            fog = new Fog(w, h);

            //map data

            int tt;
            String s;

            for (e = 0; e < h ; e++)
                for (int i = 0; i < w; i++)
                {
                    tt = fin.ReadByte();
                    rn = fin.ReadByte();
                    fog.set(i, e, Gen.conv((byte)fin.ReadByte()));
                    
                    if (rn != 0)
                        s = refls[rn - 1];
                    else
                        s = "";

                    map[i, e] = new Tile((Tile.TileType)tt, s);
                }

            fin.Close();
        }

        public void save(String path)
        {
            List<String> refls = new List<String>();

            foreach (Tile t in map)
                if (t.RegionName!="" && !refls.Contains(t.RegionName))
                    refls.Add(t.RegionName);

            FileStream fout = new FileStream(path, FileMode.Create);

            //version system header (Forgotten Shism/Map File/v1.0)
            VersionSys.writeHeader(fout, uid, type, ver);

            fout.WriteByte((byte)refls.Count);

            foreach (String str in refls)
            {
                fout.WriteByte((byte)str.Length);
                foreach (char c in str.ToCharArray())
                    fout.WriteByte((byte)c);
            }

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
                }

            fout.Close();
        }
    }
}
