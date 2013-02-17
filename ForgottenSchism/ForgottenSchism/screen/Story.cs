using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;

namespace ForgottenSchism.screen
{
    class Story: Screen
    {
        Label lbl_test;

        public Story()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_bigMenu;

            lbl_test = new Label("sadasd asd asdsa dasd\n asdas sdas dasd asdsa  dasd asd a\n dasdsad sadasd asdasd as dasdsad\n asdasd asdas d asdad asdas dasd as da");
            lbl_test.Color = Color.Blue;
            lbl_test.Position = new Vector2(50, 50);
        }
    }
}
