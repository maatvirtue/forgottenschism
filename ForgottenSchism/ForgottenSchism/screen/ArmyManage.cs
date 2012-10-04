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

            Menu menu_units = new Menu(game, 15);
            //foreach (Unit u in army.Units)
            //{
            //    menu_units.add(new Link(game, u.Name));
            //}
            menu_units.add(new Link(game, "1"));
            menu_units.add(new Link(game, "2"));
            menu_units.add(new Link(game, "3"));
            menu_units.add(new Link(game, "4"));
            menu_units.add(new Link(game, "5"));
            menu_units.add(new Link(game, "6"));
            menu_units.add(new Link(game, "7"));
            menu_units.add(new Link(game, "8"));
            menu_units.add(new Link(game, "9"));
            menu_units.add(new Link(game, "10"));
            menu_units.add(new Link(game, "11"));
            menu_units.add(new Link(game, "12"));
            menu_units.add(new Link(game, "13"));
            menu_units.add(new Link(game, "14"));
            menu_units.add(new Link(game, "15"));
            menu_units.add(new Link(game, "16"));
            menu_units.add(new Link(game, "17"));
            menu_units.add(new Link(game, "18"));
            menu_units.add(new Link(game, "19"));
            menu_units.add(new Link(game, "20"));
            menu_units.Position = new Vector2(70, 60);

            Label lbl_unitComp = new Label(game, "Unit Composition");
            lbl_unitComp.Color = Color.Gold;
            lbl_unitComp.Position = new Vector2(430, 30);

            Menu menu_chars = new Menu(game, 15);
            foreach(Character c in army.Units.Characters)
            {
                menu_chars.add(new Link(game, c.Name))
            }

            cm.add(lbl_UnitList);
            cm.add(lbl_unitComp);
            cm.add(menu_units);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
        }
    }
}
