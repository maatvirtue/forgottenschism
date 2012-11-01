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
        PictureBox pb_char;

        public CharCre()
        {
            Label lbl_title = new Label("Character Creation");
            lbl_title.Color = Color.Blue;
            lbl_title.Position = new Vector2(50, 50);

            pb_char = new PictureBox();
            pb_char.Image = Content.Graphics.Instance.Images.characters.fighter;
            pb_char.Position = new Vector2(300, 75);
            pb_char.Size = new Vector2(384, 384);

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
            sel_class.selectionChanged = selch;
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

        private void selch(object o, EventArgs e)
        {
            if (sel_class.SelectedValue == "Fighter")
                pb_char.Image = Content.Graphics.Instance.Images.characters.fighter;
            else if (sel_class.SelectedValue == "Archer")
                pb_char.Image = Content.Graphics.Instance.Images.characters.archer;
            else if (sel_class.SelectedValue == "Healer")
                pb_char.Image = Content.Graphics.Instance.Images.characters.healer;
            else if (sel_class.SelectedValue == "Caster")
                pb_char.Image = Content.Graphics.Instance.Images.characters.caster;
            else if (sel_class.SelectedValue == "Scout")
                pb_char.Image = Content.Graphics.Instance.Images.characters.scout;
            else
                pb_char.Image = Content.Graphics.Instance.Images.characters.fighter;
        }

        private void cont(object sender, EventArgs e)
        {
            if (txt_name.Text != "")
            {
                if (sel_class.SelectedValue == "Fighter")
                    GameState.CurrentState.mainChar = new Fighter(txt_name.Text);
                else if (sel_class.SelectedValue == "Archer")
                    GameState.CurrentState.mainChar = new Archer(txt_name.Text);
                else if (sel_class.SelectedValue == "Healer")
                    GameState.CurrentState.mainChar = new Healer(txt_name.Text);
                else if (sel_class.SelectedValue == "Caster")
                    GameState.CurrentState.mainChar = new Caster(txt_name.Text);
                else if (sel_class.SelectedValue == "Scout")
                    GameState.CurrentState.mainChar = new Scout(txt_name.Text);
                else
                    GameState.CurrentState.mainChar = new Fighter(txt_name.Text);

                Army a=new Army();

                Unit u = new Unit(GameState.CurrentState.mainChar);
                u.set(3, 2, new Fighter("Guard1"));
                u.set(3, 3, new Archer("Guard2"));
                u.set(0, 3, new Caster("Guard3"));
                u.set(1, 1, new Healer("Guard4"));
                a.Standby.Add(new Scout("Guard5"));

                a.Units.Add(u);

                GameState.CurrentState.mainArmy = a;

                GameState.CurrentState.gen = Content.Instance.gen.Fog;

                GameState.CurrentState.mainCharPos = new Point(Content.Instance.gen.StartingPosition.X, Content.Instance.gen.StartingPosition.Y);

                StateManager.Instance.reset(new WorldMap());
            }
            else
                lbl_err.Text = "Name cannot be empty";
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            /*if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();*/
        }
    }
}
