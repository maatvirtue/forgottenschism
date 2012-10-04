using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    class Save: Screen
    {
        public Save(Game1 game): base(game)
        {
            //
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
        }
    }
}
