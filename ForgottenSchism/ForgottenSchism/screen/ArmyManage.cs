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
        public delegate bool TestEventHandler(object sender, object args);

        bool fromRegion = false;

        Army army;
        Label lbl_UnitList;
        Menu menu_units;
        Label lbl_unitComp;
        Menu menu_chars;
        int sel;

        Label lbl_a;
        Label lbl_aAction;
        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_r;
        Label lbl_rAction;
        Label lbl_n;
        Label lbl_nAction;
        Label lbl_d;
        Label lbl_dAction;

        Boolean standby = false;

        DialogTxt txt_renameUnit;

        public TestEventHandler deploy;

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

            menu_units = new Menu(14);
            menu_units.Position = new Vector2(70, 60);

            foreach (Unit u in army.Units)
            {
                Link l = new Link(u.Name);
                if (u.Deployed)
                {
                    l.NormColor = Color.Gray;
                    l.SelColor = Color.Orange;
                    fromRegion = true;
                }
                menu_units.add(l);
            }

            menu_units.add(new Link("Standby Soldiers"));

            sel = menu_units.Selected;

            lbl_unitComp = new Label("Unit Composition");
            lbl_unitComp.Color = Color.Gold;
            lbl_unitComp.Position = new Vector2(430, 30);

            lbl_a = new Label("A");
            lbl_a.Color = Color.Blue;
            lbl_a.Position = new Vector2(50, 470);

            lbl_aAction = new Label("Add Unit");
            lbl_aAction.Color = Color.White;
            lbl_aAction.Position = new Vector2(80, 470);

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
            lbl_n.Position = new Vector2(400, 500);

            lbl_nAction = new Label("Rename Unit");
            lbl_nAction.Color = Color.White;
            lbl_nAction.Position = new Vector2(430, 500);

            lbl_d = new Label("D");
            lbl_d.Color = Color.Blue;
            lbl_d.Position = new Vector2(400, 440);

            lbl_dAction = new Label("Deploy Unit");
            lbl_dAction.Color = Color.White;
            lbl_dAction.Position = new Vector2(430, 440);

            menu_chars = new Menu(14);
            menu_chars.Position = new Vector2(450, 60);

            if (army.Units.Count > 0)
            {
                foreach (Character c in army.Units[sel].Characters)
                {
                    menu_chars.add(new Link(c.Name));
                }
                visible();
            }
            else
            {
                foreach (Character c in army.Standby)
                {
                    menu_chars.add(new Link(c.Name));
                }
                invisible();
                lbl_enterAction.Text = "View Standby";
            }

            menu_chars.TabStop = false;
            menu_chars.unfocusLink();

            cm.add(lbl_UnitList);
            cm.add(lbl_unitComp);
            cm.add(menu_units);
            cm.add(menu_chars);
            cm.add(lbl_a);
            cm.add(lbl_aAction);
            cm.add(lbl_enter);
            cm.add(lbl_enterAction);
            cm.add(lbl_r);
            cm.add(lbl_rAction);
            cm.add(lbl_n);
            cm.add(lbl_nAction);
            cm.add(lbl_d);
            cm.add(lbl_dAction);
        }

        public override void resume()
        {
            base.resume();

            menu_units.clear();

            if (army.Units.Count > 0)
            {
                foreach (Unit u in army.Units)
                {
                    Link l = new Link(u.Name);
                    if (u.Deployed)
                    {
                        l.NormColor = Color.Gray;
                        l.SelColor = Color.Orange;
                    }
                    menu_units.add(l);
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
                visible();
            }
            else
            {
                menu_chars.clear();
                foreach (Character c in army.Standby)
                {
                    menu_chars.add(new Link(c.Name));
                }

                lbl_enterAction.Text = "View Standby";
                invisible();
            }
        }

        public void visible()
        {
            lbl_n.Visible = true;
            lbl_nAction.Visible = true;

            if (army.Standby.Count > 0)
            {
                lbl_a.Visible = true;
                lbl_aAction.Visible = true;
            }
            else
            {
                lbl_a.Visible = false;
                lbl_aAction.Visible = false;
            }

            if (army.Units[menu_units.Selected].Deployed)
            {
                lbl_d.Visible = false;
                lbl_dAction.Text = "DEPLOYED";
                lbl_dAction.Color = Color.Gold;
                lbl_dAction.Visible = true;

                lbl_r.Visible = false;
                lbl_rAction.Visible = false;
            }
            else
            {
                if (army.Units[menu_units.Selected].isMainUnit())
                {
                    lbl_r.Visible = false;
                    lbl_rAction.Visible = false;
                }
                else
                {
                    lbl_r.Visible = true;
                    lbl_rAction.Visible = true;
                }
                lbl_d.Visible = true;
                lbl_dAction.Text = "Deploy Unit";
                lbl_dAction.Color = Color.White;
                lbl_dAction.Visible = true;

                if (deploy == null)
                {
                    lbl_d.Visible = false;
                    lbl_dAction.Visible = false;
                }
            }
        }

        public void invisible()
        {
            lbl_r.Visible = false;
            lbl_rAction.Visible = false;
            lbl_n.Visible = false;
            lbl_nAction.Visible = false;
            lbl_d.Visible = false;
            lbl_dAction.Visible = false;
            
            lbl_a.Visible = false;
            lbl_aAction.Visible = false;
            
            if(!standby && army.Standby.Count > 0)
            {
                lbl_a.Visible = true;
                lbl_aAction.Visible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (txt_renameUnit.Enabled)
                txt_renameUnit.handleInput(gameTime);
            else
            {
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
                        visible();
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
                        invisible();
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
                        invisible();
                    }
                    else
                        StateManager.Instance.goBack();
                }

                if (InputHandler.keyReleased(Keys.Enter))
                {
                    if (standby)
                    {
                        if (army.Standby.Count > 0)
                            StateManager.Instance.goForward(new CharManage(army.Standby[menu_chars.Selected]));
                    }
                    else
                    {
                        if (lbl_enterAction.Text == "View Standby")
                        {
                            if (menu_chars.Count > 0)
                            {
                                menu_units.TabStop = false;
                                menu_chars.TabStop = true;
                                menu_chars.refocusLink();
                                standby = true;
                                lbl_enterAction.Text = "View Character";
                                invisible();
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
                    resume();
                }

                if (InputHandler.keyReleased(Keys.N) && lbl_n.Visible)
                {
                    dialog_showTxt(null, null);
                }

                if (InputHandler.keyReleased(Keys.D) && menu_units.Selected < army.Units.Count)
                {
                    if (army.Units[menu_units.Selected].Deployed)
                        army.Units[menu_units.Selected].Deployed = false;
                    else
                    {
                        if (deploy != null && deploy(this, army.Units[menu_units.Selected]))
                            army.Units[menu_units.Selected].Deployed = true;
                    }

                    resume();
                }

                if (InputHandler.keyReleased(Keys.A) && lbl_a.Visible)
                {
                    StateManager.Instance.goForward(new UnitCreation(army));
                }
            }
        }

        private void dialog_complete(object sender, EventArgs e)
        {
            String s = ((EventArgObject)e).o.ToString();
            if(s != String.Empty)
                army.Units[sel].Name = s;

            InputHandler.flush();
            txt_renameUnit.Enabled = false;
            txt_renameUnit.Visible = false;
            cm.Enabled = true;

            resume();
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
