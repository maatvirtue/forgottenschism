using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class DialogTxt: Window
    {
        Label lbl_text;
        TextBox txt;

        public EventHandler complete;

        public DialogTxt(String fq)
        {
            lbl_text = new Label(fq);
            lbl_text.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.NORM;
            lbl_text.Position = new Vector2(125, 125);
            add(lbl_text);

            txt = new TextBox(15);
            txt.Position = new Vector2(125, 200);
            txt.HasFocus = true;
            add(txt);

            pos = new Point(100, 100);
            size = new Point(300, 150);
        }

        public char[] CText
        {
            get { return txt.CText; }
            set { txt.CText = value; }
        }
    }
}
