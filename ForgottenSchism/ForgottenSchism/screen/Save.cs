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
    public class Save: Screen
    {
        Menu m;
        Link ns;
        DialogYN dyn;
        DialogTxt dtxt;
        bool di;

        public Save(Game1 game): base(game)
        {
            di = false;

            cm.ArrowEnable = false;

            dtxt = new DialogTxt(game, "Save name:");
            dtxt.Position = new Vector2(200, 100);
            dtxt.complete = dtxtComplete;
            dtxt.Enabled = false;
            dtxt.Visible = false;
            cm.add(dtxt);
            cm.addLastDraw(dtxt);

            dyn = new DialogYN(game, "");
            dyn.Position = new Vector2(200, 100);
            dyn.chose = dynChose;
            dyn.Enabled = false;
            dyn.Visible = false;
            cm.add(dyn);
            cm.addLastDraw(dyn);

            Label lbl_title = new Label(game, "Save Game");
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

        private void save(object o, EventArgs e)
        {
            String path = ((Link)o).Text;

            save(path);
        }

        private void save(String path)
        {
            System.Console.Out.WriteLine("Overwriting  save: " + path);
        }

        private void newSave(object o, EventArgs e)
        {
            dtxt.Enabled = true;
            dtxt.Visible = true;
            m.Enabled = false;
            m.ArrowEnabled = false;
            di = true;
        }

        private void list()
        {
            ns=new Link(Game, "New save");
            ns.selected = newSave;
            m.add(ns);

            foreach (String str in Directory.EnumerateFiles(".\\save\\", "*.save"))
            {
                Link l = new Link(Game, str);
                l.selected = save;
                m.add(l);
            }
        }

        private void dtxtComplete(object o, EventArgs e)
        {
            di = false;
            dtxt.Enabled = false;
            dtxt.Visible = false;
            m.Enabled = true;
            m.ArrowEnabled = true;

            save(new String(((char[])((EventArgObject)e).o)));
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
            
            if(dtxt.Enabled)
                dtxt.HandleInput(gameTime);

            if (di)
                return;

            if (InputHandler.keyReleased(Keys.Escape))
                Game.stateMng.goBack();

            if (InputHandler.keyReleased(Keys.D)&&m.Focused!=ns)
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
