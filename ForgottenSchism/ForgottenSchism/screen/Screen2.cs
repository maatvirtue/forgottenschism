using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.engine;
using ForgottenSchism.control;

namespace ForgottenSchism.screen
{
    class Screen2: DrawableGameComponent
    {
        /// <summary>
        /// list of windows
        /// </summary>
        List<Window> wls;

        public Screen2(): base(Game1.Instance)
        {
            wls = new List<Window>();

            add(new Window(true));
        }

        protected Window MainWindow
        {
            get { return wls[0]; }
        }

        /// <summary>
        /// adds a Window to the Screen
        /// </summary>
        /// <param name="w">Window to add</param>
        public void add(Window w)
        {
            if (wls.Count > 0)
                wls.Last().HasFocus = false;

            wls.Add(w);

            w.HasFocus = true;
        }

        /// <summary>
        /// removes a window from the Screen
        /// </summary>
        /// <param name="w"></param>
        public void rem(Window w)
        {
            wls.Remove(w);

            if (wls.Count > 0)
                wls.Last().HasFocus = true;
        }

        /// <summary>
        /// Show a Screen for the first time
        /// </summary>
        public virtual void start()
        {
            Enabled = true;
            Visible = true;
        }

        /// <summary>
        /// resume diplay of a screen
        /// </summary>
        public virtual void resume()
        {
            Visible = true;
            Enabled = true;
        }

        /// <summary>
        /// pause display of a screen
        /// </summary>
        public virtual void pause()
        {
            Visible = false;
            Enabled = false;
        }

        /// <summary>
        /// stop display of a screen
        /// </summary>
        public virtual void stop()
        {
            Enabled = false;
            Visible = false;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible)
                return;

            Graphic.Instance.SB.Begin();

            base.Draw(gameTime);

            foreach (Window w in wls)
                w.Draw(gameTime);

            Graphic.Instance.SB.End();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!Enabled)
                return;

            foreach (Window w in wls)
                w.Update(gameTime);

            if (wls.Count > 0)
                wls.Last().handleInput(gameTime);
        }
    }
}
