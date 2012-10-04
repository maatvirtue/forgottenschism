using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.control;

namespace ForgottenSchism.screen
{
    public abstract class Screen : DrawableGameComponent
    {
        protected ControlManager cm;
        private Game1 game;
        private Texture2D bgimg;
        private String imgurl;

        public Screen(Game1 fgame): base(fgame)
        {
            cm = new ControlManager(fgame);
            game = fgame;

            imgurl = "";

            Enabled = false;
            Visible = false;
        }

        protected String BgImgUrl
        {
            set { imgurl = value; }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            if (imgurl != "")
                bgimg=game.Content.Load<Texture2D>(@imgurl);

            cm.loadContent();
        }

        new protected Game1 Game
        {
            get { return game; }
        }

        public override void Initialize()
        {
            base.Initialize();

            cm.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Enabled)
                cm.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (!Visible)
                return;

            Game.sb.Begin();

            base.Draw(gameTime);

            if(bgimg != null)
                game.sb.Draw(bgimg, new Rectangle(0, 0, game.WindowWidth, game.WindowHeight), Color.White);

            cm.Draw(gameTime);

            Game.sb.End();
        }

        public virtual void start()
        {
            Enabled = true;
            Visible = true;

            cm.focusFirst();
        }

        public virtual void resume()
        {
            Visible = true;
            Enabled = true;

            cm.Enabled = true;
        }

        public virtual void pause()
        {
            Visible = false;
            Enabled = false;

            cm.Enabled = false;
        }

        public virtual void stop()
        {
            Enabled = false;
            Visible = false;
        }
    }
}
