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
        TimeSpan timeInterval = new TimeSpan(0);
        TimeSpan startDisplay = new TimeSpan(0);

        String text;
        SpriteFont font;
        Color color;
        ColorTheme.LabelColorTheme.LabelFunction fun;

        bool requestTime;

        public EventHandler doneShowing;

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

            requestTime = false;
        }

        public float Width
        {
            get { return Font.MeasureString(Text).X; }
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

        public void visibleTemp(int duration)
        {
            timeInterval = TimeSpan.FromMilliseconds(duration);
            requestTime = true;
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

            if (requestTime)
            {
                requestTime = false;
                startDisplay = gameTime.TotalGameTime;
            }

            if (startDisplay != new TimeSpan(0) && timeInterval != new TimeSpan(0))
            {
                if (gameTime.TotalGameTime < startDisplay + timeInterval)
                    Visible = true;
                else
                {
                    Visible = false;
                    if (doneShowing != null)
                        doneShowing(this, null);

                    timeInterval = new TimeSpan(0);
                }
            }
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
