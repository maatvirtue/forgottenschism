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

        TextBox txt_unitName;

        Select sel_leader;

        Link lnk_create;

        Army army;

        public UnitCreation(Army a)
        {
            army = a;

            lbl_unitCre = new Label("Unit Creation");
            lbl_unitCre.Color = Color.Gold;
            lbl_unitCre.Position = new Vector2(50, 30);

            txt_unitName = new TextBox(10);
            txt_unitName.Position = new Vector2(90, 90);

            sel_leader = new Select();
            sel_leader.Position = new Vector2(90, 150);
            foreach(Character c in army.Standby)
            {
                sel_leader.add(c.Name);
            }

            lnk_create = new Link("Create Unit");
            lnk_create.Position = new Vector2(90, 210);
            lnk_create.selected = create;

            cm.add(lbl_unitCre);
            cm.add(txt_unitName);
            cm.add(sel_leader);
            cm.add(lnk_create);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
        }

        private void create(object sender, EventArgs e)
        {
            army.Units.Add(new Unit(army.Standby[sel_leader.Selection], txt_unitName.Text));
            army.Standby.RemoveAt(sel_leader.Selection);
            StateManager.Instance.goBack();
        }
    }
}
