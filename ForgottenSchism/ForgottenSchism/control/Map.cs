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
        Dictionary<Tile.TileType, Content.Graphics.CachedImage> tbuf;
        Tilemap tm;
        static Texture2D tcur;
        static Texture2D tsel;
        static int TW=64;
        static int TH=64;
        int nx;
        int ny;
        Point tlc;
        Point curp;
        Point sel;
        Point lsel;
        Fog fog;
        Dictionary<Point, Content.Graphics.CachedImage> cls;
        bool are;
        bool sele;

        public EventHandler changeCurp;
        public EventHandler curSelection;

        public Map(Tilemap ftm)
        {
            nx = Graphic.Instance.GDM.PreferredBackBufferWidth / 64;
            ny = 6;

            init(ftm);
        }

        public Map(Tilemap ftm, int fnx, int fny)
        {
            nx = fnx;
            ny = fny;

            init(ftm);
        }

        private void init(Tilemap ftm)
        {
            if (ftm.NumX < nx || ftm.NumY < ny)
                throw new Exception("Error Tilemap is smaller than viewport");

            are = true;
            sele = true;
            cls = new Dictionary<Point, Content.Graphics.CachedImage>();
            tm = ftm;
            TabStop = true;
            tlc = new Point(0, 0);
            curp = new Point(nx / 2, ny / 2);
            sel = new Point(-1, -1);

            tcur = Content.Graphics.Instance.Images.gui.cursor.Image;
            tsel = Content.Graphics.Instance.Images.gui.selCursor.Image;

            tbuf = Content.Graphics.Instance.Images.tiles;

            if (tm != null)
                Size = new Vector2(tm.NumX * TW, tm.NumY * TH);
        }

        public bool SelectionEnabled
        {
            get { return sele; }
            set { sele = value; }
        }

        public Dictionary<Point, Content.Graphics.CachedImage> CharLs
        {
            get { return cls; }
        }

        public bool ArrowEnabled
        {
            get { return are; }
            set { are = value; }
        }

        public void focus(int x, int y)
        {
            if (x >= tm.NumX || y >= tm.NumY || x<0 || y<0)
                return;

            Point tp = new Point(-1, -1);

            int i=3;
            int e=3;

            while (x - i < 0 || y - e < 0)
            {
                if (x - i < 0)
                    i--;

                if (y - e < 0)
                    e--;
            }

            tp.X = x;
            tp.Y = y;
            x = x - i;
            y = y - e;

            if (x < tm.NumX - nx)
            {
                tlc.X = x;
                curp.X = 0;
            }
            else
            {
                tlc.X = tm.NumX - nx;
                curp.X = x - tlc.X;
            }

            if (y < tm.NumY - ny)
            {
                tlc.Y = y;
                curp.Y = 0;
            }
            else
            {
                tlc.Y = tm.NumY - ny;
                curp.Y = y - tlc.Y;
            }

            if (tp.X >= 0 && tp.Y >= 0)
            {
                curp = tp;
            }

            if (tlc.X < 0 || tlc.Y < 0 || curp.X < 0 || curp.Y < 0)
            {
                throw new Exception("Error in above algorithm");
            }
        }

        public Fog Fog
        {
            get { return fog; }
            set { fog = value; }
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

        public Point LastSelection
        {
            get { return lsel; }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            for (int e = 0; e < ny; e++)
                for (int i = 0; i < nx; i++)
                {
                    if (fog != null && fog.get((i + tlc.X), (e + tlc.Y)))
                        Graphic.Instance.SB.Draw(Content.Graphics.Instance.Images.fog.Image, new Rectangle((int)Position.X + (i * TW), (int)Position.Y + (e * TH), TW, TH), Color.White);
                    else
                    {
                        Graphic.Instance.SB.Draw(tbuf[tm.get((i + tlc.X), (e + tlc.Y)).Type].Image, new Rectangle((int)Position.X + (i * TW), (int)Position.Y + (e * TH), TW, TH), Color.White);

                        if (cls.ContainsKey(new Point((i + tlc.X), (e + tlc.Y))))
                            Graphic.Instance.SB.Draw(cls[new Point((i + tlc.X), (e + tlc.Y))].Image, new Rectangle((int)Position.X + (i * TW), (int)Position.Y + (e * TH), TW, TH), Color.White);
                    }
                }

            if(are)
                Graphic.Instance.SB.Draw(tcur, new Rectangle((int)(Position.X + ((curp.X - tlc.X) * TW)), (int)(Position.Y + ((curp.Y - tlc.Y) * TH)), TW, TH), Color.White);

            if(are&&sele&&sel.X>=tlc.X&&sel.Y>=tlc.Y&&sel.X<=(tlc.X+nx-1)&&sel.Y<=(tlc.Y+ny-1))
                Graphic.Instance.SB.Draw(tsel, new Rectangle((int)(Position.X + ((sel.X - tlc.X) * TW)), (int)(Position.Y + ((sel.Y - tlc.Y) * TH)), TW, TH), Color.White);
        }   

        public override void HandleInput(GameTime gameTime)
        {
            if (are)
            {
                if (InputHandler.keyReleased(Keys.Up))
                    if (curp.Y != tlc.Y)
                        curp.Y--;
                    else if (tlc.Y != 0)
                    {
                        tlc.Y--;
                        curp.Y--;
                    }

                if (InputHandler.keyReleased(Keys.Down))
                    if (curp.Y != tlc.Y+(ny - 1))
                        curp.Y++;
                    else if (tlc.Y + (ny - 1) != tm.NumY - 1)
                    {
                        tlc.Y++;
                        curp.Y++;
                    }

                if (InputHandler.keyReleased(Keys.Left))
                    if (curp.X != tlc.X)
                        curp.X--;
                    else if (tlc.X != 0)
                    {
                        tlc.X--;
                        curp.X--;
                    }

                if (InputHandler.keyReleased(Keys.Right))
                    if (curp.X != tlc.X+(nx - 1))
                        curp.X++;
                    else if (tlc.X + (nx - 1) != tm.NumX - 1)
                    {
                        tlc.X++;
                        curp.X++;
                    }

                if (InputHandler.arrowReleased() && changeCurp != null)
                    changeCurp(this, new EventArgObject(new Point(curp.X, curp.Y)));
            }

            if (InputHandler.keyReleased(Keys.Enter))
            {
                if (sele)
                {
                    if (sel.X < 0 && sel.Y < 0)
                    {
                        sel = new Point(curp.X + tlc.X, curp.Y + tlc.Y);
                        lsel = new Point(curp.X + tlc.X, curp.Y + tlc.Y);
                    }
                    else
                        sel = new Point(-1, -1);
                }
                else
                    sel = new Point(curp.X, curp.Y);

                if (curSelection != null)
                {
                    curSelection(this, new EventArgObject(sel));
                }
            }
        }
    }
}
