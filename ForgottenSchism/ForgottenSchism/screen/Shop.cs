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
    class Shop: Screen
    {
        Label lbl_title;
        Label lbl_item;
        Menu menu_item;
        Label lbl_money;
        Label lbl_cmoney;
        Label lbl_v;
        Label lbl_vAction;
        Label lbl_m;
        Label lbl_mAction;
        Label lbl_price;
        Label lbl_cprice;
        Label lbl_enter;
        Label lbl_enterAction;

        /// <summary>
        /// wether in buy (true) mode or sell (false) mode
        /// </summary>
        bool buymode;

        public Shop()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            buymode = true;

            lbl_title = new Label("Shop");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_title.Position = new Vector2(250, 100);
            MainWindow.add(lbl_title);

            lbl_item = new Label("Items");
            lbl_item.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_item.Position = new Vector2(90, 130);
            MainWindow.add(lbl_item);

            menu_item = new Menu(10);
            menu_item.Position = new Vector2(70, 150);
            MainWindow.add(menu_item);

            lbl_money = new Label("Money");
            lbl_money.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_money.Position=new Vector2(50, 50);
            MainWindow.add(lbl_money);

            lbl_cmoney = new Label(GameState.CurrentState.mainArmy.Money.ToString());
            lbl_cmoney.Position = new Vector2(150, 50);
            MainWindow.add(lbl_cmoney);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(100, 470);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Item");
            lbl_vAction.Position = new Vector2(130, 470);
            MainWindow.add(lbl_vAction);

            lbl_m = new Label("M");
            lbl_m.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_m.Position = new Vector2(100, 500);
            MainWindow.add(lbl_m);

            lbl_mAction = new Label("Sell Mode");
            lbl_mAction.Position = new Vector2(130, 500);
            MainWindow.add(lbl_mAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(50, 440);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Buy Item");
            lbl_enterAction.Position = new Vector2(130, 440);
            MainWindow.add(lbl_enterAction);

            update_menuItem();
        }

        /// <summary>
        /// updates the labels depending on the mode
        /// </summary>
        private void update_labels()
        {
            if (buymode)
            {
                lbl_mAction.Text = "Sell Mode";
                lbl_enterAction.Text="Buy Item";
            }
            else
            {
                lbl_mAction.Text = "Buy Mode";
                lbl_enterAction.Text = "Sell Item";
            }
        }

        /// <summary>
        /// Updates menu_item
        /// </summary>
        private void update_menuItem()
        {
            menu_item.clear();

            if (buymode)
            {
                foreach (Item i in Content.Instance.shop.Items)
                    menu_item.add(new Link(i.Name));
            }
            else
            {
                foreach (Item i in GameState.CurrentState.mainArmy.Inventory.Items)
                    menu_item.add(new Link(i.Name));
            }

            if (menu_item.Count == 0)
            {
                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
                lbl_enter.Visible = false;
                lbl_enterAction.Visible = false;
            }
            else
            {
                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
                lbl_enter.Visible = true;
                lbl_enterAction.Visible = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();

            if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
            {
                Item i;

                if (buymode)
                    i=Content.Instance.shop.Items[menu_item.Selected];
                else
                    i=GameState.CurrentState.mainArmy.Inventory.Items[menu_item.Selected];

                StateManager.Instance.goForward(new ItemManage(i));
            }

            if (InputHandler.keyReleased(Keys.Enter))
            {
                if (buymode)
                {
                    Item i=Content.Instance.shop.Items[menu_item.Selected];

                    if (GameState.CurrentState.mainArmy.Money >= i.Cost)
                    {
                        GameState.CurrentState.mainArmy.Inventory.Items.Add(i.clone());
                    }
                }
            }

            if (InputHandler.keyReleased(Keys.M))
            {
                buymode = !buymode;
                update_menuItem();
                update_labels();
            }
        }
    }
}
