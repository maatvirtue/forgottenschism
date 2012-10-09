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
        Label lbl_UnitList;
        Menu menu_units;
        Label lbl_unitComp;
        Menu menu_chars;
        int sel;

        Game1 game;

        public ArmyManage(Game1 game) : base(game)
        {
            army.GenerateArmy();

            this.game = game;

            lbl_UnitList = new Label(game, "Unit List");
            lbl_UnitList.Color = Color.Gold;
            lbl_UnitList.Position = new Vector2(50, 30);

            menu_units = new Menu(game, 12);
            
            foreach (Unit u in army.Units)
            {
                menu_units.add(new Link(game, u.Name));
            }

            menu_units.add(new Link(game, "Standby Soldiers"));
            menu_units.Position = new Vector2(70, 60);

            sel = menu_units.Selected;

            lbl_unitComp = new Label(game, "Unit Composition");
            lbl_unitComp.Color = Color.Gold;
            lbl_unitComp.Position = new Vector2(430, 30);

            menu_chars = new Menu(game, 12);
            foreach(Character c in army.Units[sel].Characters)
            {
                menu_chars.add(new Link(game, c.Name));
            }
            menu_chars.Position = new Vector2(450, 60);
            menu_chars.ArrowEnabled = false;
            menu_chars.TabStop = false;
            menu_chars.unfocus();

            cm.add(lbl_UnitList);
            cm.add(lbl_unitComp);
            cm.add(menu_units);
            cm.add(menu_chars);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (menu_units.Selected != sel)
            {
                sel = menu_units.Selected;
                if (sel < army.Units.Count)
                {
                    menu_chars.clear();
                    foreach (Character c in army.Units[sel].Characters)
                    {
                        menu_chars.add(new Link(game, c.Name));
                    }
                    menu_chars.unfocus();
                }
                else
                {
                    menu_chars.clear();
                    foreach(Character c in army.Standby)
                    {
                        menu_chars.add(new Link(game, c.Name));
                    }
                    menu_chars.unfocus();
                }
            }

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
        }
    }
}
