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
        Label lbl_city;
        Label lbl_cityName;

        public WorldMap()
        {
            cm.ArrowEnable = false;

            map = new Map(Content.Instance.gen);
            map.Fog = GameState.CurrentState.gen;
            map.changeRegion = changeRegion;
            map.CharLs.Add(GameState.CurrentState.mainCharPos, Graphic.getSprite(GameState.CurrentState.mainChar));
            cm.add(map);

            lbl_city = new Label("City");
            lbl_city.Color = Color.Blue;
            lbl_city.Position = new Vector2(150, 400);
            lbl_city.Visible = false;
            cm.add(lbl_city);

            lbl_cityName = new Label("Name:");
            lbl_cityName.Color = Color.White;
            lbl_cityName.Position = new Vector2(150, 450);
            lbl_cityName.Visible = false;
            cm.add(lbl_cityName);

            Label lbl_a = new Label("A");
            lbl_a.Color=Color.Blue;
            lbl_a.Position=new Vector2(450, 400);
            cm.add(lbl_a);

            Label lbl_army = new Label("Army Screen");
            lbl_army.Color = Color.White;
            lbl_army.Position = new Vector2(550, 400);
            cm.add(lbl_army);

            Label lbl_enter = new Label("Enter");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            Label lbl_reg = new Label("Enter Region");
            lbl_reg.Color = Color.White;
            lbl_reg.Position = new Vector2(550, 450);
            cm.add(lbl_reg);
        }

        private void changeRegion(object o, EventArgs e)
        {
            Region r = new Region((Tilemap)o);
            StateManager.Instance.goForward(r);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goForward(new PauseMenu());
            else if (InputHandler.keyReleased(Keys.A))
                StateManager.Instance.goForward(new ArmyManage());
        }
    }
}
