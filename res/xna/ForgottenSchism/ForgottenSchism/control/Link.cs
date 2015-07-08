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
        SpriteFont font;
        String text;
        
        public EventHandler selected;

        public Link(String ftxt)
        {
            text = ftxt;
            TabStop = true;

            font = Content.Graphics.Instance.DefaultFont;
        }

        public String Text
        {
            get { return text; }
            set { text = value; }
        }

        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        public void center()
        {
            Position = new Vector2(Game1.Instance.Window.ClientBounds.Width / 2 - Font.MeasureString(Text).X / 2, Position.Y);
        }

        public void center(int yPos)
        {
            Position = new Vector2(Game1.Instance.Window.ClientBounds.Width / 2 - Font.MeasureString(Text).X / 2, yPos);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Graphic.Instance.SB.DrawString(font, text, Position, getThemeColor());
        }

        public override void handleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Enter))
            {
                if (selected != null)
                    selected(this, null);
            }
        }
    }
}
