using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    public class Test: Screen
    {
        Label lbl_test;
        Link lnk_test;
        TextBox txt_test;

        DialogYN dyn;

        public Test()
        {
            lbl_test = new Label("Test");
            lbl_test.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_test.Position = new Vector2(50, 50);
            MainWindow.add(lbl_test);

            txt_test = new TextBox(10);
            txt_test.Position = new Vector2(50, 100);
            MainWindow.add(txt_test);

            lnk_test = new Link("Go!");
            lnk_test.Position = new Vector2(50, 150);
            lnk_test.selected += sel;
            MainWindow.add(lnk_test);

            dyn = new DialogYN(this);
            dyn.complete = con;
        }

        private void sel(object o, EventArgs e)
        {
            dyn.show("Es-ce que?");
        }

        private void con(bool b)
        {
            String str;

            if (b)
                str = "yes!!!";
            else
                str = "nooooooo";

            lbl_test.Text = str;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();
        }
    }
}
