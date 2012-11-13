using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    class AI
    {
        private static bool canMove(UnitMap umap, Tilemap tm, Point dest, String org)
        {
            if (tm.get(dest.X, dest.Y).Type == Tile.TileType.MOUNTAIN || tm.get(dest.X, dest.Y).Type == Tile.TileType.WATER)
                return false;

            return umap.canMove(dest.X, dest.Y, org);
        }

        private static Point pathFind(UnitMap umap, Tilemap tm, Unit u, Point src, Point dest, String org)
        {
            //very stupid algorithm

            if(dest==src)
                return new Point(dest.X, dest.Y);

            Point d=new Point(src.X, src.Y);

            if (dest.X > src.X)
                d.X++;
            else if (dest.X < src.X)
                d.X--;

            if (dest.Y > src.Y)
                d.Y++;
            else if (dest.Y < src.Y)
                d.Y--;

            if (canMove(umap, tm, d, org))
                return d;
            else
                return src;
        }

        private static bool isOrgPresent(UnitMap umap, Point p, String org)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= umap.NumX || p.Y >= umap.NumY)
                return false;

            if (!umap.isUnit(p.X, p.Y))
                return false;

            return umap.get(p.X, p.Y).Organization == org;
        }

        private static Point nearest(UnitMap umap, Point src, String org)
        {
            if (isOrgPresent(umap, new Point(src.X, src.Y - 1), org))
                return new Point(src.X, src.Y - 1);

            if (isOrgPresent(umap, new Point(src.X+1, src.Y), org))
                return new Point(src.X+1, src.Y);

            if (isOrgPresent(umap, new Point(src.X, src.Y + 1), org))
                return new Point(src.X, src.Y + 1);

            if (isOrgPresent(umap, new Point(src.X-1, src.Y), org))
                return new Point(src.X-1, src.Y);

            if (isOrgPresent(umap, new Point(src.X-1, src.Y - 1), org))
                return new Point(src.X-1, src.Y - 1);

            if (isOrgPresent(umap, new Point(src.X+1, src.Y - 1), org))
                return new Point(src.X+1, src.Y - 1);

            if (isOrgPresent(umap, new Point(src.X-1, src.Y + 1), org))
                return new Point(src.X-1, src.Y + 1);

            if (isOrgPresent(umap, new Point(src.X+1, src.Y + 1), org))
                return new Point(src.X+1, src.Y + 1);

            //inner cercle checked

            //very inefficient

            int mr = Gen.max(umap.NumX, umap.NumY);

            for (int r = 2; r < mr + 1; r++)
                for (int i = -r; i < r + 1; i++)
                    for (int e = -r; e < r + 1; e++)
                    {
                        if (i != -r && i != r && e != -r && e != r)
                            continue;

                        if (isOrgPresent(umap, new Point(src.X + i, src.Y + e), org))
                            return new Point(src.X + i, src.Y + e);
                    }

            return new Point(-1, -1);
        }

        public static void region(UnitMap umap, Tilemap tm, String org)
        {
            //
        }
    }
}
