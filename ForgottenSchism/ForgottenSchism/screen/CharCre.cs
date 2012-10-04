using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.control;
using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    public class CharCre : Screen
    {
        TextBox txt_name;
        Select sel_class;

        public CharCre(Game1 game): base(game)
        {
            Label lbl_title = new Label(game, "Character Creation");
            lbl_title.Color = Color.Blue;
            lbl_title.Position = new Vector2(50, 50);

            PictureBox pb_char = new PictureBox(game, "img\\test");
            pb_char.Position = new Vector2(500, 75);
            pb_char.Size = new Vector2(200, 400);

            Label lbl_name = new Label(game, "Name:");
            lbl_name.Color = Color.White;
            lbl_name.Position = new Vector2(60, 100);

            Label lbl_class = new Label(game, "Class:");
            lbl_class.Color = Color.White;
            lbl_class.Position = new Vector2(60, 160);

            txt_name = new TextBox(game, 10);
            txt_name.Position = new Vector2(130, 92);
            cm.add(txt_name);

            sel_class = new Select(game);
            sel_class.Position = new Vector2(115, 160);
            sel_class.add("Fighter");
            sel_class.add("Caster");
            sel_class.add("Healer");
            sel_class.add("Archer");
            sel_class.add("Scout");
            cm.add(sel_class);

            Link lnk_con = new Link(game, "Continue");
            lnk_con.Position = new Vector2(90, 230);
            lnk_con.selected = cont;
            cm.add(lnk_con);

            cm.add(lbl_name);
            cm.add(lbl_class);
            cm.add(pb_char);
            cm.add(lbl_title);
        }

        private void cont(object sender, EventArgs e)
        {
            Game.stateMng.goForward(Game.worldMap);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();
        }
    }
}
