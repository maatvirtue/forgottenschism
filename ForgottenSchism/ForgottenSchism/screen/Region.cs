using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class Region: Screen
    {
        Map map;

        public Region(Game1 game): base(game)
        {
            cm.ArrowEnable = false;

            map = new Map(game, null);
            cm.add(map);
        }

        public void load(Tilemap tm)
        {
            map.load(tm);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
        }
    }
}
