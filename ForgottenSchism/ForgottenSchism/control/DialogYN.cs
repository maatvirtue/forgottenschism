using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.screen;

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
            lbl_txt = new Label("");
            lbl_txt.Position = new Vector2(125, 110);
            add(lbl_txt);

            lnk_yes = new Link("Yes");
            lnk_yes.Position = new Vector2(110, 220);
            lnk_yes.selected = com;
            add(lnk_yes);

            lnk_no = new Link("No");
            lnk_no.Position = new Vector2(250, 220);
            lnk_no.selected = com;
            add(lnk_no);

            pos = new Vector2(100, 100);
            size = new Vector2(300, 150);

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
