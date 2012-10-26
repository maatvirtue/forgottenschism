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

        public Region(Tilemap tm)
        {
            cm.ArrowEnable = false;

            map = new Map(tm);
            cm.add(map);

            Label lbl_title = new Label("Battle Screen");
            lbl_title.Position = new Vector2(100, 400);
            lbl_title.Color = Color.Blue;
        }

        private void changeRegion(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new BattleField((Tilemap)o));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();
        }
    }
}
