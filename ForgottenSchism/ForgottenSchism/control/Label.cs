using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenSchism.control
{
    public class Label: Control
    {
        private String text;
        private static SpriteFont font;
        private Color color;

        public Label(Game1 game, String ftxt): base(game)
        {
            text = ftxt;
            color = Color.DarkRed;
            TabStop = false;
            font = null;
        }

        public override void loadContent()
        {
            if(font==null)
                font=game.Content.Load<SpriteFont>(@"font\\arial12norm");
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

            game.sb.DrawString(font, text, Position, color);
        }

        public override void HandleInput(GameTime gameTime)
        {
 	        //
        }
    }
}
