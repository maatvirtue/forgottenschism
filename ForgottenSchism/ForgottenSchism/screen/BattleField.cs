using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.control;
using ForgottenSchism.world;
using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    class BattleField: Screen
    {
        Map map;

        public BattleField(Tilemap tm)
        {
            cm.ArrowEnable = false;

            map = new Map(tm);
            cm.add(map);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();
        }
    }
}
