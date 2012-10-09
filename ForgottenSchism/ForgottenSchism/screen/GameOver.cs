using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.control;

namespace ForgottenSchism.screen
{
    public class GameOver: Screen
    {
        public GameOver(Game1 game): base(game)
        {
            Label lbl_gameOver = new Label(game, "Game Over");
            lbl_gameOver.Color = Color.Blue;
            lbl_gameOver.Position = new Vector2(300, 100);
            cm.add(lbl_gameOver);

            Link lnk_c = new Link(game, "Contrinue to main screen");
            lnk_c.Position = new Vector2(200, 300);
            lnk_c.selected = toMainmenu;
            cm.add(lnk_c);
        }

        private void toMainmenu(object o, EventArgs e)
        {
            Game.stateMng.reset(Game.mainMenu);
        }
    }
}
