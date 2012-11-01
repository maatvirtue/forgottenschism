using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.control;
using ForgottenSchism.engine;

namespace ForgottenSchism.world
{
    class UnitMap
    {
        List<Unit>[,] umap;

        public UnitMap(int w, int h)
        {
            umap = new List<Unit>[w, h];

            for (int i = 0; i < umap.GetLength(0); i++)
                for (int e = 0; e < umap.GetLength(1); e++)
                    umap[i, e] = new List<Unit>();
        }

        public UnitMap(Tilemap tm)
        {
            umap = new List<Unit>[tm.NumX, tm.NumY];

            for (int i = 0; i < umap.GetLength(0); i++)
                for (int e = 0; e < umap.GetLength(1); e++)
                    umap[i, e] = new List<Unit>();
        }

        public int NumX
        {
            get { return umap.GetLength(0); }
        }

        public int NumY
        {
            get { return umap.GetLength(1); }
        }

        public bool isUnit(int x, int y)
        {
            return umap[x, y].Count > 0;
        }

        public Unit get(int x, int y)
        {
            foreach (Unit u in umap[x, y])
                if (u.hasLeader())
                    return u;

            if (umap[x, y].Count == 0)
                return null;

            return umap[x, y][0];
        }

        public bool canMove(int x, int y, String org)
        {
            if (umap[x, y].Count == 0)
                return true;

            if (get(x, y).Organization != org)
                return false;

            return !get(x, y).hasLeader();
        }

        public void add(int x, int y, Unit u)
        {
            umap[x, y].Add(u);
        }

        public void rem(int x, int y, Unit u)
        {
            umap[x, y].Remove(u);
        }

        public void move(int sx, int sy, int dx, int dy)
        {
            umap[dx, dy].Add(get(sx, sy));
            umap[sx, sy].Remove(get(sx, sy));
        }

        public void move(int sx, int sy, int dx, int dy, Unit u)
        {
            if (!umap[sx, sy].Contains(u))
                return;

            umap[dx, dy].Add(u);
            umap[sx, sy].Remove(u);
        }

        public void update(Map map)
        {
            map.CharLs.Clear();

            for(int i=0; i<umap.GetLength(0); i++)
                for(int e=0; e<umap.GetLength(1); e++)
                    if(umap[i, e].Count>0)
                        map.CharLs.Add(new Microsoft.Xna.Framework.Point(i, e), Graphic.getSprite(get(i, e).Leader));
        }
    }
}

