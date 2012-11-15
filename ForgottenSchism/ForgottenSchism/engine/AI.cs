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

        private static bool canMove(CharMap cmap, Tilemap tm, Point dest, String org)
        {
            if (tm.get(dest.X, dest.Y).Type == Tile.TileType.MOUNTAIN || tm.get(dest.X, dest.Y).Type == Tile.TileType.WATER)
                return false;

            return cmap.canMove(dest.X, dest.Y);
        }

        private static Point pathFind(UnitMap umap, Tilemap tm, Point src, Point dest, String org)
        {
            //very stupid algorithm

            if (dest.X < 0 || dest.Y < 0)
                return src;

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

            if (!isDiag(src, d))
            {
                if (canMove(umap, tm, d, org))
                    return d;
                
                return src;
            }
            else
            {
                Point[] mp = XYDir(src, d);

                if (canMove(umap, tm, mp[0], org))
                    return mp[0];

                if (canMove(umap, tm, mp[1], org))
                    return mp[1];

                return src;
            }
        }

        private static Point pathFind(CharMap cmap, Tilemap tm, Point src, Point dest, String org)
        {
            //very stupid algorithm

            if (dest.X < 0 || dest.Y < 0)
                return src;

            if (dest == src)
                return new Point(dest.X, dest.Y);

            Point d = new Point(src.X, src.Y);

            if (dest.X > src.X)
                d.X++;
            else if (dest.X < src.X)
                d.X--;

            if (dest.Y > src.Y)
                d.Y++;
            else if (dest.Y < src.Y)
                d.Y--;

            if (!isDiag(src, d))
            {
                if (canMove(cmap, tm, d, org))
                    return d;

                return src;
            }
            else
            {
                Point[] mp = XYDir(src, d);

                if (canMove(cmap, tm, mp[0], org))
                    return mp[0];

                if (canMove(cmap, tm, mp[1], org))
                    return mp[1];

                return src;
            }
        }

        //gives the 2 points adjacent to src that are adjacent to dest
        private static Point[] XYDir(Point src, Point dest)
        {
            Point[] ret=new Point[2];

            if (dest.X == src.X - 1)
                ret[0] = new Point(dest.X, src.Y);
            else if (dest.X == src.X + 1)
                ret[0] = new Point(dest.X, src.Y);

            if (dest.Y == src.Y - 1)
                ret[1] = new Point(src.X, dest.Y);
            else if (dest.Y == src.Y + 1)
                ret[1] = new Point(src.X, dest.Y);

            return ret;
        }

        private static bool isDiag(Point src, Point dest)
        {
            return ((dest.X == src.X - 1 || dest.X == src.X + 1) && (dest.Y == src.Y - 1 || dest.Y == src.Y + 1));
        }

        private static bool isAdj(Point src, Point dest)
        {
            if (src == dest||isDiag(src, dest))
                return false;

            return (dest.X>=src.X-1&&dest.X<=src.X+1&&dest.Y>=src.Y-1&&dest.Y<=src.Y+1);
        }

        private static bool isOrgPresent(UnitMap umap, Point p, String org)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= umap.NumX || p.Y >= umap.NumY)
                return false;

            if (!umap.isUnit(p.X, p.Y))
                return false;

            return umap.get(p.X, p.Y).Organization == org;
        }

        private static bool isOrgPresent(CharMap cmap, Point p, String org)
        {
            if (p.X < 0 || p.Y < 0 || p.X >= cmap.NumX || p.Y >= cmap.NumY)
                return false;

            if (!cmap.isChar(p.X, p.Y))
                return false;

            return cmap.get(p.X, p.Y).Organization == org;
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

        private static Point nearest(CharMap cmap, Point src, String org)
        {
            if (isOrgPresent(cmap, new Point(src.X, src.Y - 1), org))
                return new Point(src.X, src.Y - 1);

            if (isOrgPresent(cmap, new Point(src.X + 1, src.Y), org))
                return new Point(src.X + 1, src.Y);

            if (isOrgPresent(cmap, new Point(src.X, src.Y + 1), org))
                return new Point(src.X, src.Y + 1);

            if (isOrgPresent(cmap, new Point(src.X - 1, src.Y), org))
                return new Point(src.X - 1, src.Y);

            if (isOrgPresent(cmap, new Point(src.X - 1, src.Y - 1), org))
                return new Point(src.X - 1, src.Y - 1);

            if (isOrgPresent(cmap, new Point(src.X + 1, src.Y - 1), org))
                return new Point(src.X + 1, src.Y - 1);

            if (isOrgPresent(cmap, new Point(src.X - 1, src.Y + 1), org))
                return new Point(src.X - 1, src.Y + 1);

            if (isOrgPresent(cmap, new Point(src.X + 1, src.Y + 1), org))
                return new Point(src.X + 1, src.Y + 1);

            //inner cercle checked

            //very inefficient

            int mr = Gen.max(cmap.NumX, cmap.NumY);

            for (int r = 2; r < mr + 1; r++)
                for (int i = -r; i < r + 1; i++)
                    for (int e = -r; e < r + 1; e++)
                    {
                        if (i != -r && i != r && e != -r && e != r)
                            continue;

                        if (isOrgPresent(cmap, new Point(src.X + i, src.Y + e), org))
                            return new Point(src.X + i, src.Y + e);
                    }

            return new Point(-1, -1);
        }

        public static Unit[] region(UnitMap umap, Tilemap tm, String org)
        {
            Unit u;
            Point ne;
            Point d;

            for(int i=0; i<umap.NumX; i++)
                for(int e=0; e<umap.NumY; e++)
                    if (umap.isUnit(i, e) && umap.get(i, e).movement > 0 && umap.get(i, e).Organization == org)
                    {
                        u = umap.get(i, e);

                        if (u.hasLeader())
                        {
                            ne = nearest(umap, new Point(i, e), "main");

                            if (isAdj(new Point(i, e), ne))
                            {
                                u.movement=0;
                                return new Unit[] { umap.get(ne.X, ne.Y), u };
                            }

                            //finds path to nearest ennemy
                            d=pathFind(umap, tm, new Point(i, e), nearest(umap, new Point(i, e), "main"), org);

                            umap.move(i, e, d.X, d.Y);
                            u.movement--;
                        }
                    }
            return null;
        }

        public static void battle(CharMap cmap, Tilemap tm, String org)
        {
            cmap.resetAllMovement(org);

            Character c;
            Point p;

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isChar(i, e) && cmap.get(i, e).stats.movement > 0 && cmap.get(i, e).Organization == org)
                    {
                        c = cmap.get(i, e);

                        //finds path to nearest ennemy
                        p = pathFind(cmap, tm, new Point(i, e), nearest(cmap, new Point(i, e), "main"), org);

                        if (p != new Point(i, e))
                            cmap.move(i, e, p.X, p.Y);
                        
                        c.stats.movement--;
                    }
        }
    }
}
