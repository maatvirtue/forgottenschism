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
    class DialogYN: Control
    {
        Color border;
        Color bg;
        Color fg;
        String txt;
        Texture2D tbord;
        Texture2D tbg;
        SpriteFont font;
        bool y;
        bool show;

        public EventHandler chose;

        public DialogYN(Game1 game, String ftxt): base(game)
        {
            border = Color.Red;
            bg = Color.Blue;
            fg = Color.White;
            txt = ftxt;

            TabStop = false;

            show = false;
            y = true;

            Size = new Vector2(300, 150);
        }

        public bool Show
        {
            get { return show; }
            set { show = value; }
        }

        public Color TextColor
        {
            get { return fg; }
            set { fg = value; }
        }

        public String Text
        {
            get { return txt; }
            set { txt = value; }
        }

        public Color Background
        {
            get { return bg; }
            set { bg = value; }
        }

        public Color Border
        {
            get { return border; }
            set { border = value; }
        }

        public override void loadContent()
        {
            font = game.Content.Load<SpriteFont>(@"font\\arial12norm");
            tbord = Graphic.rect(game, (int)Size.X-1, (int)Size.Y-1, border);
            tbg = Graphic.rect(game, (int)Size.X - 2, (int)Size.Y - 2, bg);
        }

        public override void Draw(GameTime gameTime)
        { 
            base.Draw(gameTime);

            if (!show)
                return;

            Color yc;
            Color nc;

            if (y)
            {
                yc = Color.Red;
                nc = fg;
            }
            else
            {
                yc = fg;
                nc = Color.Red;
            }

            game.sb.Draw(tbord, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X-1, (int)Size.Y-1), Color.White);
            game.sb.Draw(tbg, new Rectangle((int)Position.X+1, (int)Position.Y+1, (int)Size.X - 2, (int)Size.Y - 2), Color.White);
            game.sb.DrawString(font, txt, new Vector2(Position.X + 12, Position.Y + 12), fg);
            game.sb.DrawString(font, "Yes", new Vector2(Position.X+ + 22, (Position.Y+Size.Y) - 31), yc);
            game.sb.DrawString(font, "No", new Vector2(Position.X + +242, (Position.Y + Size.Y) - 31), nc);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (!show)
                return;

            if (InputHandler.keyReleased(Keys.Left) || InputHandler.keyReleased(Keys.Right) || InputHandler.keyReleased(Keys.Up) || InputHandler.keyReleased(Keys.Down))
                y = !y;

            if (InputHandler.keyReleased(Keys.Enter) && chose != null)
                chose(this, new EventArgObject(y));
        }
    }
}
