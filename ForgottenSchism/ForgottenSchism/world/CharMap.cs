using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.engine;
using ForgottenSchism.control;

namespace ForgottenSchism.world
{
    public class CharMap
    {
        Character[,] cmap;

        /// <summary>
        /// Show additionnal stuff when showing character (life, etc)
        /// </summary>
        bool showMisc;

        public CharMap(int w, int h)
        {
            cmap=new Character[w, h];
            showMisc = false;
        }

        public CharMap(Tilemap tm)
        {
            cmap = new Character[tm.NumX, tm.NumY];
            showMisc = false;
        }

        /// <summary>
        /// Show additionnal stuff when showing character (life, etc)
        /// </summary>
        public bool ShowMisc
        {
            set { showMisc = value; }
        }

        public bool isChar(int x, int y)
        {
            if (x < 0 || y < 0 || x >= cmap.GetLength(0) || y >= cmap.GetLength(1))
                return false;
            return cmap[x, y] != null;
        }

        public void set(int x, int y, Character c)
        {
            cmap[x, y] = c;
        }

        public void move(int sx, int sy, int dx, int dy)
        {
            cmap[dx, dy]=cmap[sx, sy];
            cmap[sx, sy] = null;
        }

        public int NumX
        {
            get { return cmap.GetLength(0); }
        }

        public int NumY
        {
            get { return cmap.GetLength(1); }
        }

        public String[] getAllOrg()
        {
            List<String> orgls = new List<string>();

            for (int i = 0; i < cmap.GetLength(0); i++)
                for (int e = 0; e < cmap.GetLength(1); e++)
                    if (cmap[i, e] != null)
                    {
                            if (cmap[i, e].Organization != "" && !orgls.Contains(cmap[i, e].Organization))
                                orgls.Add(cmap[i, e].Organization);
                    }

            return orgls.ToArray();
        }

        public Character get(int x, int y)
        {
            return cmap[x, y];
        }

        public bool canMove(int x, int y)
        {
            return cmap[x, y] == null;
        }

        public void resetAllMovement(String org)
        {

            for (int i = 0; i < cmap.GetLength(0); i++)
                for (int e = 0; e < cmap.GetLength(1); e++)
                    if (cmap[i, e] != null && cmap[i, e].Organization == org)
                        cmap[i, e].stats.movement = cmap[i, e].stats.traits.spd / 10;
        }

        public void update(Map map)
        {
            if (showMisc)
            {
                map.MiscLs.Clear();
            }

            map.CharLs.Clear();

            for(int i=0; i<cmap.GetLength(0); i++)
                for(int e=0; e<cmap.GetLength(1); e++)
                    if (cmap[i, e] != null)
                    {
                        map.CharLs.Add(new Point(i, e), Graphic.getSprite(cmap[i, e]));

                        if (showMisc)
                        {
                            map.MiscLs.Add(new Point(i, e), Graphic.Instance.getMisc(cmap[i, e]));
                        }
                    }
        }
    }
}
