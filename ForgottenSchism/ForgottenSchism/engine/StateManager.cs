using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.screen;

namespace ForgottenSchism.engine
{
    public class StateManager
    {
        static StateManager instance;

        Screen prev;
        Screen cur;

        private StateManager()
        {
            cur = null;
        }

        public static StateManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new StateManager();

                return instance;
            }
        }

        public Screen State
        {
            get { return cur; }
        }

        public void go(Screen sc)
        {
            if (cur != null)
                cur.stop();

            Game1.Instance.Components.Remove(cur);
            Game1.Instance.Components.Add(sc);

            sc.start();

            prev = cur;
            cur = sc;
        }

        public void goBack()
        {
            if (prev is ArmyManage)
                go(new ArmyManage());
            else if (prev is PauseMenu)
                go(new PauseMenu());
            else if (prev is MainMenu)
                go(new MainMenu());
            else if (prev is WorldMap)
                go(new WorldMap());
            else
                go(new MainMenu());
        }
    }
}
