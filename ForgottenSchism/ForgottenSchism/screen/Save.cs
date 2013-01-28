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
        Label lbl_stat;

        public Save()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            di = false;

            dtxt = new DialogTxt(this);
            dtxt.complete = dtxtComplete;
            dtxt.InputEnabled = false;

            dyn = new DialogYN(this);
            dyn.complete = dynChose;
            dyn.InputEnabled = false;

            Label lbl_title = new Label("Save Game");
            lbl_title.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_title.Position = new Vector2(100, 20);
            MainWindow.add(lbl_title);

            lbl_stat = new Label("");
            lbl_stat.Position = new Vector2(100, 50);
            lbl_stat.Color = Color.Green;
            MainWindow.add(lbl_stat);

            Label lbl_d = new Label("D");
            lbl_d.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_d.Position = new Vector2(80, 500);
            MainWindow.add(lbl_d);

            Label lbl_del = new Label("Delete Save");
            
            lbl_del.Position = new Vector2(100, 500);
            MainWindow.add(lbl_del);

            m = new Menu(10);
            m.Position = new Vector2(10, 75);
            list();
            MainWindow.add(m);
        }

        private void del()
        {
            String path = m.Focused.Text;

            File.Delete(".\\save\\"+path+".save");

            lbl_stat.Text = "\""+path+"\" Deleted";

            list();
        }

        private void save(object o, EventArgs e)
        {
            String path = ((Link)o).Text;

            save(".\\save\\"+path+".save");
        }

        private void save(String path)
        {
            GameState.CurrentState.save(path);

            lbl_stat.Text = "Game Saved";

            list();
        }

        private void newSave(object o, EventArgs e)
        {
            dtxt.InputEnabled = true;
            dtxt.show("Save name:");
        }

        private void list()
        {
            m.clear();

            ns=new Link("New save");
            ns.selected = newSave;
            m.add(ns);

            Link l;
            String s;

            foreach (String str in Directory.EnumerateFiles(".\\save\\", "*.save"))
            {
                s = Path.GetFileNameWithoutExtension(str);
                l = new Link(s);
                l.selected = save;
                m.add(l);
            }
        }

        private void dtxtComplete(char[] txt)
        {
            save(".\\save\\" + new String(txt).Trim() + ".save");
            dtxt.InputEnabled = false;
        }

        private void dynChose(bool b)
        {
            if (b)
                del();
            dyn.InputEnabled = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (dyn.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    dyn.InputEnabled = false;
                    dyn.close();
                }
            }
            else if (dtxt.InputEnabled)
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    dtxt.InputEnabled = false;
                    dtxt.close();
                }
            }
            else
            {
                if (di)
                    return;

                if (InputHandler.keyReleased(Keys.Escape))
                    StateManager.Instance.goBack();

                if (InputHandler.keyReleased(Keys.D) && m.Focused != ns)
                {
                    if (m.Count > 0)
                    {
                        dyn.InputEnabled = true;
                        dyn.show("Delete saved game\n" + m.Focused.Text + " ?");
                    }
                }
            }
        }
    }
}
