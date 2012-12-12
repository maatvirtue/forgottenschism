using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;
using ForgottenSchism.screen;
using ForgottenSchism.control;

namespace ForgottenSchism.engine
{
    class AI
    {
        private struct PointCounter
        {
            public Point p;
            public int c;

            public PointCounter(Point fp, int fc)
            {
                p = fp;
                c = fc;
            }

            public PointCounter(int x, int y, int fc)
            {
                p = new Point(x, y);
                c = fc;
            }
        }

        private static bool canMove(UnitMap umap, Tilemap tm, Point dest, String org)
        {
            if (tm.get(dest.X, dest.Y).Type == Tile.TileType.MOUNTAIN || tm.get(dest.X, dest.Y).Type == Tile.TileType.WATER)
                return false;

            return umap.canMove(dest.X, dest.Y, org);
        }

        private static bool canMove(Tilemap tm, Point dest, String org)
        {
            return !(tm.get(dest.X, dest.Y).Type == Tile.TileType.MOUNTAIN || tm.get(dest.X, dest.Y).Type == Tile.TileType.WATER);
        }

        private static bool canMove(CharMap cmap, Tilemap tm, Point dest)
        {
            if (tm.get(dest.X, dest.Y).Type == Tile.TileType.MOUNTAIN || tm.get(dest.X, dest.Y).Type == Tile.TileType.WATER)
                return false;

            return cmap.canMove(dest.X, dest.Y);
        }

        private static bool inMap(Tilemap tm, Point p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X < tm.NumX && p.Y < tm.NumY;
        }

        private static Point pathFindFallBack(UnitMap umap, Tilemap tm, Point src, Point dest, String org)
        {
            Dictionary<Point, int> map = new Dictionary<Point, int>();
            Queue<PointCounter> main = new Queue<PointCounter>();
            Queue<PointCounter> temp = new Queue<PointCounter>();
            PointCounter cur;
            PointCounter tcur;

            main.Enqueue(new PointCounter(dest, 0));
            map[dest] = 0;

            int cc;
            bool f = false;

            while (main.Count > 0)
            {
                cur = main.Dequeue();
                temp.Clear();

                if (cur.p == src)
                {
                    f = true;
                    break;
                }

                cc = cur.c + 1;

                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y - 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X + 1, cur.p.Y, cc));
                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y + 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X - 1, cur.p.Y, cc));

                while (temp.Count > 0)
                {
                    tcur = temp.Dequeue();

                    if (tcur.p != src)
                    {
                        if (!inMap(tm, tcur.p) || !canMove(tm, tcur.p, org))
                            continue;

                        if (map.ContainsKey(tcur.p) && map[tcur.p] <= tcur.c)
                            continue;
                    }

                    map[tcur.p] = tcur.c;
                    main.Enqueue(tcur);
                }
            }

            if (!f)
                return src;

            Point ret = src;
            cc = map[src];

            temp.Clear();

            temp.Enqueue(new PointCounter(src.X, src.Y - 1, 0));
            temp.Enqueue(new PointCounter(src.X + 1, src.Y, 0));
            temp.Enqueue(new PointCounter(src.X, src.Y + 1, 0));
            temp.Enqueue(new PointCounter(src.X - 1, src.Y, 0));

            while (temp.Count > 0)
            {
                tcur = temp.Dequeue();

                if (map.ContainsKey(tcur.p) && map[tcur.p] < cc)
                {
                    cc = map[tcur.p];
                    ret = tcur.p;
                }
            }

            if (!canMove(umap, tm, ret, org))
                return src;

            return ret;
        }

        private static Point pathFindFallBack(CharMap cmap, Tilemap tm, Point src, Point dest, String org)
        {
            Dictionary<Point, int> map = new Dictionary<Point, int>();
            Queue<PointCounter> main = new Queue<PointCounter>();
            Queue<PointCounter> temp = new Queue<PointCounter>();
            PointCounter cur;
            PointCounter tcur;

            main.Enqueue(new PointCounter(dest, 0));
            map[dest] = 0;

            int cc;
            bool f = false;

            while (main.Count > 0)
            {
                cur = main.Dequeue();
                temp.Clear();

                if (cur.p == src)
                {
                    f = true;
                    break;
                }

                cc = cur.c + 1;

                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y - 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X + 1, cur.p.Y, cc));
                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y + 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X - 1, cur.p.Y, cc));

                while (temp.Count > 0)
                {
                    tcur = temp.Dequeue();

                    if (tcur.p != src)
                    {
                        if (!inMap(tm, tcur.p) || !canMove(tm, tcur.p, org))
                            continue;

                        if (map.ContainsKey(tcur.p) && map[tcur.p] <= tcur.c)
                            continue;
                    }

                    map[tcur.p] = tcur.c;
                    main.Enqueue(tcur);
                }
            }

            if (!f)
                return src;

            Point ret = src;
            cc = map[src];

            temp.Clear();

            temp.Enqueue(new PointCounter(src.X, src.Y - 1, 0));
            temp.Enqueue(new PointCounter(src.X + 1, src.Y, 0));
            temp.Enqueue(new PointCounter(src.X, src.Y + 1, 0));
            temp.Enqueue(new PointCounter(src.X - 1, src.Y, 0));

            while (temp.Count > 0)
            {
                tcur = temp.Dequeue();

                if (map.ContainsKey(tcur.p) && map[tcur.p] < cc)
                {
                    cc = map[tcur.p];
                    ret = tcur.p;
                }
            }

            if (!canMove(cmap, tm, ret))
                return src;

            return ret;
        }

        private static Point pathFind(UnitMap umap, Tilemap tm, Point src, Point dest, String org)
        {
            Dictionary<Point, int> map=new Dictionary<Point,int>();
            Queue<PointCounter> main = new Queue<PointCounter>();
            Queue<PointCounter> temp = new Queue<PointCounter>();
            PointCounter cur;
            PointCounter tcur;

            main.Enqueue(new PointCounter(dest, 0));
            map[dest] = 0;

            int cc;
            bool f = false;

            while (main.Count > 0)
            {
                cur = main.Dequeue();
                temp.Clear();

                if (cur.p == src)
                {
                    f = true;
                    break;
                }

                cc = cur.c + 1;

                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y - 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X+1, cur.p.Y, cc));
                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y + 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X-1, cur.p.Y, cc));

                while (temp.Count > 0)
                {
                    tcur = temp.Dequeue();

                    if (tcur.p != src)
                    {
                        if (!inMap(tm, tcur.p) || !canMove(umap, tm, tcur.p, org))
                            continue;

                        if (map.ContainsKey(tcur.p) && map[tcur.p] <= tcur.c)
                            continue;
                    }

                    map[tcur.p] = tcur.c;
                    main.Enqueue(tcur);
                }
            }

            if (!f)
                return pathFindFallBack(umap, tm, src, dest, org);

            Point ret=src;
            cc = map[src];

            temp.Clear();

            temp.Enqueue(new PointCounter(src.X, src.Y - 1, 0));
            temp.Enqueue(new PointCounter(src.X + 1, src.Y, 0));
            temp.Enqueue(new PointCounter(src.X, src.Y + 1, 0));
            temp.Enqueue(new PointCounter(src.X - 1, src.Y, 0));

            while (temp.Count > 0)
            {
                tcur = temp.Dequeue();

                if (map.ContainsKey(tcur.p) && map[tcur.p] < cc)
                {
                    cc = map[tcur.p];
                    ret = tcur.p;
                }
            }

            return ret;
        }

        private static Point pathFind(CharMap cmap, Tilemap tm, Point src, Point dest, String org)
        {
            Dictionary<Point, int> map = new Dictionary<Point, int>();
            Queue<PointCounter> main = new Queue<PointCounter>();
            Queue<PointCounter> temp = new Queue<PointCounter>();
            PointCounter cur;
            PointCounter tcur;

            main.Enqueue(new PointCounter(dest, 0));
            map[dest] = 0;

            int cc;
            bool f = false;

            while (main.Count > 0)
            {
                cur = main.Dequeue();
                temp.Clear();

                if (cur.p == src)
                {
                    f = true;
                    break;
                }

                cc = cur.c + 1;

                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y - 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X + 1, cur.p.Y, cc));
                temp.Enqueue(new PointCounter(cur.p.X, cur.p.Y + 1, cc));
                temp.Enqueue(new PointCounter(cur.p.X - 1, cur.p.Y, cc));

                while (temp.Count > 0)
                {
                    tcur = temp.Dequeue();

                    if (tcur.p != src)
                    {
                        if (!inMap(tm, tcur.p) || !canMove(cmap, tm, tcur.p))
                            continue;

                        if (map.ContainsKey(tcur.p) && map[tcur.p] <= tcur.c)
                            continue;
                    }

                    map[tcur.p] = tcur.c;
                    main.Enqueue(tcur);
                }
            }

            if (!f)
                return pathFindFallBack(cmap, tm, src, dest, org);

            Point ret = src;
            cc = map[src];

            temp.Clear();

            temp.Enqueue(new PointCounter(src.X, src.Y - 1, 0));
            temp.Enqueue(new PointCounter(src.X + 1, src.Y, 0));
            temp.Enqueue(new PointCounter(src.X, src.Y + 1, 0));
            temp.Enqueue(new PointCounter(src.X - 1, src.Y, 0));

            while (temp.Count > 0)
            {
                tcur = temp.Dequeue();

                if (map.ContainsKey(tcur.p) && map[tcur.p] < cc)
                {
                    cc = map[tcur.p];
                    ret = tcur.p;
                }
            }

            return ret;
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

        private static Point targetCharacter(Character c, Point p, CharMap cmap)
        {
            bool targetable = false;
            bool castable = false;
            List<Point> targetableChar = new List<Point>();
            Point target = new Point(-1, -1);

            if (c is Fighter || c is Scout)
            {
                if (cmap.isChar(p.X - 1, p.Y) && cmap.get(p.X - 1, p.Y).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 1) && cmap.get(p.X, p.Y - 1).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y) && cmap.get(p.X + 1, p.Y).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 1) && cmap.get(p.X, p.Y + 1).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 1));
                }
            }
            else if (c is Archer)
            {
                if (cmap.isChar(p.X - 1, p.Y + 1) && cmap.get(p.X - 1, p.Y + 1).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y + 1));
                }
                if (cmap.isChar(p.X - 1, p.Y - 1) && cmap.get(p.X - 1, p.Y - 1).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y + 1) && cmap.get(p.X + 1, p.Y + 1).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y + 1));
                }
                if (cmap.isChar(p.X + 1, p.Y - 1) && cmap.get(p.X + 1, p.Y - 1).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y - 1));
                }
                if (cmap.isChar(p.X - 2, p.Y) && cmap.get(p.X - 2, p.Y).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 2) && cmap.get(p.X, p.Y - 2).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 2));
                }
                if (cmap.isChar(p.X + 2, p.Y) && cmap.get(p.X + 2, p.Y).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 2) && cmap.get(p.X, p.Y + 2).Organization == "main")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 2));
                }
            }
            else if (c is Healer)
            {
                if (cmap.isChar(p.X - 1, p.Y) && cmap.get(p.X - 1, p.Y).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 1) && cmap.get(p.X, p.Y - 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y) && cmap.get(p.X + 1, p.Y).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 1) && cmap.get(p.X, p.Y + 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 1));
                }
            }
            else if (c is Caster)
            {
                if (cmap.isChar(p.X - 1, p.Y) && cmap.get(p.X - 1, p.Y).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 1) && cmap.get(p.X, p.Y - 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y) && cmap.get(p.X + 1, p.Y).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 1) && cmap.get(p.X, p.Y + 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 1));
                }
                if (cmap.isChar(p.X - 1, p.Y + 1) && cmap.get(p.X - 1, p.Y + 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y + 1));
                }
                if (cmap.isChar(p.X - 1, p.Y - 1) && cmap.get(p.X - 1, p.Y - 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y + 1) && cmap.get(p.X + 1, p.Y + 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y + 1));
                }
                if (cmap.isChar(p.X + 1, p.Y - 1) && cmap.get(p.X + 1, p.Y - 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y - 1));
                }
                if (cmap.isChar(p.X - 2, p.Y) && cmap.get(p.X - 2, p.Y).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 2) && cmap.get(p.X, p.Y - 2).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 2));
                }
                if (cmap.isChar(p.X + 2, p.Y) && cmap.get(p.X + 2, p.Y).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 2) && cmap.get(p.X, p.Y + 2).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 2));
                }
            }

            if (targetable)
            {
                foreach (Point pt in targetableChar)
                {
                    if (target == new Point(-1, -1) || cmap.get(pt.X, pt.Y).stats.hp < cmap.get(target.X, target.Y).stats.hp)
                        target = pt;
                }
            }

            if (castable)
            {
                foreach (Point pt in targetableChar)
                {
                    if (target == new Point(-1, -1) || cmap.get(pt.X, pt.Y).stats.hp < cmap.get(target.X, target.Y).stats.hp)
                        target = pt;
                }
            }

            return target;
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

        public static Boolean battle(CharMap cmap, Tilemap tm, String org, Map map, Unit ally, Unit enemy, Label dmg)
        {
            Character c;
            Character m;
            Point p;
            Point ne;

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isChar(i, e) && cmap.get(i, e).stats.movement > 0 && cmap.get(i, e).Organization == org)
                    {
                        if (map.CursorPosition != new Point(i, e))
                        {
                            map.CurLs.Clear();
                            dmg.Visible = false;
                            map.changeCursor(new Point(i, e));
                            return false;
                        }
                        c = cmap.get(i, e);

                        ne = nearest(cmap, new Point(i, e), "main");

                        if (isAdj(new Point(i, e), ne))
                        {
                            c.stats.movement = 0;
                            p = new Point(i, e);
                        }
                        else
                        {
                            //finds path to nearest ennemy
                            p = pathFind(cmap, tm, new Point(i, e), nearest(cmap, new Point(i, e), "main"), org);
                            Console.WriteLine(i + " " + e + " - " + p.X + " " + p.Y);
                            if (p != new Point(i, e))
                            {
                                cmap.move(i, e, p.X, p.Y);
                                map.changeCursor(new Point(p.X, p.Y));
                            }
                                c.stats.movement--;
                        }

                         Point tar = targetCharacter(c, p, cmap);

                         if (tar != new Point(-1, -1))
                         {
                             m = cmap.get(tar.X, tar.Y);
                             map.CurLs.Add(tar, Content.Graphics.Instance.Images.gui.cursorRed);

                             if (c is Fighter)
                                 dmg.Text = ((Fighter)c).attack(m);
                             else if (c is Archer)
                                 dmg.Text = ((Archer)c).attack(m);
                             else if (c is Scout)
                                 dmg.Text = ((Scout)c).attack(m);
                             else if (c is Healer)
                                 dmg.Text = ((Healer)c).heal(m).ToString();
                             else if (c is Caster)
                                 dmg.Text = ((Caster)c).attack(m, new Spell("DerpCast"));
                             else
                                 dmg.Text = "Cant";

                             dmg.Position = new Vector2(tar.X * 64 - map.getTlc.X * 64, tar.Y * 64 - map.getTlc.Y * 64 + 20);
                             dmg.Visible = true;

                             if (dmg.Text != "miss" || dmg.Text != "Cant")
                             {
                                 enemy.set(c.Position.X, c.Position.Y, c);
                                 ally.set(m.Position.X, m.Position.Y, m);
                                 if (m.stats.hp <= 0)
                                 {
                                     if (m.isMainChar())
                                         StateManager.Instance.goForward(new GameOver());

                                     ally.delete(m.Position.X, m.Position.Y);
                                     cmap.set(tar.X, tar.Y, null);
                                     cmap.update(map);

                                     if (ally.Characters.Count <= 0)
                                         StateManager.Instance.goBack();
                                 }
                             }
                         }
                         map.changeCursor(new Point(p.X, p.Y));
                         return false;
                    }

            cmap.resetAllMovement(org);
            return true;
        }
    }
}
