using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForgottenSchism.control
{
    class Display: DrawableGameComponent
    {
        /// <summary>
        /// list of windows
        /// </summary>
        List<Window> wls;

        public Display(): base(Game1.Instance)
        {
            wls = new List<Window>();
        }

        public Display(Window main): base(Game1.Instance)
        {
            wls = new List<Window>();
            wls.Add(main);
        }

        /// <summary>
        /// add a Window to the display
        /// </summary>
        /// <param name="w">Window to add</param>
        public void add(Window w)
        {
            if (wls.Count > 0)
                wls.Last().HasFocus = false;

            wls.Add(w);

            w.HasFocus = true;
        }

        public void rem(Window w)
        {
            wls.Remove(w);

            if (wls.Count > 0)
                wls.Last().HasFocus = true;
        }

        public void handleInput(GameTime gameTime)
        {
            if (wls.Count > 0)
                wls.Last().handleInput(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            foreach (Window w in wls)
                w.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (Window w in wls)
                w.Update(gameTime);
        }
    }
}
