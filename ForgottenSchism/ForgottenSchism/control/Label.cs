using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    public class Label: Control
    {
        private String text;
        private SpriteFont font;
        private Color color;

        public Label(String ftxt)
        {
            text = ftxt;
            color = Color.DarkRed;
            TabStop = false;
            font = null;

            font = Content.Graphics.Instance.DefaultFont;
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
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
            base.Draw(gameTime);

            Graphic.Instance.SB.DrawString(font, text, Position, color);
        }

        public override void HandleInput(GameTime gameTime)
        {
 	        //
        }
    }
}
