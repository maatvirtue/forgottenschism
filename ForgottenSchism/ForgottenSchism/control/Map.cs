using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.world;
using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class Map: Control
    {
        Tilemap tm;
        static Dictionary<Tile.TileType, Texture2D> tbuf;
        static Texture2D tcur;
        static Texture2D tsel;
        static int tw;
        static int th;
        int nx;
        int ny;
        Vector2 tlc;
        Vector2 curp;
        Vector2 sel;

        public EventHandler changeRegion;

        public Map(Tilemap ftm)
        {
            tm = ftm;
            TabStop = true;
            nx = Graphic.Instance.GDM.PreferredBackBufferWidth / 64;
            ny = 6;
            tlc = new Vector2(0, 0);
            curp = new Vector2(nx/2, ny/2);
            sel = new Vector2(-1, -1);

            tcur = Graphic.Content.Instance.Images.gui.cursor.Image;
            tsel = Graphic.Content.Instance.Images.gui.selCursor.Image;

            loadContent();
        }

        public Map(Tilemap ftm, int fnx, int fny)
        {
            tm = ftm;
            TabStop = true;
            nx = fnx;
            ny = fny;
            tlc = new Vector2(0, 0);
            curp = new Vector2(nx / 2, ny / 2);
            sel = new Vector2(-1, -1);

            tcur = Graphic.Content.Instance.Images.gui.cursor.Image;
            tsel = Graphic.Content.Instance.Images.gui.selCursor.Image;

            loadContent();
        }

        public int NumX
        {
            get { return nx; }
            set { nx = value; }
        }

        public int NumY
        {
            get { return ny; }
            set { ny = value; }
        }

        private void loadContent()
        {
            if (tbuf == null)
            {
                tbuf = new Dictionary<Tile.TileType, Texture2D>();

                tw = 64;
                th = 64;

                if(tm!=null)
                    Size = new Vector2(tm.NumX*tw, tm.NumY*th);

                tbuf.Add(Tile.TileType.PLAIN, Graphic.Instance.rect(tw, th, Color.Beige));
                tbuf.Add(Tile.TileType.WATER, Graphic.Instance.rect(tw, th, Color.Blue));
                tbuf.Add(Tile.TileType.MOUNTAIN, Graphic.Instance.rect(tw, th, Color.Brown));
                tbuf.Add(Tile.TileType.FOREST, Graphic.Instance.rect(tw, th, Color.Green));
                tbuf.Add(Tile.TileType.ROADS, Graphic.Instance.rect(tw, th, Color.Gray));
                tbuf.Add(Tile.TileType.CITY, Graphic.Content.Instance.Images.characters.healer.Image);
            }
        }

        public void load(Tilemap ftm)
        {
            tm = ftm;

            if (tm != null)
                Size = new Vector2(tm.NumX * tw, tm.NumY * th);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            for (int e = 0; e < ny; e++)
                for (int i = 0; i < nx; i++)
                    Graphic.Instance.SB.Draw(tbuf[tm.get((int)(i + tlc.X), (int)(e + tlc.Y)).Type], new Rectangle((int)Position.X + (i * tw), (int)Position.Y + (e * th), tw, th), Color.White);

            Graphic.Instance.SB.Draw(tcur, new Rectangle((int)(Position.X + (curp.X * tw)), (int)(Position.Y + (curp.Y * th)), tw, th), Color.White);

            if(sel.X>=tlc.X&&sel.Y>=tlc.Y&&sel.X<=(tlc.X+nx-1)&&sel.Y<=(tlc.Y+ny-1))
                Graphic.Instance.SB.Draw(tsel, new Rectangle((int)(Position.X + ((sel.X - tlc.X) * tw)), (int)(Position.Y + ((sel.Y - tlc.Y) * th)), tw, th), Color.White);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Up))
                if (curp.Y != 0)
                    curp.Y--;
                else if (tlc.Y != 0)
                    tlc.Y--;

            if (InputHandler.keyReleased(Keys.Down))
                if (curp.Y != ny-1)
                    curp.Y++;
                else if (tlc.Y+ny != tm.NumY-1)
                    tlc.Y++;

            if (InputHandler.keyReleased(Keys.Left))
                if (curp.X != 0)
                    curp.X--;
                else if (tlc.X != 0)
                    tlc.X--;

            if (InputHandler.keyReleased(Keys.Right))
                if (curp.X != nx-1)
                    curp.X++;
                else if (tlc.X+nx != tm.NumX-1)
                    tlc.X++;

            if (InputHandler.keyReleased(Keys.Enter))
            {
                sel = new Vector2(curp.X + tlc.X, curp.Y + tlc.Y);

                if (tm.get((int)sel.X, (int)sel.Y).Region != null && changeRegion != null)
                    changeRegion(tm.get((int)sel.X, (int)sel.Y).Region, null);
            }
        }
    }
}
