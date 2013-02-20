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
        Label lbl_name;
        TextBox txt_name;
        Label lbl_class;
        Select sel_class;
        Label lbl_err;
        PictureBox pb_char;

        public CharCre()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_menuless;

            Label lbl_title = new Label("Character Creation");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_title.Position = new Vector2(50, 50);
            MainWindow.add(lbl_title);

            pb_char = new PictureBox();
            pb_char.Image = Content.Graphics.Instance.Images.characters.fighter;
            pb_char.Position = new Vector2(350, 80);
            pb_char.Size = new Vector2(384, 384);
            MainWindow.add(pb_char);

            lbl_name = new Label("Name:");
            lbl_name.Position = new Vector2(110, 180);
            MainWindow.add(lbl_name);

            lbl_class = new Label("Class:");
            lbl_class.Position = new Vector2(120, 240);
            MainWindow.add(lbl_class);

            txt_name = new TextBox(10);
            txt_name.Position = new Vector2(180, 172);
            MainWindow.add(txt_name);

            sel_class = new Select();
            sel_class.Position = new Vector2(175, 240);
            sel_class.add("Fighter");
            sel_class.add("Caster");
            sel_class.add("Healer");
            sel_class.add("Archer");
            sel_class.add("Scout");
            sel_class.selectionChanged = selch;
            MainWindow.add(sel_class);

            Link lnk_con = new Link("Continue");
            lnk_con.Position = new Vector2(150, 300);
            lnk_con.selected = cont;
            MainWindow.add(lnk_con);

            lbl_err = new Label("Name cannot be empty");
            lbl_err.Position = new Vector2(100, 360);
            lbl_err.Color = Color.Red;
            lbl_err.Visible = false;
            MainWindow.add(lbl_err);
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

        /// <summary>
        /// Generates Main Army
        /// </summary>
        private void genArmy()
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

            GameState.CurrentState.mainChar.toLvl(5);

            Army a = new Army();

            Unit u = new Unit(GameState.CurrentState.mainChar);

            Fighter f1 = new Fighter("Patrick");
            f1.toLvl(3);

            Archer a1 = new Archer("Violaine");
            a1.toLvl(4);

            Character.Stats.Traits gat=new Character.Stats.Traits();
            gat.norm();
            gat.dex=10;

            Item ga=new Item("Art Bow", Item.Item_Type.WEAPON, 20, gat);
            a1.equipWeapon(ga);

            Caster cc = new Caster("Brendan");
            cc.levelUp();

            Healer h1 = new Healer("Sophie");

            Scout s1 = new Scout("Steven");

            u.set(3, 2, f1);
            u.set(3, 3, a1);
            u.set(0, 3, cc);
            u.set(1, 1, h1);
            a.Standby.Add(s1);

            a.Units.Add(u);

            a.setOrgAll("main");

            Item i = new Item("Heal Potion", Item.Item_Type.CONSUMABLE, 100, new Character.Stats.Traits(), new Item.OtherEffect(10, 0));

            a.Inventory.Items.Add(i);

            GameState.CurrentState.mainArmy = a;
        }

        private void cont(object sender, EventArgs e)
        {
            if (txt_name.Text != "")
            {
                genArmy();

                GameState.CurrentState.gen = Content.Instance.gen.Fog;

                GameState.CurrentState.mainCharPos = new Point(Content.Instance.gen.StartingPosition.X, Content.Instance.gen.StartingPosition.Y);

                StateManager.Instance.reset(new WorldMap());
            }
            else
                lbl_err.visibleTemp(2000);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goBack();
        }
    }
}
