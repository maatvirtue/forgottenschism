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

        public CharMap(int w, int h)
        {
            cmap=new Character[w, h];
        }

        public CharMap(Tilemap tm)
        {
            cmap = new Character[tm.NumX, tm.NumY];
        }

        public bool isChar(int x, int y)
        {
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

        public Character get(int x, int y)
        {
            return cmap[x, y];
        }

        public void update(Map map)
        {
            map.CharLs.Clear();

            for(int i=0; i<cmap.GetLength(0); i++)
                for(int e=0; e<cmap.GetLength(1); e++)
                    if(cmap[i, e]!=null)
                        map.CharLs.Add(new Point(i, e), Graphic.getSprite(cmap[i, e])); 
        }
    }
}
