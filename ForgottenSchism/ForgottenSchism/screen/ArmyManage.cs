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
        //documentation
        Army army;
        Label lbl_UnitList;
        Menu menu_units;
        Label lbl_unitComp;
        Menu menu_chars;
        int sel;

        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_r;
        Label lbl_rAction;
        Label lbl_n;
        Label lbl_nAction;

        Boolean standby = false;

        DialogTxt txt_renameUnit;

        public ArmyManage()
        {
            txt_renameUnit = new DialogTxt("Rename unit: ");
            txt_renameUnit.Position = new Vector2(150, 100);
            txt_renameUnit.Enabled = false;
            txt_renameUnit.Visible = false;
            txt_renameUnit.complete = dialog_complete;
            cm.addLastDraw(txt_renameUnit);

            army = GameState.CurrentState.mainArmy;

            lbl_UnitList = new Label("Unit List");
            lbl_UnitList.Color = Color.Gold;
            lbl_UnitList.Position = new Vector2(50, 30);

            menu_units = new Menu(12);
            menu_units.Position = new Vector2(70, 60);

            foreach (Unit u in army.Units)
            {
                menu_units.add(new Link(u.Name));
            }

            menu_units.add(new Link("Standby Soldiers"));

            sel = menu_units.Selected;

            lbl_unitComp = new Label("Unit Composition");
            lbl_unitComp.Color = Color.Gold;
            lbl_unitComp.Position = new Vector2(430, 30);

            menu_chars = new Menu(12);
            menu_chars.Position = new Vector2(450, 60);

            if (army.Units.Count > 0)
            {
                foreach (Character c in army.Units[sel].Characters)
                {
                    menu_chars.add(new Link(c.Name));
                }
            }
            else
            {
                foreach (Character c in army.Standby)
                {
                    menu_chars.add(new Link(c.Name));
                }
            }
            
            menu_chars.TabStop = false;
            menu_chars.unfocusLink();


            lbl_enter = new Label("ENTER");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(50, 500);

            lbl_enterAction = new Label("Manage Unit");
            lbl_enterAction.Color = Color.White;
            lbl_enterAction.Position = new Vector2(130, 500);

            lbl_r = new Label("R");
            lbl_r.Color = Color.Blue;
            lbl_r.Position = new Vector2(50, 440);

            lbl_rAction = new Label("Remove Unit");
            lbl_rAction.Color = Color.White;
            lbl_rAction.Position = new Vector2(80, 440);

            lbl_n = new Label("N");
            lbl_n.Color = Color.Blue;
            lbl_n.Position = new Vector2(400, 490);

            lbl_nAction = new Label("Rename Unit");
            lbl_nAction.Color = Color.White;
            lbl_nAction.Position = new Vector2(430, 490);

            cm.add(lbl_UnitList);
            cm.add(lbl_unitComp);
            cm.add(menu_units);
            cm.add(menu_chars);
            cm.add(lbl_enter);
            cm.add(lbl_enterAction);
            cm.add(lbl_r);
            cm.add(lbl_rAction);
            cm.add(lbl_n);
            cm.add(lbl_nAction);
        }

        public override void resumeUpdate()
        {
            menu_units.clear();

            if (army.Units.Count > 0)
            {
                foreach (Unit u in army.Units)
                {
                    menu_units.add(new Link(u.Name));
                }

                sel = menu_units.Selected;
            }
            else
            {
                sel = 0;
            }

            menu_units.add(new Link("Standby Soldiers"));

            if (sel < army.Units.Count)
            {
                menu_chars.clear();
                foreach (Character c in army.Units[sel].Characters)
                {
                    menu_chars.add(new Link(c.Name));
                }
                menu_chars.unfocusLink();

                menu_units.TabStop = true;
                menu_chars.TabStop = false;
                standby = false;

                lbl_enterAction.Text = "Manage Unit";
                lbl_r.Visible = true;
                lbl_rAction.Visible = true;
                lbl_n.Visible = true;
            }
            else
            {
                menu_chars.clear();
                foreach (Character c in army.Standby)
                {
                    menu_chars.add(new Link(c.Name));
                }
                menu_chars.unfocusLink();

                lbl_enterAction.Text = "View Standby";
                lbl_r.Visible = false;
                lbl_rAction.Visible = false;
                lbl_rAction.Visible = false;
                lbl_n.Visible = false;
            }
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
                        menu_chars.add(new Link(c.Name));
                    }
                    menu_chars.unfocusLink();

                    lbl_enterAction.Text = "Manage Unit";
                    lbl_r.Visible = true;
                    lbl_rAction.Visible = true;
                    lbl_rAction.Visible = true;
                    lbl_n.Visible = true;
                }
                else
                {
                    menu_chars.clear();
                    foreach(Character c in army.Standby)
                    {
                        menu_chars.add(new Link(c.Name));
                    }
                    menu_chars.unfocusLink();

                    lbl_enterAction.Text = "View Standby";
                    lbl_r.Visible = false;
                    lbl_rAction.Visible = false;
                    lbl_rAction.Visible = false;
                    lbl_n.Visible = false;
                }
            }

            if (InputHandler.keyReleased(Keys.Escape))
            {
                if (standby)
                {
                    menu_units.TabStop = true;
                    menu_chars.TabStop = false;
                    menu_chars.unfocusLink();
                    standby = false;
                    lbl_enterAction.Text = "View Standby";
                    lbl_r.Visible = true;
                    lbl_rAction.Visible = true;
                    lbl_rAction.Visible = true;
                    lbl_n.Visible = true;
                }
                else
                    StateManager.Instance.goBack();
            }

            if (InputHandler.keyReleased(Keys.Enter))
            {
                if (standby)
                {
                    if(army.Standby.Count>0)
                        StateManager.Instance.goForward(new CharManage(army.Standby[menu_chars.Selected]));
                }
                else
                {
                    if (lbl_enterAction.Text == "View Standby")
                    {
                        if(menu_chars.ListItems.Count > 0)
                        {
                            menu_units.TabStop = false;
                            menu_chars.TabStop = true;
                            menu_chars.refocusLink();
                            standby = true;
                            lbl_enterAction.Text = "View Character";
                            lbl_r.Visible = false;
                            lbl_rAction.Visible = false;
                        }
                    }
                    else
                    {
                        StateManager.Instance.goForward(new UnitManage(army, menu_units.Selected));
                    }
                }
            }

            if (InputHandler.keyReleased(Keys.R) && lbl_r.Visible)
            {
                foreach (Character c in army.Units[menu_units.Selected].Characters)
                {
                    army.Standby.Add(c);
                }

                army.Units.Remove(army.Units[menu_units.Selected]);
                resumeUpdate();
            }

            if (InputHandler.keyReleased(Keys.N) && lbl_n.Visible)
            {
                dialog_showTxt(null, null);
            }
        }

        private void dialog_complete(object sender, EventArgs e)
        {
            String s = ((EventArgObject)e).o.ToString();
            army.Units[sel].Name = s;

            InputHandler.flush();
            txt_renameUnit.Enabled = false;
            txt_renameUnit.Visible = false;
            cm.Enabled = true;
        }

        private void dialog_showTxt(object sender, EventArgs e)
        {
            InputHandler.flush();
            txt_renameUnit.Enabled = true;
            txt_renameUnit.Visible = true;
            cm.Enabled = false;
        }
    }
}
