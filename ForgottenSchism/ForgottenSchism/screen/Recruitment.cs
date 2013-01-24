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
    class Recruitment : Screen
    {
        private static readonly TimeSpan labelShowDuration = TimeSpan.FromMilliseconds(2000);
        private TimeSpan lastTimeAction;

        Army army;

        Label lbl_hireSoldiers;

        Label lbl_money;
        Label lbl_moneyAmount;

        Label lbl_noHire;
        Label lbl_noMoney;

        Label lbl_name;
        Label lbl_level;
        Label lbl_class;
        Label lbl_price;

        Menu menu_name;
        Menu menu_level;
        Menu menu_class;
        Menu menu_price;

        Label lbl_v;
        Label lbl_vAction;

        Label lbl_enter;
        Label lbl_enterAction;

        Label lbl_esc;
        Label lbl_escAction;

        List<Character> charLs;

        public Recruitment()
        {
            army = GameState.CurrentState.mainArmy;

            lbl_hireSoldiers = new Label("Hire Soldiers");
            lbl_hireSoldiers.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_hireSoldiers.Position = new Vector2(50, 30);
            MainWindow.add(lbl_hireSoldiers);

            lbl_money = new Label("Money:");
            lbl_money.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_money.Position = new Vector2(50, 60);
            MainWindow.add(lbl_money);

            lbl_moneyAmount = new Label(army.Money.ToString());
            lbl_moneyAmount.Color = Color.White;
            lbl_moneyAmount.Position = new Vector2(120, 60);
            MainWindow.add(lbl_moneyAmount);

            lbl_noHire = new Label("Sorry, there are currently no soldiers available to recruit.");
            lbl_noHire.Color = Color.Gray;
            lbl_noHire.Position = new Vector2(150, 170);
            lbl_noHire.Visible = false;
            MainWindow.add(lbl_noHire);

            lbl_noMoney = new Label("Not enough money to hire this soldier.");
            lbl_noMoney.Color = Color.Red;
            lbl_noMoney.Position = new Vector2(100, 120);
            lbl_noMoney.Visible = false;
            MainWindow.add(lbl_noMoney);

            lbl_name = new Label("Name");
            lbl_name.Color = Color.DarkRed;
            lbl_name.Position = new Vector2(100, 90);
            MainWindow.add(lbl_name);

            lbl_level = new Label("Level");
            lbl_level.Color = Color.DarkRed;
            lbl_level.Position = new Vector2(250, 90);
            MainWindow.add(lbl_level);

            lbl_class = new Label("Class");
            lbl_class.Color = Color.DarkRed;
            lbl_class.Position = new Vector2(400, 90);
            MainWindow.add(lbl_class);

            lbl_price = new Label("Hiring Price");
            lbl_price.Color = Color.DarkRed;
            lbl_price.Position = new Vector2(550, 90);
            MainWindow.add(lbl_price);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(50, 430);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Color = Color.White;
            lbl_vAction.Position = new Vector2(80, 430);
            MainWindow.add(lbl_vAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(50, 460);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Hire Character");
            lbl_enterAction.Color = Color.White;
            lbl_enterAction.Position = new Vector2(130, 460);
            MainWindow.add(lbl_enterAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_esc.Position = new Vector2(50, 490);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.Color = Color.White;
            lbl_escAction.Position = new Vector2(110, 490);
            MainWindow.add(lbl_escAction);

            menu_name = new Menu(10);
            menu_name.Position = new Vector2(100, 120);
            menu_name.selectionChanged = update_all_menus;
            MainWindow.add(menu_name);

            menu_level = new Menu(10);
            menu_level.Position = new Vector2(250, 120);
            menu_level.TabStop = false;
            menu_level.Enabled = false;
            MainWindow.add(menu_level);

            menu_class = new Menu(10);
            menu_class.Position = new Vector2(400, 120);
            menu_class.TabStop = false;
            menu_class.Enabled = false;
            MainWindow.add(menu_class);

            menu_price = new Menu(10);
            menu_price.Position = new Vector2(550, 120);
            menu_price.TabStop = false;
            menu_price.Enabled = false;
            MainWindow.add(menu_price);

            charLs = new List<Character>();

            LoadMenus();
        }

        private void LoadMenus()
        {
            lbl_moneyAmount.Text = army.Money.ToString();

            menu_name.clear();
            menu_level.clear();
            menu_class.clear();
            menu_price.clear();

            charLs.Clear();

            menu_name.add(new Link("Herp"));
            menu_level.add(new Link("5"));
            menu_class.add(new Link("Healer"));
            menu_price.add(new Link("1000"));

            Character c = new Healer("Herp");
            c.levelUp();
            c.levelUp();
            c.levelUp();
            c.levelUp();

            charLs.Add(c);

            menu_name.add(new Link("Derp"));
            menu_level.add(new Link("2"));
            menu_class.add(new Link("Fighter"));
            menu_price.add(new Link("9001"));

            Character c2 = new Fighter("Derp");
            c2.levelUp();

            charLs.Add(c2);

            menu_name.add(new Link("Pedro"));
            menu_level.add(new Link("3"));
            menu_class.add(new Link("Caster"));
            menu_price.add(new Link("100"));

            Character c3 = new Caster("Pedro");
            c3.levelUp();
            c3.levelUp();

            charLs.Add(c3);

            if(menu_name.Count == 0)
            {
                lbl_noHire.Visible = true;
            }
        }

        private void update_all_menus(object sender, EventArgs e)
        {
            menu_level.Selected = menu_name.Selected;
            menu_class.Selected = menu_name.Selected;
            menu_price.Selected = menu_name.Selected;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (lastTimeAction == new TimeSpan(0))
            {
                lastTimeAction = gameTime.TotalGameTime;
            }
            if (lastTimeAction + labelShowDuration < gameTime.TotalGameTime)
            {
                lbl_noMoney.Visible = false;
            }

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }

            if (InputHandler.keyReleased(Keys.V))
            {
                if (menu_name.Selected >= 0)
                {
                    StateManager.Instance.goForward(new CharManage(charLs[menu_name.Selected], null));
                }
            }

            if (InputHandler.keyReleased(Keys.Enter))
            {
                if (menu_name.Selected >= 0)
                {
                    if (army.Money >= int.Parse(menu_price.SelectedText))
                    {
                        Character ch = charLs[menu_name.Selected];
                        army.Standby.Add(ch);
                        army.Money -= int.Parse(menu_price.SelectedText);

                        LoadMenus();
                    }
                    else
                    {
                        lbl_noMoney.Visible = true;
                        lastTimeAction = gameTime.TotalGameTime;
                    }
                }
            }
        }
    }
}
