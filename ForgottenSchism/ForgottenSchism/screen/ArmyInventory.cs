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
    public class ArmyInventory: Screen
    {
        Label lbl_title;
        Label lbl_money;
        Label lbl_cmoney;
        Label lbl_items;
        Menu menu_items;
        Menu menu_units;
        Label lbl_units;
        Label lbl_enter;
        Label lbl_enterGive;
        Label lbl_v;
        Label lbl_vView;
        Inventory inv;

        bool inMenuItem;

        public ArmyInventory()
        {
            inv = GameState.CurrentState.mainArmy.Inventory;

            lbl_money = new Label("Money");
            lbl_money.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_money.Position = new Vector2(50, 50);
            MainWindow.add(lbl_money);

            lbl_cmoney = new Label(GameState.CurrentState.mainArmy.Money.ToString());
            lbl_cmoney.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_cmoney.Position = new Vector2(150, 50);
            MainWindow.add(lbl_cmoney);

            lbl_title = new Label("Army Inventory");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_title.Position = new Vector2(250, 100);
            MainWindow.add(lbl_title);

            lbl_items = new Label("Items");
            lbl_items.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_items.Position = new Vector2(90, 130);
            MainWindow.add(lbl_items);

            menu_items = new Menu(10);
            menu_items.Position = new Vector2(70, 150);
            MainWindow.add(menu_items);

            lbl_units = new Label("Units");
            lbl_units.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_units.Position = new Vector2(450, 130);
            MainWindow.add(lbl_units);

            menu_units = new Menu(10);
            menu_units.Position = new Vector2(430, 160);
            MainWindow.add(menu_units);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(50, 440);
            MainWindow.add(lbl_enter);

            lbl_enterGive = new Label("Give Item to Unit");
            lbl_enterGive.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_enterGive.Position = new Vector2(130, 440);
            MainWindow.add(lbl_enterGive);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(100, 470);
            MainWindow.add(lbl_v);

            lbl_vView = new Label("View Item");
            lbl_vView.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_vView.Position = new Vector2(130, 470);
            MainWindow.add(lbl_vView);

            update_menuUnits();
            update_menuItems();

            inMenuItem = true;

            menu_units.TabStop = false;
            menu_units.unfocusLink();
        }

        /// <summary>
        /// updates menu_units
        /// </summary>
        private void update_menuUnits()
        {
            menu_units.clear();

            foreach (Unit u in GameState.CurrentState.mainArmy.Units)
                if (!u.Deployed)
                    menu_units.add(new Link(u.Name));
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
                lbl_enter.Visible = false;
                lbl_enterGive.Visible = false;
                lbl_v.Visible = false;
                lbl_vView.Visible = false;
            }
            else
            {
                if (menu_units.Count > 0)
                {
                    lbl_enter.Visible = true;
                    lbl_enterGive.Visible = true;
                }
                else
                {
                    lbl_enter.Visible = false;
                    lbl_enterGive.Visible = false;
                }

                lbl_v.Visible = true;
                lbl_vView.Visible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                if (inMenuItem)
                    StateManager.Instance.goBack();
                else
                {
                    lbl_v.Visible = true;
                    lbl_vView.Visible = true;

                    menu_items.TabStop = true;
                    menu_units.TabStop = false;
                    menu_items.HasFocus = true;
                    menu_units.HasFocus = false;

                    menu_units.unfocusLink();

                    inMenuItem = true;
                }
            }

            if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
            {
                StateManager.Instance.goForward(new ItemManage(inv.Items[menu_items.Selected]));
            }

            if (InputHandler.keyReleased(Keys.Enter)&&inv.Items.Count>0)
            {
                if (inMenuItem)
                {
                    //the user just selected an item to give

                    if (menu_units.Count > 0)
                    {
                        lbl_v.Visible = false;
                        lbl_vView.Visible = false;

                        menu_items.TabStop = false;
                        menu_units.TabStop = true;
                        menu_items.HasFocus = false;
                        menu_units.HasFocus = true;

                        menu_units.refocusLink();

                        inMenuItem = false;
                    }
                }
                else
                {
                    //the user selected a unit to give the item to

                    inv.give(menu_items.Selected, GameState.CurrentState.mainArmy.Units[menu_units.Selected].Inventory);

                    lbl_v.Visible = true;
                    lbl_vView.Visible = true;

                    menu_items.TabStop = true;
                    menu_units.TabStop = false;
                    menu_items.HasFocus = true;
                    menu_units.HasFocus = false;

                    menu_units.unfocusLink();

                    update_menuItems();

                    inMenuItem = true;
                }
            }
        }
    }
}
