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
    class DialogTxt: Control
    {
        Color border;
        Color bg;
        Color fg;
        String q;
        Texture2D tbord;
        Texture2D tbg;
        SpriteFont font;
        TextBox txt;
        bool sel;

        public EventHandler complete;

        public DialogTxt(String fq)
        {
            border = Color.Red;
            bg = Color.Blue;
            fg = Color.White;
            q = fq;

            txt = new TextBox(15);
            txt.HasFocus = true;

            sel = false;

            TabStop = false;

            Size = new Vector2(300, 150);

            font = Content.Graphics.Instance.DefaultFont;
            loadContent();
        }

        public Color TextColor
        {
            get { return fg; }
            set { fg = value; }
        }

        public char[] CText
        {
            get { return txt.CText; }
            set { txt.CText = value; }
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

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;

                txt.Position = new Vector2(Position.X + 12, Position.Y + 42);
            }
        }

        private void loadContent()
        {
            tbord = Graphic.Instance.rect((int)Size.X-1, (int)Size.Y-1, border);
            tbg = Graphic.Instance.rect((int)Size.X - 2, (int)Size.Y - 2, bg);
        }

        public override void Draw(GameTime gameTime)
        { 
            base.Draw(gameTime);

            Color selc;

            if (sel)
                selc = Color.Red;
            else
                selc = fg;

            Graphic.Instance.SB.Draw(tbord, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X-1, (int)Size.Y-1), Color.White);
            Graphic.Instance.SB.Draw(tbg, new Rectangle((int)Position.X + 1, (int)Position.Y + 1, (int)Size.X - 2, (int)Size.Y - 2), Color.White);
            Graphic.Instance.SB.DrawString(font, q, new Vector2(Position.X + 12, Position.Y + 12), fg);
            txt.Draw(gameTime);
            Graphic.Instance.SB.DrawString(font, "Enter", new Vector2(Position.X + 72, (Position.Y + Size.Y) - 31), selc);
        }

        public override void handleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Up) || InputHandler.keyReleased(Keys.Down))
            {
                sel = !sel;

                if (sel)
                    txt.HasFocus = false;
                else
                    txt.HasFocus = true;
            }

            if (txt.HasFocus)
                txt.handleInput(gameTime);

            if (InputHandler.keyReleased(Keys.Enter) && sel && complete != null)
                complete(this, new EventArgObject(txt.Text));
        }
    }
}
