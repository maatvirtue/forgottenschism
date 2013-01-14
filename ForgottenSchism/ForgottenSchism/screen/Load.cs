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
        Label lbl_stat;

        public Load()
        {
            di = false;

            dyn = new DialogYN(this);
            dyn.complete = dynChose;
            dyn.InputEnabled = false;

            Label lbl_title = new Label("Load Game");
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
            lbl_del.Color = Color.White;
            lbl_del.Position = new Vector2(100, 500);
            MainWindow.add(lbl_del);

            m = new Menu(10);
            m.Position = new Vector2(50, 75);
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

        private void load(object o, EventArgs e)
        {
            String path = ((Link)o).Text;

            GameState.CurrentState.load(".\\save\\"+path+".save");

            StateManager.Instance.reset(new WorldMap());
        }

        private void list()
        {
            m.clear();

            String s;

            foreach (String str in Directory.EnumerateFiles(".\\save\\", "*.save"))
            {
                s = Path.GetFileNameWithoutExtension(str);
                Link l = new Link(s);
                l.selected = load;
                m.add(l);
            }
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

            if (di)
                return;

            if (InputHandler.keyReleased(Keys.Escape))
                    StateManager.Instance.goBack();

            if (InputHandler.keyReleased(Keys.D))
            {
                if(m.Count > 0)
                {
                    dyn.InputEnabled = true;
                    dyn.show("Delete saved game\n" + m.Focused.Text + " ?");
                }
            }
        }
    }
}
