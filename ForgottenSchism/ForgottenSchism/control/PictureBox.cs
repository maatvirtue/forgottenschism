using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ForgottenSchism.control
{
    class PictureBox: Control
    {
        private String imgurl;
        private Texture2D img;

        public PictureBox(Game1 fgame, String fimgurl): base(fgame)
        {
            imgurl = fimgurl;
            TabStop = false;
        }

        public Texture2D Image
        {
            get { return img; }
        }
            
        public override void loadContent()
        {
            base.LoadContent();

            img = game.Content.Load<Texture2D>(@imgurl);
            
            if(Size.X==0||Size.Y==0)
                Size = new Vector2(img.Width, img.Height);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            game.sb.Draw(img, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White);
        }

        public override void HandleInput(GameTime gameTime)
        {
            //
        }
    }
}
