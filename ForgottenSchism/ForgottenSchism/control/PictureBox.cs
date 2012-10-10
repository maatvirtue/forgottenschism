using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class PictureBox: Control
    {
        private Graphic.Content.CachedImage img;

        public PictureBox()
        {
            TabStop = false;
        }

        public Graphic.Content.CachedImage Image
        {
            get { return img; }
            set
            {
                img = value;

                if(Size.X==0||Size.Y==0)
                    Size=new Vector2(img.Image.Bounds.Width, img.Image.Bounds.Height);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Graphic.Instance.SB.Draw(img.Image, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
        }

        public override void HandleInput(GameTime gameTime)
        {
            //
        }
    }
}
