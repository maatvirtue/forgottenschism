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
    class Link: Control
    {
        private String text;
        private static SpriteFont font;
        private Color normColor;
        private Color selColor;

        public EventHandler selected;

        public Link(String ftxt)
        {
            text = ftxt;
            normColor = Color.White;
            selColor = Color.DarkRed;
            TabStop = true;

            font = Content.Graphics.Instance.DefaultFont;
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public Color NormColor
        {
            get { return normColor; }
            set { normColor = value; }
        }

        public Color SelColor
        {
            get { return selColor; }
            set { selColor = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(!HasFocus)
                Graphic.Instance.SB.DrawString(font, text, Position, normColor);
            else
                Graphic.Instance.SB.DrawString(font, text, Position, selColor);

            base.Draw(gameTime);
        }

        public override void HandleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Enter))
            {
                if (selected != null)
                    selected(this, null);
            }
        }
    }
}
