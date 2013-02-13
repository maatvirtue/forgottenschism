using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    class UnitInventory: Screen
    {
        Label lbl_title;
        Label lbl_money;
        Label lbl_cmoney;
        Label lbl_items;
        Menu menu_items;
        Label lbl_r;
        Label lbl_rReturn;
        Label lbl_v;
        Label lbl_vView;
        Label lbl_esc;
        Label lbl_escAction;
        Inventory inv;

        public UnitInventory(Unit u)
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            inv = u.Inventory;

            lbl_money = new Label("Money");
            lbl_money.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_money.Position = new Vector2(50, 50);
            MainWindow.add(lbl_money);

            lbl_cmoney = new Label(GameState.CurrentState.mainArmy.Money.ToString());
            lbl_cmoney.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cmoney.Position = new Vector2(150, 50);
            MainWindow.add(lbl_cmoney);

            lbl_title = new Label("Unit Inventory");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_title.Position = new Vector2(250, 100);
            MainWindow.add(lbl_title);

            lbl_items = new Label("Items");
            lbl_items.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_items.Position = new Vector2(90, 130);
            MainWindow.add(lbl_items);

            menu_items = new Menu(8);
            menu_items.Position = new Vector2(70, 150);
            MainWindow.add(menu_items);

            lbl_r = new Label("R");
            lbl_r.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_r.Position = new Vector2(50, 440);
            MainWindow.add(lbl_r);

            lbl_rReturn = new Label("Return to army inventory");
            lbl_rReturn.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_rReturn.Position = new Vector2(80, 440);
            MainWindow.add(lbl_rReturn);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(50, 470);
            MainWindow.add(lbl_v);

            lbl_vView = new Label("View Item");
            lbl_vView.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_vView.Position = new Vector2(80, 470);
            MainWindow.add(lbl_vView);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_esc.Position = new Vector2(50, 500);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_escAction.Position = new Vector2(100, 500);
            MainWindow.add(lbl_escAction);

            update_menuItems();
        }

        /// <summary>
        /// updates menu_items
        /// </summary>
        private void update_menuItems()
        {
            menu_items.clear();

            foreach (Item i in inv.Items)
                menu_items.add(new Link(i.Name));

            if (inv.Items.Count == 0)
            {
                lbl_r.Visible = false;
                lbl_rReturn.Visible = false;
                lbl_v.Visible = false;
                lbl_vView.Visible = false;
            }
            else
            {
                lbl_r.Visible = true;
                lbl_rReturn.Visible = true;
                lbl_v.Visible = true;
                lbl_vView.Visible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                    StateManager.Instance.goBack();

            if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
            {
                StateManager.Instance.goForward(new ItemManage(inv.Items[menu_items.Selected]));
            }

            if (InputHandler.keyReleased(Keys.R)&&inv.Items.Count>0)
            {
                inv.give(menu_items.Selected, GameState.CurrentState.mainArmy.Inventory);
                update_menuItems();

                GameState.CurrentState.saved = false;
            }
        }
    }
}
