using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class CharCre : Screen
    {
        TextBox txt_name;
        Select sel_class;
        Label lbl_err;

        public CharCre()
        {
            Label lbl_title = new Label("Character Creation");
            lbl_title.Color = Color.Blue;
            lbl_title.Position = new Vector2(50, 50);

            PictureBox pb_char = new PictureBox();
            pb_char.Image = Graphic.Content.Instance.TestImage;
            pb_char.Position = new Vector2(500, 75);
            pb_char.Size = new Vector2(200, 400);

            Label lbl_name = new Label("Name:");
            lbl_name.Color = Color.White;
            lbl_name.Position = new Vector2(60, 100);

            Label lbl_class = new Label("Class:");
            lbl_class.Color = Color.White;
            lbl_class.Position = new Vector2(60, 160);

            txt_name = new TextBox(10);
            txt_name.Position = new Vector2(130, 92);
            cm.add(txt_name);

            sel_class = new Select();
            sel_class.Position = new Vector2(115, 160);
            sel_class.add("Fighter");
            sel_class.add("Caster");
            sel_class.add("Healer");
            sel_class.add("Archer");
            sel_class.add("Scout");
            cm.add(sel_class);

            Link lnk_con = new Link("Continue");
            lnk_con.Position = new Vector2(90, 230);
            lnk_con.selected = cont;
            cm.add(lnk_con);

            lbl_err = new Label("");
            lbl_err.Position = new Vector2(90, 330);
            lbl_err.Color = Color.Red;
            cm.add(lbl_err);

            cm.add(lbl_name);
            cm.add(lbl_class);
            cm.add(pb_char);
            cm.add(lbl_title);
        }

        private void cont(object sender, EventArgs e)
        {
            if (txt_name.Text != "")
            {
                if (sel_class.SelectedValue == "Fighter")
                    GameState.CurrentState.mainChar = new Fighter(txt_name.Text);
                else
                    GameState.CurrentState.mainChar = new Fighter(txt_name.Text);

                StateManager.Instance.goForward(new WorldMap());
            }
            else
                lbl_err.Text = "Name cannot be empty";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();
        }
    }
}
