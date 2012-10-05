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

        public MainMenu(Game1 game): base(game)
        {
            lbl_title = new Label(Game, "Main menu");
            lbl_title.Position = new Vector2(200, 50);
            lbl_title.Color = Color.Blue;

            lnk_newGame = new Link(Game, "New Game");
            lnk_newGame.Position = new Vector2(150, 125);
            lnk_newGame.selected = newGame;

            lnk_loadGame = new Link(Game, "Load Game");
            lnk_loadGame.Position = new Vector2(150, 175);
            lnk_loadGame.selected = loadGame;

            Link lnk_saveGame = new Link(Game, "Save Game");
            lnk_saveGame.Position = new Vector2(150, 225);
            lnk_saveGame.selected = saveGame;

            lnk_option = new Link(Game, "Option");
            lnk_option.Position = new Vector2(150, 275);
            lnk_option.selected = sm;

            lnk_exit = new Link(Game, "Exit");
            lnk_exit.Position = new Vector2(150, 325);
            lnk_exit.selected = exit;

            cm.add(lbl_title);
            cm.add(lnk_newGame);
            cm.add(lnk_loadGame);
            cm.add(lnk_saveGame);
            cm.add(lnk_option);
            cm.add(lnk_exit);
        }

        public void loadGame(object o, EventArgs e)
        {
            Game.stateMng.goForward(Game.load);
        }

        public void saveGame(object o, EventArgs e)
        {
            Game.stateMng.goForward(Game.save);
        }

        public void sm(object o, EventArgs e)
        {
            Tilemap tm = new Tilemap();
            tm.save("gen.map");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                exit(null, null);
        }

        private void newGame(object sender, EventArgs e)
        {
            Game.stateMng.goForward(Game.charCre);
        }

        private void exit(object sender, EventArgs e)
        {
            Game.Exit();
        }
    }
}
