using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace ForgottenSchism.control
{
    public class Control
    {
        private bool visible;
        private bool hasFocus;
        private bool enable;
        private Rectangle rect;

        public event EventHandler selected;

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public bool HasFocus
        {
            get { return hasFocus; }
        }

        public bool Enable
        {
            retu
    }
}
