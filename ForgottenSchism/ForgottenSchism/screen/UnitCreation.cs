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
        Label lbl_err;

        Label lbl_v;
        Label lbl_vAction;
        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_esc;
        Label lbl_escAction;

        TextBox txt_unitName;

        Menu menu_leader;

        Link lnk_choose;
        Link lnk_create;

        Army army;

        Boolean choosing;

        int sel;

        public UnitCreation(Army a)
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            sel = 0;

            choosing = false;

            army = a;

            lbl_unitCre = new Label("Unit Creation");
            lbl_unitCre.Color = Color.Gold;
            lbl_unitCre.Position = new Vector2(50, 30);
            MainWindow.add(lbl_unitCre);

            lbl_chooseLdr = new Label("Available Characters");
            lbl_chooseLdr.Color = Color.Gold;
            lbl_chooseLdr.Position = new Vector2(400, 90);
            MainWindow.add(lbl_chooseLdr);

            txt_unitName = new TextBox(10);
            txt_unitName.Position = new Vector2(130, 150);
            MainWindow.add(txt_unitName);

            lnk_choose = new Link("Choose Leader");
            lnk_choose.Position = new Vector2(130, 210);
            lnk_choose.selected = choose;
            MainWindow.add(lnk_choose);

            lnk_create = new Link("Create Unit");
            lnk_create.Position = new Vector2(130, 270);
            lnk_create.selected = create;
            MainWindow.add(lnk_create);

            menu_leader = new Menu(10);
            menu_leader.Position = new Vector2(400, 90);
            foreach(Character c in army.Standby)
            {
                menu_leader.add(new Link(c.Name));
            }
            menu_leader.TabStop = false;
            MainWindow.add(menu_leader);

            lbl_err = new Label("You must name your new unit!");
            lbl_err.Position = new Vector2(90, 330);
            lbl_err.Color = Color.Red;
            lbl_err.Visible = false;
            MainWindow.add(lbl_err);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_v.Position = new Vector2(50, 440);
            lbl_v.Visible = false;
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Position = new Vector2(80, 440);
            lbl_vAction.Visible = false;
            MainWindow.add(lbl_vAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_enter.Position = new Vector2(50, 470);
            lbl_enter.Visible = false;
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("");
            lbl_enterAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_enterAction.Position = new Vector2(120, 470);
            lbl_enterAction.Visible = false;
            MainWindow.add(lbl_enterAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_esc.Position = new Vector2(50, 500);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_escAction.Position = new Vector2(100, 500);
            MainWindow.add(lbl_escAction);
        }

        private void showLabels()
        {
            if (choosing)
            {
                lbl_enterAction.Text = "Confirm Leader";
                lbl_enter.Visible = true;
                lbl_enterAction.Visible = true;

                lbl_escAction.Text = "Cancel Selection";

                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
            }
            else
            {
                lbl_enter.Visible = false;
                lbl_enterAction.Visible = false;

                lbl_escAction.Text = "Go Back";

                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                if (choosing)
                {
                    choosing = false;
                    menu_leader.Enabled = false;
                    txt_unitName.Enabled = true;
                    lnk_choose.Enabled = true;
                    lnk_create.Enabled = true;

                    lnk_choose.HasFocus = true;
                    menu_leader.HasFocus = false;

                    menu_leader.Selected = sel;

                    showLabels();
                }
                else
                {
                    StateManager.Instance.goBack();
                }
            }

            if (InputHandler.keyReleased(Keys.Enter) && choosing)
            {
                choosing = false;
                menu_leader.Enabled = false;
                txt_unitName.Enabled = true;
                lnk_choose.Enabled = true;
                lnk_create.Enabled = true;

                lnk_choose.HasFocus = true;
                menu_leader.HasFocus = false;

                sel = menu_leader.Selected;

                showLabels();
            }

            if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
            {
                StateManager.Instance.goForward(new CharManage(army.Standby[menu_leader.Selected], null));
            }
        }

        private void create(object sender, EventArgs e)
        {
            if (txt_unitName.Text != "")
            {
                army.Units.Add(new Unit(army.Standby[menu_leader.Selected], txt_unitName.Text, "main"));
                army.Standby.RemoveAt(menu_leader.Selected);

                GameState.CurrentState.saved = false;

                StateManager.Instance.changeCur(new UnitManage(army, army.Units.Count - 1));
            }
            else
            {
                lbl_err.visibleTemp(2000);
            }
        }

        private void choose(object sender, EventArgs e)
        {
            choosing = true;

            menu_leader.Enabled = true;
            txt_unitName.Enabled = false;
            lnk_choose.Enabled = false;
            lnk_create.Enabled = false;

            menu_leader.HasFocus = true;
            lnk_choose.HasFocus = false;

            showLabels();

            InputHandler.flush();
        }
    }
}
