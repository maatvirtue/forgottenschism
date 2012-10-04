using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenSchism.control
{
    public class Graphic
    {
        public static Texture2D arrowUp(Game1 game, int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(game.graphics.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (x < (w / 2))
                        if (y >= h - x - 1)
                            c = fg;
                        else
                            c = Color.Transparent;
                    else
                        if (y>=x)
                            c = fg;
                        else
                            c = Color.Transparent;
                    
                        

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public static Texture2D arrowDown(Game1 game, int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(game.graphics.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (x < (w / 2))
                        if (y <= x )
                            c = fg;
                        else
                            c = Color.Transparent;
                    else
                        if (y<=w-x)
                            c = fg;
                        else
                            c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public static Texture2D arrowLeft(Game1 game, int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(game.graphics.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;
            int ty;

            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                {
                    if (y < (h / 2))
                        ty = y;
                    else
                        ty = h - y;

                    if ((w-x) <= ty)
                        c = fg;
                    else
                        c = Color.Transparent;

                    color[i] = c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public static Texture2D arrowRight(Game1 game, int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(game.graphics.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w * h];

            Color c;

            int i = 0;
            int ty;

            for (int y = 0; y < h; y++)
                for(int x = 0; x < w; x++)
                {
                    if (y < (h / 2))
                        ty = y;
                    else
                        ty = h - y;

                        if (x <= ty)
                            c = fg;
                        else
                            c = Color.Transparent;

                    color[i]=c;

                    i++;
                }

            t.SetData(color);

            return t;
        }

        public static Texture2D rect(Game1 game, int w, int h, Color c)
        {
            Texture2D rectangleTexture = new Texture2D(game.graphics.GraphicsDevice, w, h, true, SurfaceFormat.Color);
            Color[] color = new Color[w*h];
            
            for (int i = 0; i < color.Length; i++)
            {
                color[i] = c;
            }

            rectangleTexture.SetData(color);
            
            return rectangleTexture;
        }
    }
}
