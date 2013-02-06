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
    class PauseMenu: Screen
    {
        Label lbl_title;
        SpriteFont font;
        Link lnk_resumeGame;
        Link lnk_loadGame;
        Link lnk_saveGame;
        Link lnk_option;
        Link lnk_exit;
        DialogYN yn_exit;

        public PauseMenu()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_menuless;

            yn_exit = new DialogYN(this);
            yn_exit.complete = dialog_ret;
            yn_exit.InputEnabled = false;

            lbl_title = new Label("Pause menu");
            lbl_title.Position = new Vector2(50, 50);
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;

            lnk_resumeGame = new Link("Resume Game");
            lnk_resumeGame.Position = new Vector2(150, 125);
            lnk_resumeGame.selected = resumeGame;
            MainWindow.add(lnk_resumeGame);

            lnk_loadGame = new Link("Load Game");
            lnk_loadGame.Position = new Vector2(150, 175);
            lnk_loadGame.selected = loadGame;

            lnk_saveGame = new Link("Save Game");
            lnk_saveGame.Position = new Vector2(150, 225);
            lnk_saveGame.selected = saveGame;

            lnk_option = new Link("Option");
            lnk_option.Position = new Vector2(150, 275);
            lnk_option.selected = options;

            lnk_exit = new Link("Exit");
            lnk_exit.Position = new Vector2(150, 325);
            lnk_exit.selected = exit;

            MainWindow.add(lbl_title);
            MainWindow.add(lnk_loadGame);
            MainWindow.add(lnk_saveGame);
            MainWindow.add(lnk_option);
            MainWindow.add(lnk_exit);
        }

        private void resumeGame(object o, EventArgs e)
        {
            StateManager.Instance.reset(new WorldMap());
        }

        private void exit(object o, EventArgs e)
        {
            if (GameState.CurrentState.saved)
                Game.Exit();
            else
            {
                yn_exit.InputEnabled = true;
                yn_exit.show("Exit whitout saving?");
            }
        }

        private void options(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new Options());
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

            if (yn_exit.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    yn_exit.InputEnabled = false;
                    yn_exit.close();
                }
            }
            else
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    if (GameState.CurrentState.saved)
                        Game.Exit();
                    else
                    {
                        yn_exit.InputEnabled = true;
                        yn_exit.show("Exit whitout saving?");
                    }
                }
            }
        }

        private void dialog_ret(bool b)
        {
            if (b)
                Game.Exit();
            yn_exit.InputEnabled = false;
        }
    }
}
