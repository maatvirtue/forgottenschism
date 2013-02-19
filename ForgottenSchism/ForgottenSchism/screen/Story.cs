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
        TextAnim txta_test;
        Link lnk_cont;

        /// <summary>
        /// called when the link continue is activated
        /// </summary>
        public EventHandler done;

        public Story()
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_menuless;

            lnk_cont = new Link("Continue");
            lnk_cont.Position = new Vector2(75, 450);
            lnk_cont.selected = done;
            MainWindow.add(lnk_cont);

            txta_test = new TextAnim("Lorem ipsum dolor sit amet, consectetur adipiscing elit.\n Praesent ut vulputate lectus. Phasellus at nisi in massa interdum consectetur.\n Curabitur quis ligula cursus neque fermentum posuere. Integer congue rutrum risus ac fringilla.\n Ut eget mi id augue vehicula laoreet. Proin sollicitudin, nisi sit amet euismod convallis, mauris lorem rutrum lorem, eget ultricies tellus diam sit amet massa. Nam sed arcu lorem, nec sollicitudin turpis. In magna sem, accumsan euismod porta id, scelerisque sed eros. Pellentesque in mollis sem. Morbi ultrices ullamcorper lectus, ut auctor felis lacinia at. In vitae enim neque, vel ultricies leo.\n Aliquam sed felis nisl.");
            txta_test.Position = new Vector2(50, 50);
            txta_test.animate();
            MainWindow.add(txta_test);
        }
    }
}
