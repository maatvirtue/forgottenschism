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
         
        public Menu(Game1 game, int fnumy): base(game)
        {
            numy = fnumy;
            sel = 0;
            lnkls = new List<Link>();
            TabStop = true;
            are = true;
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

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (lnkls.Count == 0)
                return;

            int f = (sel / numy)*numy;

            if (f != 0)
                game.sb.Draw(ta, new Rectangle((int)Position.X + 50, (int)Position.Y, 20, 20), Color.White);

            if ((sel/numy)<(lnkls.Count/numy))
                game.sb.Draw(ba, new Rectangle((int)Position.X + 50, (int)(Position.Y + (numy * lnkls[0].Font.MeasureString("M").Y)+25), 20, 20), Color.White);

            for (int i = 0; i < numy; i++)
                if((i+f)<lnkls.Count)
                    lnkls[i + f].Draw(gameTime);
        }

        public void add(Link l)
        {
            if (lnkls.Count == 0)
                l.HasFocus = true;
            
            lnkls.Add(l);
        }

        public override void loadContent()
        {
            ta = Graphic.arrowUp(game, 20, 20, Color.Blue);
            ba = Graphic.arrowDown(game, 20, 20, Color.Blue);

            Link l;

            for(int i=0; i<lnkls.Count; i++)
            {
                l = lnkls[i];
                l.loadContent();
                l.Position = new Vector2(Position.X, (int)(Position.Y + ((i % numy) * l.Font.MeasureString("M").Y)) + 25);
            }
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
