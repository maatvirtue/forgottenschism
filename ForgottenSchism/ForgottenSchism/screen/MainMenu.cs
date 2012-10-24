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
        Label lbl_title;
        SpriteFont font;
        Link lnk_newGame;
        Link lnk_loadGame;
        Link lnk_option;
        Link lnk_exit;
        bool di;
        DialogYN yn_exit;

        public MainMenu()
        {
            di = false;
            yn_exit = new DialogYN("Exit whitout saving?");
            yn_exit.Position = new Vector2(150, 100);
            yn_exit.Enabled = false;
            yn_exit.Visible = false;
            yn_exit.chose = dialog_ret;
            cm.addLastDraw(yn_exit);

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
            lnk_exit.selected = dialog_show;

            cm.add(yn_exit);
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

            if (yn_exit.Enabled)
                yn_exit.HandleInput(gameTime);

            if (di)
                return;

            if (InputHandler.keyReleased(Keys.Escape))
                dialog_show(null, null);
        }

        private void newGame(object sender, EventArgs e)
        {
            StateManager.Instance.goForward(new CharCre());
        }

        private void dialog_show(object sender, EventArgs e)
        {
            InputHandler.flush();
            di = true;
            yn_exit.Visible = true;
            yn_exit.Enabled = true;
            cm.ArrowEnable = false;
        }

        private void dialog_ret(object sender, EventArgs e)
        {
            if ((bool)((EventArgObject)e).o)
                Game.Exit();
            else
            {
                InputHandler.flush();
                di = false;
                yn_exit.Visible = false;
                yn_exit.Enabled = false;
                cm.ArrowEnable = true;
            }
        }
    }
}
