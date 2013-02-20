using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.control;
using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    public class GameOver: Screen
    {
        public GameOver()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_menuless;

            Label lbl_gameOver = new Label("Game Over");
            lbl_gameOver.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_gameOver.center(100);
            MainWindow.add(lbl_gameOver);

            Link lnk_c = new Link("Contrinue to main screen");
            lnk_c.center(300);
            lnk_c.selected = toMainmenu;
            MainWindow.add(lnk_c);

            Link lnk_q = new Link("Quit Game");
            lnk_q.center(330);
            lnk_q.selected = exitGame;
            MainWindow.add(lnk_q);
        }

        private void toMainmenu(object o, EventArgs e)
        {
            StateManager.Instance.reset(new MainMenu());
        }

        private void exitGame(object o, EventArgs e)
        {
            Game.Exit();
        }
    }
}
