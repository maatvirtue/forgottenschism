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
    public class MainMenu: Screen
    {
        private Label lbl_title;
        private SpriteFont font;
        private Link lnk_newGame;
        private Link lnk_loadGame;
        private Link lnk_option;
        private Link lnk_exit;

        public MainMenu()
        {
            lbl_title = new Label("Main menu");
            lbl_title.Position = new Vector2(200, 50);
            lbl_title.Color = Color.Blue;

            lnk_newGame = new Link("New Game");
            lnk_newGame.Position = new Vector2(150, 125);
            lnk_newGame.selected = newGame;

            lnk_loadGame = new Link("Load Game");
            lnk_loadGame.Position = new Vector2(150, 175);
            lnk_loadGame.selected = loadGame;

            Link lnk_saveGame = new Link("Save Game");
            lnk_saveGame.Position = new Vector2(150, 225);
            lnk_saveGame.selected = saveGame;

            lnk_option = new Link("Option");
            lnk_option.Position = new Vector2(150, 275);
            lnk_option.selected = options;

            lnk_exit = new Link("Exit");
            lnk_exit.Position = new Vector2(150, 325);
            lnk_exit.selected = exit;

            cm.add(lbl_title);
            cm.add(lnk_newGame);
            cm.add(lnk_loadGame);
            cm.add(lnk_saveGame);
            cm.add(lnk_option);
            cm.add(lnk_exit);
        }

        private void options(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new GameOver());
        }

        public void loadGame(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new Load());
        }

        public void saveGame(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new Save());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                exit(null, null);
        }

        private void newGame(object sender, EventArgs e)
        {
            StateManager.Instance.goForward(new CharCre());
        }

        private void exit(object sender, EventArgs e)
        {
            Game.Exit();
        }
    }
}
