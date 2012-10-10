using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenSchism.engine
{
    public class Graphic
    {
        public class Content
        {
            public struct SImages
            {
                public struct SGUI
                {
                    public CachedImage cursor;
                    public CachedImage selCursor;
                }

                public struct SBG
                {
                    public CachedImage black;
                }

                public struct SCharacters
                {
                    public CachedImage healer;
                }

                public SCharacters characters;
                public SGUI gui;
                public SBG background;
            }

            public abstract class CachedImage
            {
                protected Texture2D img;

                public Texture2D Image
                {
                    get { return img; }
                    set { img = value; }
                }
            }

            private class CachedImageInst : CachedImage
            {
                public CachedImageInst(Texture2D t)
                {
                    img = t;
                }
            }

            static Content instance;
            SpriteFont defFont;
            SpriteFont monoFont;
            SImages images;
            CachedImage testimg;

            private Content()
            {
                loadContent();
            }

            public static Content Instance
            {
                get
                {
                    if (instance == null)
                        instance = new Content();

                    return instance;
                }
            }

            public SImages Images
            {
                get { return images; }
            }

            public SpriteFont DefaultFont
            {
                get { return defFont; }
            }

            public SpriteFont MonoFont
            {
                get { return monoFont; }
            }

            public CachedImage TestImage
            {
                get { return testimg; }
            }

            public static void instanciate()
            {
                if (instance == null)
                    instance = new Content();
            }

            private void loadContent()
            {
                defFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\arial12norm");
                monoFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\mono12norm");

                testimg =new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\test"));

                images.background.black = new CachedImageInst(Graphic.Instance.rect(Graphic.Instance.GDM.PreferredBackBufferWidth, Graphic.Instance.GDM.PreferredBackBufferHeight, Color.Black));

                images.gui.cursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\cur"));
                images.gui.selCursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\sel"));

                images.characters.healer = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\healer"));
            }
        }

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
