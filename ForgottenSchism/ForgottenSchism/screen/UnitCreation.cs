using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class UnitCreation : Screen
    {
        Label lbl_unitCre;
        Label lbl_chooseLdr;

        TextBox txt_unitName;

        Menu menu_leader;

        Link lnk_choose;
        Link lnk_create;

        Army army;

        Boolean choosing;

        public UnitCreation(Army a)
        {
            choosing = false;

            army = a;

            lbl_unitCre = new Label("Unit Creation");
            lbl_unitCre.Color = Color.Gold;
            lbl_unitCre.Position = new Vector2(50, 30);

            lbl_chooseLdr = new Label("Available Characters");
            lbl_chooseLdr.Color = Color.Gold;
            lbl_chooseLdr.Position = new Vector2(400, 90);

            txt_unitName = new TextBox(10);
            txt_unitName.Position = new Vector2(90, 90);

            menu_leader = new Menu(10);
            menu_leader.Position = new Vector2(400, 90);
            foreach(Character c in army.Standby)
            {
                menu_leader.add(new Link(c.Name));
            }
            menu_leader.TabStop = false;

            lnk_create = new Link("Create Unit");
            lnk_create.Position = new Vector2(90, 210);
            lnk_create.selected = create;

            lnk_choose = new Link("Choose Leader");
            lnk_choose.Position = new Vector2(90, 150);
            lnk_choose.selected = choose;

            cm.add(lbl_unitCre);
            cm.add(lbl_chooseLdr);
            cm.add(txt_unitName);
            cm.add(menu_leader);
            cm.add(lnk_choose);
            cm.add(lnk_create);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                if (choosing)
                {
                    choosing = false;
                    menu_leader.TabStop = false;
                    txt_unitName.TabStop = true;
                    lnk_choose.TabStop = true;
                    lnk_create.TabStop = true;

                    menu_leader.HasFocus = false;
                    lnk_choose.HasFocus = true;
                }
                else
                {
                    StateManager.Instance.goBack();
                }
            }
        }

        private void create(object sender, EventArgs e)
        {
            army.Units.Add(new Unit(army.Standby[menu_leader.Selected], txt_unitName.Text));
            army.Standby.RemoveAt(menu_leader.Selected);
            StateManager.Instance.changeCur(new UnitManage(army, army.Units.Count - 1));
        }

        private void choose(object sender, EventArgs e)
        {
            choosing = true;

            menu_leader.TabStop = true;
            txt_unitName.TabStop = false;
            lnk_choose.TabStop = false;
            lnk_create.TabStop = false;

            lnk_choose.HasFocus = false;
            menu_leader.HasFocus = true;
        }
    }
}
