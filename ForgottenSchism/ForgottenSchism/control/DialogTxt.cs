using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.screen;

namespace ForgottenSchism.control
{
    class DialogTxt: Window
    {
        public delegate void TxtEvent(char[] str);

        Label lbl_text;
        TextBox txt;
        Link lnk_enter;

        /// <summary>
        /// Event triggered when the user press the "enter" link
        /// </summary>
        public TxtEvent complete;

        public DialogTxt(Screen display): base(display)
        {
            lbl_text = new Label("");
            lbl_text.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_text.Position = new Vector2(125, 110);
            add(lbl_text);

            txt = new TextBox(15);
            txt.Position = new Vector2(125, 150);
            txt.HasFocus = true;
            add(txt);

            lnk_enter = new Link("Enter");
            lnk_enter.Position = new Vector2(125, 200);
            lnk_enter.selected = sel;
            add(lnk_enter);

            pos = new Vector2(100, 100);
            size = new Vector2(300, 150);
        }

       
        /// <summary>
        /// Show the dialog with the specified text
        /// </summary>
        /// <param name="text">The text to show to the user</param>
        public void show(String text)
        {
            lbl_text.Text = text;

            show();
        }

        private void sel(object o, EventArgs e)
        {
            close();

            if (complete != null)
                complete(txt.CText);
        }
    }
}
