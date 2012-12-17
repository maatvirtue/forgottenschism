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
        Texture2D tbg;
        String selTxt;
         
        public Menu(int fnumy)
        {
            numy = fnumy;
            sel = 0;
            selTxt = "";
            lnkls = new List<Link>();
            TabStop = true;
            are = true;

            sf = Content.Graphics.Instance.DefaultFont;
            fheigth = (int)(sf.MeasureString("M").Y);

            loadContent();
        }

        public bool ArrowEnabled
        {
            get { return are; }
            set { are = value; }
        }

        public int Count
        {
            get { return lnkls.Count; }
        }

        public Link Focused
        {
            get { if (lnkls.Count != 0) return lnkls[sel]; else return null; }
        }

        public int Selected
        {
            get { return sel; }
        }

        public String SelectedText
        {
            get { return selTxt; }
        }

        public Link getLink(String text)
        {
            foreach (Link l in lnkls)
                if (l.Text == text)
                    return l;

            return null;
        }

        public Link getLink(int index)
        {
            if (index < 0 || index > lnkls.Count - 1)
                return null;

            return lnkls[index];
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (lnkls.Count == 0)
                return;

            Graphic.Instance.SB.Draw(tbg, new Rectangle((int)Position.X - 1, (int)Position.Y + fheigth, 200, fheigth * numy), Color.White);

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
            {
                l.HasFocus = true;
                selTxt = l.Text;
            }

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
            selTxt = lnkls[sel].Text;
        }

        private void loadContent()
        {
            tbg = Graphic.Instance.rect(200, fheigth * numy, Color.Black);
            ta = Graphic.Instance.arrowUp(20, 20, Color.Blue);
            ba = Graphic.Instance.arrowDown(20, 20, Color.Blue);
        }

        public override void handleInput(GameTime gameTime)
        {
            if (!are)
                return;


            if (lnkls.Count == 0)
                return;

            if (InputHandler.keyReleased(Keys.Enter))
                lnkls[sel].handleInput(gameTime);

            if (lnkls.Count == 1)
                return;

            if(InputHandler.keyReleased(Keys.Down))
            {
                lnkls[sel].HasFocus = false;

                sel++;

                if(sel>lnkls.Count-1)
                    sel=0;

                selTxt = lnkls[sel].Text;
                lnkls[sel].HasFocus = true;
            }
            else if(InputHandler.keyReleased(Keys.Up))
            {
                lnkls[sel].HasFocus = false;

                sel--;

                if(sel<0)
                    sel=lnkls.Count-1;

                selTxt = lnkls[sel].Text;
                lnkls[sel].HasFocus = true;
            }
        }
    }
}
