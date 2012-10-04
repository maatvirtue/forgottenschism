using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class WorldMap: Screen
    {
        Map map;

        public WorldMap(Game1 game): base(game)
        {
            cm.ArrowEnable = false;

            map = new Map(game, new Tilemap("gen"));
            map.changeRegion = changeRegion;
            cm.add(map);

            Label lbl_a = new Label(game, "A");
            lbl_a.Color=Color.Blue;
            lbl_a.Position=new Vector2(450, 400);
            cm.add(lbl_a);

            Label lbl_army = new Label(game, "Army Screen");
            lbl_army.Color = Color.White;
            lbl_army.Position = new Vector2(550, 400);
            cm.add(lbl_army);

            Label lbl_enter = new Label(game, "Enter");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            Label lbl_reg = new Label(game, "Enter Region");
            lbl_reg.Color = Color.White;
            lbl_reg.Position = new Vector2(550, 450);
            cm.add(lbl_reg);
        }

        private void changeRegion(object o, EventArgs e)
        {
            Game.region.load((Tilemap)o);
            Game.stateMng.goForward(Game.region);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
            else if (InputHandler.keyReleased(Keys.A))
                Game.stateMng.goForward(Game.armyManage);
        }
    }
}
