using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

using Microsoft.Xna.Framework;

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

        static VersionSys.VersionIdentity vi = new VersionSys.VersionIdentity(new byte[] { 0, 0, 0, 1 }, new byte[] { 0, 1 }, new byte[] { 1, 0 });

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

            //load(".\\map\\"+name+".map");
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

        /// <summary>
        /// Check if the poin is in the boundaries of the Tilemap
        /// </summary>
        /// <param name="p">Point to check</param>
        /// <returns>if the point is in the boundaries of the Tilemap</returns>
        public bool inMap(Point p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X < NumX && p.Y < NumY;
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
            CityMap cmap = new CityMap(1, 1);
            cmap.load(Path.ChangeExtension(path, ".citymap"));

            List<String> refls = new List<string>();

            foreach (City c in cmap.toArray())
                if (c.Name != "" && !refls.Contains(c.Name))
                    refls.Add(c.Name);

            return refls.ToArray();
        }

        public static String[] ownlist(String name)
        {
            return CityMap.ownlist(name);
        }

        public void load(String path)
        {
            name = Path.GetFileNameWithoutExtension(path);

            if (!VersionSys.match(path, vi))
                throw new Exception("File is not a Forgotten Schism Map file v1.0");

            FileStream fin = new FileStream(path, FileMode.Open);

            fin.Seek(12, SeekOrigin.Begin);

            startpos.X = fin.ReadByte();
            startpos.Y = fin.ReadByte();

            //map width and height
            int w = fin.ReadByte();
            int h = fin.ReadByte();

            map=new Tile[w,h];
            fog = new Fog(w, h);

            if(cmap==null)
                cmap = new CityMap(w, h);

            cmap.load(Path.ChangeExtension(path, ".citymap"));

            //map data

            int tt;

            for (int e = 0; e < h ; e++)
                for (int i = 0; i < w; i++)
                {
                    tt = fin.ReadByte();
                    
                    fog.set(i, e, Gen.conv((byte)fin.ReadByte()));
                    
                    map[i, e] = new Tile((Tile.TileType)tt);
                }

            fin.Close();
        }

        public void save(String path)
        {
            name = Path.GetFileNameWithoutExtension(path);

            cmap.save(Path.ChangeExtension(path, ".citymap"));

            FileStream fout = new FileStream(path, FileMode.Create);

            //version system header (Forgotten Shism/Map File/v1.0)
            VersionSys.writeHeader(fout, vi);

            fout.WriteByte((byte)startpos.X);
            fout.WriteByte((byte)startpos.Y);

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

                    fout.WriteByte(Gen.conv(fog.get(i, e)));
                }

            fout.Close();
        }
    }
}
