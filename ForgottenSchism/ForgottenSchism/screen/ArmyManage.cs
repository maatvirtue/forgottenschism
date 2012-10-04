using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class ArmyManage : Screen
    {
        Army army = new Army();

        public ArmyManage(Game1 game) : base(game)
        {
            Label lbl_UnitList = new Label(game, "Unit List");
            lbl_UnitList.Color = Color.Gold;
            lbl_UnitList.Position = new Vector2(50, 30);

            Menu menu_units = new Menu(game, 10);
            foreach (Unit u in army.Units)
            {
                menu_units.add(new Link(game, u.Name));
            }
            menu_units.Position = new Vector2(50, 60);

            cm.add(lbl_UnitList);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
        }
    }
}
