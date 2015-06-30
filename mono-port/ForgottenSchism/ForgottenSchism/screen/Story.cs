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
        /// text id (from content: text.xml)
        /// </summary>
        String tid;

        String heroName = GameState.CurrentState.mainArmy.MainCharUnit.Leader.Name;

        public Story(String ftid)
        {
            tid = ftid;

            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_menuless;

            lnk_cont = new Link("Continue");
            lnk_cont.Position = new Vector2(75, 450);
            MainWindow.add(lnk_cont);

            txta_test = new TextAnim(Content.Instance.text[tid]);
            txta_test.Position = new Vector2(50, 50);
            txta_test.animate();
            MainWindow.add(txta_test);
        }

        /// <summary>
        /// evenhandler called when the link continue is activated
        /// </summary>
        public EventHandler Done
        {
            get { return lnk_cont.selected; }
            set { lnk_cont.selected = value; }
        }
    }
}
