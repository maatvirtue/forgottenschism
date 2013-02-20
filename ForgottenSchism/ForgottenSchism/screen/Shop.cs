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
    class Shop : Screen
    {
        /// <summary>
        /// Percent of the original cost that the shop will give when you sell an Item
        /// </summary>
        static readonly double sellRate = 0.60;

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
        Label lbl_qty;
        Select sel_qty;
        Label lbl_q;
        Label lbl_qAction;
        Label lbl_esc;
        Label lbl_escAction;

        /// <summary>
        /// wether the user is selecting quantity (true) or not (which means he is on the item menu) (false)
        /// </summary>
        bool qtymode;

        /// <summary>
        /// wether in buy (true) mode or sell (false) mode
        /// </summary>
        bool buymode;

        public Shop()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            buymode = true;
            qtymode = false;

            lbl_title = new Label("Shop");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_title.Position = new Vector2(250, 100);
            MainWindow.add(lbl_title);

            lbl_item = new Label("Items");
            lbl_item.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_item.Position = new Vector2(90, 130);
            MainWindow.add(lbl_item);

            menu_item = new Menu(8);
            menu_item.Position = new Vector2(70, 150);
            MainWindow.add(menu_item);

            lbl_qty = new Label("Quantity");
            lbl_qty.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_qty.Position = new Vector2(260, 130);
            MainWindow.add(lbl_qty);

            sel_qty = new Select();
            sel_qty.Position = new Vector2(255, 160);
            for (int i = 1; i < 201; i++)
                sel_qty.add(i.ToString());
            sel_qty.TabStop = false;
            MainWindow.add(sel_qty);

            lbl_price = new Label("Price");
            lbl_price.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_price.Position = new Vector2(350, 130);
            MainWindow.add(lbl_price);

            lbl_cprice = new Label("");
            lbl_cprice.Position = new Vector2(350, 160);
            MainWindow.add(lbl_cprice);

            lbl_money = new Label("Money");
            lbl_money.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_money.Position = new Vector2(50, 50);
            MainWindow.add(lbl_money);

            lbl_cmoney = new Label(GameState.CurrentState.mainArmy.Money.ToString());
            lbl_cmoney.Position = new Vector2(150, 50);
            MainWindow.add(lbl_cmoney);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_v.Position = new Vector2(50, 440);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Item");
            lbl_vAction.Position = new Vector2(80, 440);
            MainWindow.add(lbl_vAction);

            lbl_m = new Label("M");
            lbl_m.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_m.Position = new Vector2(50, 470);
            MainWindow.add(lbl_m);

            lbl_mAction = new Label("Sell Mode");
            lbl_mAction.Position = new Vector2(80, 470);
            MainWindow.add(lbl_mAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_esc.Position = new Vector2(50, 500);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_escAction.Position = new Vector2(100, 500);
            MainWindow.add(lbl_escAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_enter.Position = new Vector2(50, 410);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Buy Item");
            lbl_enterAction.Position = new Vector2(120, 410);
            MainWindow.add(lbl_enterAction);

            lbl_q = new Label("Q");
            lbl_q.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_q.Position = new Vector2(260, 410);
            MainWindow.add(lbl_q);

            lbl_qAction = new Label("Choose quantity");
            lbl_qAction.Position = new Vector2(290, 410);
            MainWindow.add(lbl_qAction);

            update_menuItem();

            if (lbl_v.Visible)
                update_price_label();
        }

        /// <summary>
        /// updates the key labels depending on the mode
        /// </summary>
        private void update_klabels_mode()
        {
            if (!qtymode)
            {
                lbl_m.Visible = true;
                lbl_mAction.Visible = true;
                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
                
                if (buymode)
                {
                    lbl_mAction.Text = "Sell Mode";
                    lbl_enterAction.Text = "Buy Item";

                    lbl_q.Visible = true;
                    lbl_qAction.Visible = true;
                }
                else
                {
                    lbl_mAction.Text = "Buy Mode";
                    lbl_enterAction.Text = "Sell Item";

                    lbl_q.Visible = false;
                    lbl_qAction.Visible = false;
                }
            }
            else
            {
                lbl_m.Visible = false;
                lbl_mAction.Visible = false;
                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
                lbl_q.Visible = false;
                lbl_qAction.Visible = false;

                lbl_enterAction.Text = "Select Quantity";
            }
        }

        /// <summary>
        /// Updates the money label
        /// </summary>
        private void update_money()
        {
            lbl_cmoney.Text = GameState.CurrentState.mainArmy.Money.ToString();
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

        /// <summary>
        /// Updates lbl_cprice
        /// </summary>
        private void update_price_label()
        {
            int cost;
            int qty;

            if (buymode)
                cost = Content.Instance.shop.Items[menu_item.Selected].Cost;
            else
                cost = (int)((double)GameState.CurrentState.mainArmy.Inventory.Items[menu_item.Selected].Cost * sellRate);

            if (sel_qty.Visible)
                qty = int.Parse(sel_qty.SelectedValue);
            else
                qty = 1;

            lbl_cprice.Text = (cost * qty).ToString();

            if ((cost * qty) > GameState.CurrentState.mainArmy.Money)
                lbl_cprice.Color = Color.Red;
            else
                lbl_cprice.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
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
                    i = Content.Instance.shop.Items[menu_item.Selected];
                else
                    i = GameState.CurrentState.mainArmy.Inventory.Items[menu_item.Selected];

                StateManager.Instance.goForward(new ItemManage(i));
            }

            if(InputHandler.keyReleased(Keys.Q)&&lbl_q.Visible)
            {
                qtymode = true;

                menu_item.TabStop = false;

                sel_qty.TabStop = true;
                sel_qty.HasFocus = true;

                update_klabels_mode();
            }

            if (!qtymode)
            {
                if (InputHandler.keyReleased(Keys.Enter) && lbl_v.Visible) //if lbl_v.Visible is true then there is an item that is selected
                {
                    if (buymode)
                    {
                        Item i = Content.Instance.shop.Items[menu_item.Selected];
                        int qty=int.Parse(sel_qty.SelectedValue);
                        int cost = i.Cost * qty;

                        if (GameState.CurrentState.mainArmy.Money >= cost)
                        {
                            GameState.CurrentState.mainArmy.Money -= cost;

                            for(int e=0; e<qty; e++)
                                GameState.CurrentState.mainArmy.Inventory.Items.Add(i.clone());

                            update_money();

                            GameState.CurrentState.saved = false;
                        }
                    }
                    else
                    {
                        Item i = GameState.CurrentState.mainArmy.Inventory.Items[menu_item.Selected];

                        GameState.CurrentState.mainArmy.Inventory.Items.Remove(i);

                        GameState.CurrentState.mainArmy.Money += (int)((double)i.Cost * sellRate); //give back less cash than it costed (prevents infinite buying)

                        update_money();
                        update_menuItem();

                        GameState.CurrentState.saved = false;
                    }
                }
            }
            else
            {
                if (InputHandler.keyReleased(Keys.Enter))
                {
                    qtymode = false;

                    menu_item.TabStop = true;

                    sel_qty.TabStop = false;
                    sel_qty.HasFocus = false;

                    update_klabels_mode();
                }
            }

            if (InputHandler.arrowReleased()&&(qtymode||lbl_v.Visible)) //the user is either changing the item selection or the quantity. if there is an item in the item list (lbl_v.Visible) update its price
            {
                update_price_label();
            }

            if (InputHandler.keyReleased(Keys.M) && lbl_m.Visible)
            {
                buymode = !buymode;

                if (buymode)
                {
                    lbl_qty.Visible = true;
                    sel_qty.Visible = true;
                }
                else
                {
                    lbl_qty.Visible = false;
                    sel_qty.Visible = false;
                }

                update_menuItem();
                update_klabels_mode();
            }
        }
    }
}
