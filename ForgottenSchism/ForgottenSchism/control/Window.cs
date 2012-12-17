using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class Window: DrawableGameComponent
    {
        /// <summary>
        /// position of the top left corner of the window
        /// </summary>
        Point pos;

        /// <summary>
        /// size of the window
        /// </summary>
        Point size;

        /// <summary>
        /// FocusManager (contains Controls)
        /// </summary>
        FocusManager fm;

        /// <summary>
        /// border of the window
        /// </summary>
        Texture2D tbor;

        bool hasFocus;

        /// <summary>
        /// is main window (in this case no border)
        /// </summary>
        bool ismain;

        /// <summary>
        /// backgroud image of the window
        /// </summary>
        Content.Graphics.CachedImage bg;

        public Window(bool mainWindow): base(Game1.Instance)
        {
            init(mainWindow);
        }

        public Window(): base(Game1.Instance)
        {
            init(false);
        }

        private void init(bool fismain)
        {
            ismain = fismain;
            pos = new Point();
            size = new Point(100, 100);
            fm = new FocusManager();
            hasFocus = false;
        }

        /// <summary>
        /// position of the window (only used for background and border display)
        /// </summary>
        public Point Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public Content.Graphics.CachedImage BackgroundImage
        {
            get { return bg; }
            set { bg = value; }
        }

        public bool HasFocus
        {
            get { return hasFocus; }
            set
            {
                hasFocus = value;

                if(!ismain)
                    genBorder();
            }
        }

        /// <summary>
        /// size of the window (only used for background and border display)
        /// </summary>
        public Point Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// (Re)generate border texture
        /// </summary>
        private void genBorder()
        {
            tbor=Graphic.Instance.rect(size.X, size.Y, ColorTheme.Default.getColor(true, hasFocus));
        }

        /// <summary>
        /// adds a Control to the Window
        /// </summary>
        /// <param name="c">Control to add</param>
        public void add(Control c)
        {
            fm.add(c);
        }

        /// <summary>
        /// removes a Control from the Window
        /// </summary>
        /// <param name="c">Control to remove</param>
        public void rem(Control c)
        {
            fm.rem(c);
        }

        public void handleInput(GameTime gameTime)
        {
            fm.handleInput(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            fm.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if(!ismain)
                Graphic.Instance.SB.Draw(tbor, new Rectangle(pos.X, pos.Y, size.X, size.Y), Color.White);
            
            Graphic.Instance.SB.Draw(bg.Image, new Rectangle(pos.X+2, pos.Y+2, size.X-4, size.Y-4), Color.White);

            fm.Draw(gameTime);
        }
    }
}
