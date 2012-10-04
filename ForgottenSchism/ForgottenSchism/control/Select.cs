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

        public Select(Game1 game): base(game)
        {
            dt = 0;
            els = new List<String>();
            sel = 0;
        }

        public void add(String e)
        {
            els.Add(e);
        }

        public String SelectedValue
        {
            get { if(els.Count!=0) return els[sel]; else return null; }
        }

        public override void loadContent()
        {
            font = Game.Content.Load<SpriteFont>(@"font\\arial12norm");

            bra = Graphic.arrowRight(game, 20, 20, Color.Blue);
            rra = Graphic.arrowRight(game, 20, 20, Color.Red);
            bla = Graphic.arrowLeft(game, 20, 20, Color.Blue);
            rla = Graphic.arrowLeft(game, 20, 20, Color.Red);
            yla = Graphic.arrowLeft(game, 20, 20, Color.Yellow);
            yra = Graphic.arrowRight(game, 20, 20, Color.Yellow);

            Size=new Vector2(0, 20);

            if (els.Count == 0)
                els.Add(" ");

            foreach (String e in els)
            {
                if (font.MeasureString(e).X > Size.X)
                    Size = new Vector2(font.MeasureString(e).X, Size.Y);

                if (font.MeasureString(e).Y > Size.Y)
                    Size = new Vector2(Size.X, font.MeasureString(e).Y);
            }

            Size = new Vector2(Size.X+48, Size.Y);
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

            if(HasFocus)
                if(dt>0)
                    if(!r)
                        game.sb.Draw(yla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
                    else
                        game.sb.Draw(rla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
                else
                    game.sb.Draw(rla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
            else
                game.sb.Draw(bla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);

            game.sb.DrawString(font, els[sel], new Vector2(Position.X + 23, Position.Y), Color.White);

            if (HasFocus)
                if (dt > 0)
                    if (r)
                        game.sb.Draw(yra, new Rectangle((int)(Position.X + (Size.X-20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
                    else
                        game.sb.Draw(rra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
                else
                    game.sb.Draw(rra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
            else
                game.sb.Draw(bra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
        }
    }
}
