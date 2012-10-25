using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class Content
    {
        public class Graphics
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
                    public CachedImage fighter;
                }

                public Dictionary<Tile.TileType, CachedImage> tiles;
                public CachedImage fog;
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

            static Graphics instance;

            SpriteFont defFont;
            SpriteFont monoFont;
            SImages images;
            CachedImage testimg;

            private Graphics()
            {
                loadContent();
            }

            public static Graphics Instance
            {
                get
                {
                    if (instance == null)
                        instance = new Graphics();

                    return instance;
                }
            }

            public static void instantiate()
            {
                if (instance == null)
                    instance = new Graphics();
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

            private void loadContent()
            {
                defFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\arial12norm");
                monoFont = Game1.Instance.Content.Load<SpriteFont>(@"font\\mono12norm");

                testimg =new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\test"));

                images.background.black = new CachedImageInst(Graphic.Instance.rect(Graphic.Instance.GDM.PreferredBackBufferWidth, Graphic.Instance.GDM.PreferredBackBufferHeight, Color.Black));

                images.gui.cursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\cur"));
                images.gui.selCursor = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\gui\\sel"));

                images.characters.healer = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\healer"));
                images.characters.fighter = new CachedImageInst(Game1.Instance.Content.Load<Texture2D>(@"img\\char\\healer"));
                
                images.tiles = new Dictionary<Tile.TileType, CachedImage>();
                
                images.tiles.Add(Tile.TileType.CITY, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.White)));
                images.tiles.Add(Tile.TileType.FOREST, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Green)));
                images.tiles.Add(Tile.TileType.MOUNTAIN, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Brown)));
                images.tiles.Add(Tile.TileType.PLAIN, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Yellow)));
                images.tiles.Add(Tile.TileType.ROADS, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Gray)));
                images.tiles.Add(Tile.TileType.WATER, new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Blue)));
                images.fog = new CachedImageInst(Graphic.Instance.rect(64, 64, Color.Black));
            }
        }

        public struct Regions
        {
            //
        }

        static Content instance;
        
        public Tilemap gen;
        public Regions regions;

        private Content()
        {
            loadContent();
        }

        public static  Content Instance
        {
            get
            {
                if (instance == null)
                    instance = new Content();

                return instance;
            }
        }

        public static void instantiate()
        {
            if (instance == null)
                instance = new Content();
        }

        private void loadContent()
        {
            Graphics.instantiate();

            gen = new Tilemap("gen");
        }
    }
}
