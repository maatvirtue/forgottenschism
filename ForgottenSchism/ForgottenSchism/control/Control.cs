using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenSchism.control
{
    public abstract class Control: DrawableGameComponent
    {
        /// <summary>
        /// has the focus on the parent window. same as selected
        /// </summary>
        bool hasFocus;

        /// <summary>
        /// is the control enabled.
        /// IMPORTANT NOTE: this is different than the GameComponent Enable Property:
        /// The GameComponent Enable Property defines if the Control is going to be updated ( Update() ).
        /// This property defines the look of the Control and block the Control from receiving input ( handleInput() )
        /// but let the Control Update() itself.
        /// </summary>
        bool gEnable;

        /// <summary>
        /// is stopping (consuming) the tab key
        /// </summary>
        bool tabStop;

        /// <summary>
        /// absolute position on the display
        /// </summary>
        Vector2 position;

        /// <summary>
        /// size (width, height)
        /// </summary>
        Vector2 size;

        public Control(): base(Game1.Instance)
        {
            Visible = true;
            Enabled = true;
            gEnable = true;
            tabStop = true;
            hasFocus = false;
            position = new Vector2();
            size = new Vector2();
        }

        /// <summary>
        /// is the control enabled.
        /// IMPORTANT NOTE: this is different than the GameComponent Enable Property:
        /// The GameComponent Enable Property defines if the Control is going to be updated ( Update() ).
        /// This property defines the look of the Control and block the Control from receiving input ( handleInput() )
        /// but let the Control Update() itself.
        /// </summary>
        public virtual bool GEnable
        {
            get { return gEnable; }
            set { gEnable = value; }
        }

        public virtual bool TabStop
        {
            get { return tabStop; }
            set { tabStop = value; }
        }
        
        public virtual bool HasFocus
        {
            get { return hasFocus; }
            set { hasFocus = value; }
        }

        public virtual Vector2 Size
        {
            get { return new Vector2(position.X, position.Y); }
            set { position.X = value.X; position.Y = value.Y; }
        }

        public virtual Vector2 Position
        {
            get { return new Vector2(size.X, size.Y); }
            set { size.X = value.X; size.Y = value.Y; }
        }

        /// <summary>
        /// gets the Color according to default ColorTheme and hasFocus/noFocus gEnabled/gDisenabed
        /// </summary>
        /// <returns>Color according to state and default ColorTheme</returns>
        protected Color getThemeColor()
        {
            return ColorTheme.Default.getColor(gEnable, hasFocus);
        }

        public abstract void handleInput(GameTime gameTime);
    }
}
