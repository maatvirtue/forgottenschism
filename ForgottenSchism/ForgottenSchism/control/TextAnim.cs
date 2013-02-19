using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class TextAnim: Control
    {
        /// <summary>
        /// delay to wait between printing each character
        /// </summary>
        static TimeSpan delay = TimeSpan.FromMilliseconds(100);

        /// <summary>
        /// when the delay finishes
        /// </summary>
        TimeSpan delayFinish;

        /// <summary>
        /// Text that needs to be displayed
        /// </summary>
        String txt;

        /// <summary>
        /// visible text
        /// </summary>
        String vtxt;

        /// <summary>
        /// Position of next char to show
        /// </summary>
        int cur;

        public TextAnim(String ftxt)
        {
            txt = ftxt;
            cur = -1;
            vtxt = "";
            delayFinish = new TimeSpan(0);
            TabStop = false;
        }

        /// <summary>
        /// Text to show
        /// </summary>
        public String Text
        {
            get { return txt; }
            set { txt = value; }
        }

        /// <summary>
        /// erase the visible text and stop animation
        /// </summary>
        public void erase()
        {
            cur = -1;
            vtxt = "";
        }

        /// <summary>
        /// starts animation
        /// </summary>
        public void animate()
        {
            cur = 0;
            vtxt = "";
        }

        /// <summary>
        /// adds 1 char to the visible text
        /// </summary>
        private void putch()
        {
            if(cur>txt.Length-1)
                cur=-1;

            if (cur < 0)
                return;

            vtxt += txt[cur];
            cur++;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Graphic.Instance.SB.DrawString(Content.Graphics.Instance.DefaultFont, vtxt, Position, ColorTheme.Default.getColor(true, false));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (delayFinish > gameTime.TotalGameTime)
                return;

            if (cur >= 0)
            {
                putch();
                delayFinish = gameTime.TotalGameTime + delay;
            }
        }

        public override void handleInput(GameTime gameTime)
        {
            //
        }
    }
}
