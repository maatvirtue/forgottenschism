using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class Menu: Control
    {
        int numy;
        List<Link> lnkls;
        int sel;
        Texture2D ta;
        Texture2D ba;
        bool are;
        SpriteFont sf;
        int fheigth;
         
        public Menu(int fnumy)
        {
            numy = fnumy;
            sel = 0;
            lnkls = new List<Link>();
            TabStop = true;
            are = true;

            sf = Content.Graphics.Instance.DefaultFont;
            fheigth = (int)(sf.MeasureString("M").Y);

            loadContent();
        }

        public List<Link> ListItems
        {
            get { return lnkls; }
        }

        public bool ArrowEnabled
        {
            get { return are; }
            set { are = value; }
        }

        public Link Focused
        {
            get { if (lnkls.Count != 0) return lnkls[sel]; else return null; }
        }

        public int Selected
        {
            get { return sel; }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (lnkls.Count == 0)
                return;

            int f = (sel / numy)*numy;

            if (f != 0)
                Graphic.Instance.SB.Draw(ta, new Rectangle((int)Position.X + 50, (int)Position.Y, 20, 20), Color.White);

            if ((sel/numy)<(lnkls.Count/numy))
                Graphic.Instance.SB.Draw(ba, new Rectangle((int)Position.X + 50, (int)(Position.Y + (numy * lnkls[0].Font.MeasureString("M").Y) + 25), 20, 20), Color.White);

            for (int i = 0; i < numy; i++)
                if((i+f)<lnkls.Count)
                    lnkls[i + f].Draw(gameTime);
        }

        public void add(Link l)
        {
            if (lnkls.Count == 0)
                l.HasFocus = true;

            if(l.Font == null)
                l.Font = sf;

            l.Position = new Vector2(Position.X, (int)(Position.Y + ((lnkls.Count % numy) * fheigth) + 25));

            lnkls.Add(l);
        }

        public void clear()
        {
            if (sel>0 && lnkls[sel] != null)
                lnkls[sel].HasFocus = false;

            sel = 0;

            if (lnkls.Count > 0)
                lnkls.Clear();
        }

        public void unfocusLink()
        {
            foreach (Link l in lnkls)
            {
                l.HasFocus = false;
            }
            sel = 0;
        }

        public void refocusLink()
        {
            lnkls[sel].HasFocus = true;
        }

        private void loadContent()
        {
            ta = Graphic.Instance.arrowUp(20, 20, Color.Blue);
            ba = Graphic.Instance.arrowDown(20, 20, Color.Blue);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (!are)
                return;

            if (lnkls.Count < 2)
                return;

            if(InputHandler.keyReleased(Keys.Down))
            {
                lnkls[sel].HasFocus = false;

                sel++;

                if(sel>lnkls.Count-1)
                    sel=0;

                lnkls[sel].HasFocus = true;
            }
            else if(InputHandler.keyReleased(Keys.Up))
            {
                lnkls[sel].HasFocus = false;

                sel--;

                if(sel<0)
                    sel=lnkls.Count-1;

                lnkls[sel].HasFocus = true;
            }
            
            if(InputHandler.keyReleased(Keys.Enter))
                lnkls[sel].HandleInput(gameTime);
        }
    }
}
