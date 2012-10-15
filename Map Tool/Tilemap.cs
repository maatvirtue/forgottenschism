using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ForgottenSchism.world
{
    public class Tilemap
    {
        Tile[,] map;
        String name;

        static byte[] uid={0, 0, 0, 1};
        static byte[] type={0, 1};
        static byte[] ver={1, 0};

        public Tilemap()
        {
            int w = 30;
            int h = 20;

            map = new Tile[w, h];

            Random r = new Random();

            Tilemap g = new Tilemap("green");
            Tilemap b = new Tilemap("blue");

            for (int e = 0; e < h; e++)
                for (int i = 0; i < w; i++)
                {
                    map[i, e] = new Tile((Tile.TileType)r.Next(6));

                    if (map[i, e].Type == Tile.TileType.FOREST)
                        map[i, e].Region = g;
                    else if (map[i, e].Type == Tile.TileType.WATER)
                        map[i, e].Region = b;
                }
        }

        public Tilemap(String fname)
        {
            name = fname;

            load(name+".map");
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public Tile get(int x, int y)
        {
            if(x<0||y<0||x>map.GetLength(0)-1||y>map.GetLength(1))
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

        public void load(String path)
        {
            path = "./map/" + path;

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

            //map data

            int tt;
            Tilemap tm;

            for (e = 0; e < h ; e++)
                for (int i = 0; i < w; i++)
                {
                    tt = fin.ReadByte();
                    rn = fin.ReadByte();

                    if (rn != 0)
                    {
                        tm = new Tilemap(refls[rn-1]);
                    }
                    else
                        tm = null;

                    map[i, e] = new Tile((Tile.TileType)tt, tm);
                }

            fin.Close();
        }

        public void save(String path)
        {
            List<String> refls = new List<String>();

            foreach (Tile t in map)
                if (t.Region != null && !refls.Contains(t.Region.Name))
                    refls.Add(t.Region.Name);

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
            fout.WriteByte((byte)map.GetLength(0));
            fout.WriteByte((byte)map.GetLength(1));

            //map dump

            foreach (Tile t in map)
            {
                fout.WriteByte((byte)t.Type);
                if (t.Region == null)
                    fout.WriteByte(0);
                else
                    fout.WriteByte((byte)(refls.FindIndex(delegate(String s) { if (s == t.Region.Name) return true; else return false; })+1));
            }

            fout.Close();
        }
    }
}
