using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class MainMenu: Screen
    {
        Label lbl_title;
        SpriteFont font;
        Link lnk_newGame;
        Link lnk_loadGame;
        Link lnk_option;
        Link lnk_exit;

        public MainMenu()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_titleMenu;

            lbl_title = new Label("Main menu");
            lbl_title.center(350);
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;

            lnk_newGame = new Link("New Game");
            lnk_newGame.center(405);
            lnk_newGame.selected = newGame;

            lnk_loadGame = new Link("Load Game");
            lnk_loadGame.center(440);
            lnk_loadGame.selected = loadGame;

            lnk_option = new Link("Option");
            lnk_option.center(475);
            lnk_option.selected = options;

            lnk_exit = new Link("Exit");
            lnk_exit.center(510);
            lnk_exit.selected = exit;

            MainWindow.add(lbl_title);
            MainWindow.add(lnk_newGame);
            MainWindow.add(lnk_loadGame);
            MainWindow.add(lnk_option);
            MainWindow.add(lnk_exit);
        }

        public override void start()
        {
            base.start();

            //MediaPlayer.Play(Content.Instance.audio.songs.test);
            MediaPlayer.IsRepeating = true;
        }

        private void options(object o, EventArgs e)
        {
            //StateManager.Instance.goForward(new Test());
        }

        public void loadGame(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new Load());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.Exit();
        }

        private void exit(object sender, EventArgs e)
        {
            Game.Exit();
        }

        private void newGame(object sender, EventArgs e)
        {
            StateManager.Instance.goForward(new CharCre());
        }
    }
}
