using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.screen;
using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class DialogYN: Window
    {
        public delegate void YNEvent(bool b);

        Label lbl_txt;
        Link lnk_yes;
        Link lnk_no;

        /// <summary>
        /// Event triggered when user make a choice
        /// </summary>
        public YNEvent complete;

        public DialogYN(Screen display): base(display)
        {
            BackgroundImage = Content.Graphics.Instance.Images.background.bg_dialog;

            size = new Vector2(300, 150);
            pos = new Vector2(Game1.Instance.Window.ClientBounds.Width / 2 - size.X / 2, 100);

            lbl_txt = new Label("");
            lbl_txt.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_txt.Position = new Vector2(pos.X + 50, pos.Y + 10);
            add(lbl_txt);

            lnk_yes = new Link("Yes");
            lnk_yes.Position = new Vector2(pos.X + 50, pos.Y + 120);
            lnk_yes.selected = com;
            add(lnk_yes);

            lnk_no = new Link("No");
            lnk_no.Position = new Vector2(pos.X + 200, pos.Y + 120);
            lnk_no.selected = com;
            add(lnk_no);

            FocusSideArrowEnabled = true;
        }


        /// <summary>
        /// Show the current dialog with the provided text
        /// </summary>
        /// <param name="text"></param>
        public void show(String text)
        {
            lbl_txt.Text = text;
            
            show();
        }

        private void com(object o, EventArgs e)
        {
            close();

            if (complete != null)
                complete(o == lnk_yes);
        }
    }
}
