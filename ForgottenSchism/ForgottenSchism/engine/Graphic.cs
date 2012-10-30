using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class Graphic
    {

        static Graphic instance;

        SpriteBatch sb;
        GraphicsDeviceManager gdm;

        private Graphic()
        {
            //
        }

        public static Graphic Instance
        {
            get
            {
                if (instance == null)
                    instance = new Graphic();

                return instance;
            }
        }

        public static void instanciate()
        {
            if (instance == null)
                instance = new Graphic();
        }

        public SpriteBatch SB
        {
            get { return sb; }
            set { sb = value; }
        }

        public GraphicsDeviceManager GDM
        {
            get { return gdm; }
            set { gdm = value; }
        }

        public static Content.Graphics.CachedImage getSprite(Character c)
        {
            if (c.Type == Character.Class_Type.FIGHTER)
                return Content.Graphics.Instance.Images.characters.fighter;
            else if (c.Type == Character.Class_Type.HEALER)
                return Content.Graphics.Instance.Images.characters.healer;
            else if (c.Type == Character.Class_Type.ARCHER)
                return Content.Graphics.Instance.Images.characters.archer;
            else if (c.Type == Character.Class_Type.CASTER)
                return Content.Graphics.Instance.Images.characters.caster;
            else if (c.Type == Character.Class_Type.SCOUT)
                return Content.Graphics.Instance.Images.characters.scout;
            else
                return Content.Graphics.Instance.Images.characters.healer;
        }

        public Texture2D arrowUp(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
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

        public Texture2D arrowDown(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
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

        public Texture2D arrowLeft(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
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

        public Texture2D arrowRight(int w, int h, Color fg)
        {
            Texture2D t = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
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

        public Texture2D rect(int w, int h, Color c)
        {
            Texture2D rectangleTexture = new Texture2D(gdm.GraphicsDevice, w, h, true, SurfaceFormat.Color);
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
