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
        String text;
        SpriteFont font;
        Color color;
        ColorTheme.LabelColorTheme.LabelFunction fun;

        public Label(String ftxt)
        {
            init(ftxt, ColorTheme.LabelColorTheme.LabelFunction.NORM);
        }

        /// <summary>
        /// initialize label
        /// </summary>
        /// <param name="ftxt">text of the label</param>
        /// <param name="lblfun">Label's Function</param>
        public Label(String ftxt, ColorTheme.LabelColorTheme.LabelFunction lblfun)
        {
            init(ftxt, lblfun);
        }

        private void init(String ftxt, ColorTheme.LabelColorTheme.LabelFunction lblfun)
        {
            fun = lblfun;
            text = ftxt;
            color = ColorTheme.Default.LabelCT.get(fun);
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
            set
            {
                color = value;
                fun = ColorTheme.LabelColorTheme.LabelFunction.CUSTOM;
            }
        }

        public ColorTheme.LabelColorTheme.LabelFunction LabelFun
        {
            set
            {
                fun = value;

                if (fun != ColorTheme.LabelColorTheme.LabelFunction.CUSTOM)
                    color = ColorTheme.Default.LabelCT.get(fun);
            }
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

        public override void handleInput(GameTime gameTime)
        {
 	        //
        }
    }
}
