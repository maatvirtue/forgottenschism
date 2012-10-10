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
        private bool hasFocus;
        private bool tabStop;
        private Vector2 position;
        private Vector2 size;

        public Control(): base(Game1.Instance)
        {
            Visible = true;
            Enabled = true;
            tabStop = true;
            hasFocus = false;
            position = new Vector2();
            size = new Vector2();
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

        public abstract void HandleInput(GameTime gameTime);
    }
}
