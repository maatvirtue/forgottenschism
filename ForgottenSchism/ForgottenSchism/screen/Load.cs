using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;

namespace ForgottenSchism.screen
{
    public class Load: Screen
    {
        Menu m;
        DialogYN dyn;
        bool di;

        public Load(Game1 game): base(game)
        {
            di = false;

            cm.ArrowEnable = false;

            dyn = new DialogYN(game, "");
            dyn.Position = new Vector2(200, 100);
            dyn.chose = dynChose;
            dyn.Enabled = false;
            dyn.Visible = false;
            cm.add(dyn);
            cm.addLastDraw(dyn);

            Label lbl_title = new Label(game, "Load Game");
            lbl_title.Color = Color.Blue;
            lbl_title.Position = new Vector2(100, 20);
            cm.add(lbl_title);

            Label lbl_d = new Label(game, "D");
            lbl_d.Color = Color.Blue;
            lbl_d.Position = new Vector2(80, 500);
            cm.add(lbl_d);

            Label lbl_del = new Label(game, "Delete Save");
            lbl_del.Color = Color.White;
            lbl_del.Position = new Vector2(100, 500);
            cm.add(lbl_del);

            m = new Menu(game, 10);
            m.Position = new Vector2(10, 75);
            list();
            cm.add(m);
        }

        private void del()
        {
            String path = m.Focused.Text;

            System.Console.Out.WriteLine("Deleting " + m.Focused.Text );
        }

        private void load(object o, EventArgs e)
        {
            String path = ((Link)o).Text;

            System.Console.Out.WriteLine("Loading " + m.Focused.Text);
        }

        private void list()
        {
            foreach (String str in Directory.EnumerateFiles(".\\save\\", "*.save"))
            {
                Link l = new Link(Game, str);
                l.selected = load;
                m.add(l);
            }
        }

        private void dynChose(object o, EventArgs e)
        {
            di = false;
            dyn.Enabled = false;
            dyn.Visible = false;
            m.Enabled = true;
            m.ArrowEnabled = true;

            if ((bool)((EventArgObject)e).o)
                del();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(dyn.Enabled)
                dyn.HandleInput(gameTime);

            if (di)
                return;

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();

            if (InputHandler.keyReleased(Keys.D))
            {
                dyn.Text = "Delete saved game\n" + m.Focused.Text + " ?";
                dyn.Enabled = true;
                dyn.Visible = true;
                m.Enabled = false;
                m.ArrowEnabled = false;
                di = true;
            }
        }
    }
}
