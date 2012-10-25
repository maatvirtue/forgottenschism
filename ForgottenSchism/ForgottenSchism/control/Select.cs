using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class Select: Control
    {
        Texture2D bla;
        Texture2D rla;
        Texture2D bra;
        Texture2D rra;
        Texture2D yra;
        Texture2D yla;
        SpriteFont font;
        int dt;
        bool r;
        List<String> els;
        int sel;

        public Select()
        {
            dt = 0;
            els = new List<String>();
            sel = 0;

            font = Content.Graphics.Instance.DefaultFont;

            loadContent();
        }

        public void add(String e)
        {
            els.Add(e);

            if (font.MeasureString(e).X+48 > Size.X)
                Size = new Vector2(font.MeasureString(e).X + 48, Size.Y);

            if (font.MeasureString(e).Y > Size.Y)
                Size = new Vector2(Size.X, font.MeasureString(e).Y);

            System.Console.Out.WriteLine(Size.X);
        }

        public String SelectedValue
        {
            get { if(els.Count!=0) return els[sel]; else return null; }
        }

        private void loadContent()
        {
            bra = Graphic.Instance.arrowRight(20, 20, Color.Blue);
            rra = Graphic.Instance.arrowRight(20, 20, Color.Red);
            bla = Graphic.Instance.arrowLeft(20, 20, Color.Blue);
            rla = Graphic.Instance.arrowLeft(20, 20, Color.Red);
            yla = Graphic.Instance.arrowLeft(20, 20, Color.Yellow);
            yra = Graphic.Instance.arrowRight(20, 20, Color.Yellow);

            Size=new Vector2(48, 20);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Right))
            {
                sel++;
                if (sel > els.Count - 1)
                    sel = 0;
            }
            else if(InputHandler.keyReleased(Keys.Left))
            {
                sel--;
                if (sel < 0)
                    sel = els.Count - 1;
            }

            if (InputHandler.keyDown(Keys.Right))
            {
                dt = 10;
                r = true;
            }
            else if (InputHandler.keyDown(Keys.Left))
            {
                dt = 10;
                r = false;
            }
            else
            {
                if(dt>0)
                    dt--;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (els.Count == 0)
                els.Add(" ");

            if(HasFocus)
                if(dt>0)
                    if(!r)
                        Graphic.Instance.SB.Draw(yla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
                    else
                        Graphic.Instance.SB.Draw(rla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
                else
                    Graphic.Instance.SB.Draw(rla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
            else
                Graphic.Instance.SB.Draw(bla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);

            Graphic.Instance.SB.DrawString(font, els[sel], new Vector2(Position.X + 23, Position.Y), Color.White);

            if (HasFocus)
                if (dt > 0)
                    if (r)
                        Graphic.Instance.SB.Draw(yra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
                    else
                        Graphic.Instance.SB.Draw(rra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
                else
                    Graphic.Instance.SB.Draw(rra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
            else
                Graphic.Instance.SB.Draw(bra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
        }
    }
}
