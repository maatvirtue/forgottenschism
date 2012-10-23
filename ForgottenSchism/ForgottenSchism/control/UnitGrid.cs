using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.world;
using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class UnitGrid: Control
    {
        //add the others
        Graphic.Content.CachedImage fighter;
        Unit unit;
        static Texture2D tcur;
        static Texture2D tsel;
        static int TW=64;
        static int TH=64;
        int nx;
        int ny;
        Vector2 tlc;
        Vector2 curp;
        Vector2 sel;

        public EventHandler changeRegion;

        public UnitGrid(Unit funit)
        {
            nx = Graphic.Instance.GDM.PreferredBackBufferWidth / 64;
            ny = 6;

            init(funit);
        }

        public UnitGrid(Unit funit, int fnx, int fny)
        {
            nx = fnx;
            ny = fny;

            init(funit);
        }

        private void init(Unit funit)
        {
            unit = funit;
            TabStop = true;
            tlc = new Vector2(0, 0);
            curp = new Vector2(nx / 2, ny / 2);
            sel = new Vector2(-1, -1);

            tcur = Graphic.Content.Instance.Images.gui.cursor.Image;
            tsel = Graphic.Content.Instance.Images.gui.selCursor.Image;

            //change this
            fighter = Graphic.Content.Instance.Images.characters.healer;

            if (unit != null)
                //Size = new Vector2(unit.Characters * TW, tm.NumY * TH);
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

        public void load(Tilemap ftm)
        {
            tm = ftm;

            if (tm != null)
                Size = new Vector2(tm.NumX * TW, tm.NumY * TH);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            for (int e = 0; e < ny; e++)
                for (int i = 0; i < nx; i++)
                    Graphic.Instance.SB.Draw(tbuf[tm.get((int)(i + tlc.X), (int)(e + tlc.Y)).Type].Image, new Rectangle((int)Position.X + (i * TW), (int)Position.Y + (e * TH), TW, TH), Color.White);

            Graphic.Instance.SB.Draw(tcur, new Rectangle((int)(Position.X + (curp.X * TW)), (int)(Position.Y + (curp.Y * TH)), TW, TH), Color.White);

            if(sel.X>=tlc.X&&sel.Y>=tlc.Y&&sel.X<=(tlc.X+nx-1)&&sel.Y<=(tlc.Y+ny-1))
                Graphic.Instance.SB.Draw(tsel, new Rectangle((int)(Position.X + ((sel.X - tlc.X) * TW)), (int)(Position.Y + ((sel.Y - tlc.Y) * TH)), TW, TH), Color.White);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if(InputHandler.keyReleased(Keys.Up))
                if (curp.Y != 0)
                    curp.Y--;
                else if (tlc.Y != 0)
                    tlc.Y--;

            if(InputHandler.keyReleased(Keys.Down))
                if (curp.Y != ny-1)
                    curp.Y++;
                else if (tlc.Y+ny < tm.NumY-1)
                    tlc.Y++;

            if(InputHandler.keyReleased(Keys.Left))
                if (curp.X != 0)
                    curp.X--;
                else if (tlc.X != 0)
                    tlc.X--;

            if(InputHandler.keyReleased(Keys.Right))
                if (curp.X != nx-1)
                    curp.X++;
                else if (tlc.X+nx < tm.NumX-1)
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
