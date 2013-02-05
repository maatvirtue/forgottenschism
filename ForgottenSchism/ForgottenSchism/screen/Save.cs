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
        bool status;
        Label lbl_stat;

        Label lbl_d;
        Label lbl_del;

        Label lbl_enter;
        Label lbl_enterAction;

        Label lbl_esc;
        Label lbl_escAction;

        public Save()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            di = false;
            status = false;

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
            lbl_stat.Visible = false;
            MainWindow.add(lbl_stat);

            lbl_d = new Label("D");
            lbl_d.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_d.Position = new Vector2(80, 440);
            MainWindow.add(lbl_d);

            lbl_del = new Label("Delete Save");
            lbl_del.Position = new Vector2(100, 440);
            MainWindow.add(lbl_del);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(80, 470);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Save Game");
            lbl_enterAction.Position = new Vector2(150, 470);
            MainWindow.add(lbl_enterAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_esc.Position = new Vector2(80, 500);
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Go Back");
            lbl_escAction.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_escAction.Position = new Vector2(130, 500);
            MainWindow.add(lbl_escAction);

            m = new Menu(10);
            m.Position = new Vector2(10, 75);
            m.selectionChanged = selChange;
            list();
            MainWindow.add(m);
        }

        private void selChange(object o, EventArgs e)
        {
            if (m.Selected == 0)
            {
                lbl_d.Visible = false;
                lbl_del.Visible = false;
            }
            else
            {
                lbl_d.Visible = true;
                lbl_del.Visible = true;
            }
        }

        private void del()
        {
            String path = m.Focused.Text;

            File.Delete(".\\save\\"+path+".save");

            lbl_stat.Text = "\""+path+"\" Deleted";
            status = true;

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
            status = true;

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
            if (m.Count <= 1 || m.Selected == 0)
            {
                lbl_d.Visible = false;
                lbl_del.Visible = false;
            }
            else
            {
                lbl_d.Visible = true;
                lbl_del.Visible = true;
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

            if (status)
            {
                status = false;
                lbl_stat.visibleTemp(gameTime, 2000);
            }

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
