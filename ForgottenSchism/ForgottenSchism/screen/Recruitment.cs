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
        Label lbl_hireSoldiers;

        Label lbl_noHire;

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

        public Recruitment()
        {
            lbl_hireSoldiers = new Label("Hire Soldiers");
            lbl_hireSoldiers.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.TITLE;
            lbl_hireSoldiers.Position = new Vector2(50, 30);
            MainWindow.add(lbl_hireSoldiers);

            lbl_noHire = new Label("Sorry, there are currently no soldiers available to recruit.");
            lbl_noHire.Color = Color.Gray;
            lbl_noHire.Position = new Vector2(150, 150);
            lbl_noHire.Visible = false;
            MainWindow.add(lbl_noHire);

            lbl_name = new Label("Name");
            lbl_name.Color = Color.DarkRed;
            lbl_name.Position = new Vector2(100, 70);
            MainWindow.add(lbl_name);

            lbl_level = new Label("Level");
            lbl_level.Color = Color.DarkRed;
            lbl_level.Position = new Vector2(250, 70);
            MainWindow.add(lbl_level);

            lbl_class = new Label("Class");
            lbl_class.Color = Color.DarkRed;
            lbl_class.Position = new Vector2(400, 70);
            MainWindow.add(lbl_class);

            lbl_price = new Label("Hiring Price");
            lbl_price.Color = Color.DarkRed;
            lbl_price.Position = new Vector2(550, 70);
            MainWindow.add(lbl_price);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(50, 370);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Character");
            
            lbl_vAction.Position = new Vector2(80, 370);
            MainWindow.add(lbl_vAction);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(50, 400);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Hire Character");
            
            lbl_enterAction.Position = new Vector2(130, 400);
            MainWindow.add(lbl_enterAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_esc.Position = new Vector2(50, 430);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            
            lbl_escAction.Position = new Vector2(110, 430);
            MainWindow.add(lbl_escAction);

            menu_name = new Menu(12);
            menu_name.Position = new Vector2(100, 100);
            menu_name.selectionChanged = update_all_menus;
            MainWindow.add(menu_name);

            menu_level = new Menu(12);
            menu_level.Position = new Vector2(250, 100);
            menu_level.TabStop = false;
            menu_level.Enabled = false;
            MainWindow.add(menu_level);

            menu_class = new Menu(12);
            menu_class.Position = new Vector2(400, 100);
            menu_class.TabStop = false;
            menu_class.Enabled = false;
            MainWindow.add(menu_class);

            menu_price = new Menu(12);
            menu_price.Position = new Vector2(550, 100);
            menu_price.TabStop = false;
            menu_price.Enabled = false;
            MainWindow.add(menu_price);

            LoadMenus();
        }

        private void LoadMenus()
        {
            //menu_name.add(new Link("Herp"));
            //menu_level.add(new Link("5"));
            //menu_class.add(new Link("Healer"));
            //menu_price.add(new Link("1000"));

            //menu_name.add(new Link("Derp"));
            //menu_level.add(new Link("2"));
            //menu_class.add(new Link("Fighter"));
            //menu_price.add(new Link("9001"));

            //menu_name.add(new Link("Missingno"));
            //menu_level.add(new Link("-3"));
            //menu_class.add(new Link("Pedro"));
            //menu_price.add(new Link("f0rg0773n 5ch15m"));

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

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
        }
    }
}
