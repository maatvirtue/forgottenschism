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

        public EventHandler chose;

        public DialogYN(String ftxt)
        {
            border = Color.Red;
            bg = Color.Blue;
            fg = Color.White;
            txt = ftxt;

            TabStop = false;

            y = true;

            Size = new Vector2(300, 150);

            font = Content.Graphics.Instance.DefaultFont;

            loadContent();
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

        private void loadContent()
        {
            tbord = Graphic.Instance.rect((int)Size.X-1, (int)Size.Y-1, border);
            tbg = Graphic.Instance.rect((int)Size.X - 2, (int)Size.Y - 2, bg);
        }

        public override void Draw(GameTime gameTime)
        { 
            base.Draw(gameTime);

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

            Graphic.Instance.SB.Draw(tbord, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X - 1, (int)Size.Y - 1), Color.White);
            Graphic.Instance.SB.Draw(tbg, new Rectangle((int)Position.X + 1, (int)Position.Y + 1, (int)Size.X - 2, (int)Size.Y - 2), Color.White);
            Graphic.Instance.SB.DrawString(font, txt, new Vector2(Position.X + 12, Position.Y + 12), fg);
            Graphic.Instance.SB.DrawString(font, "Yes", new Vector2(Position.X + +22, (Position.Y + Size.Y) - 31), yc);
            Graphic.Instance.SB.DrawString(font, "No", new Vector2(Position.X + +242, (Position.Y + Size.Y) - 31), nc);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Left) || InputHandler.keyReleased(Keys.Right) || InputHandler.keyReleased(Keys.Up) || InputHandler.keyReleased(Keys.Down))
                y = !y;

            if (InputHandler.keyReleased(Keys.Enter) && chose != null)
                chose(this, new EventArgObject(y));
        }
    }
}
