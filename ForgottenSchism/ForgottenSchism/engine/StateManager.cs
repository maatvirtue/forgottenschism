﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.screen;

namespace ForgottenSchism.engine
{
    public class StateManager
    {
        static StateManager instance;

        Stack<Screen> cstate;

        private StateManager()
        {
            cstate = new Stack<Screen>();
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

        public StateManager(Screen sc)
        {
            cstate = new Stack<Screen>();
            goForward(sc);
        }

        public Screen State
        {
            get
            {
                if (cstate.Count > 0)
                    return cstate.Peek();
                else
                    return null;
            }
        }

        public Screen goBack()
        {
            cstate.Peek().stop();

            InputHandler.flush();

            //I know its fucked up. Dont cry
            if (cstate.Peek() is Battle && ((Battle)cstate.Peek()).done!=null)
                ((Battle)cstate.Peek()).done(this, null);

            cstate.Pop();
            cstate.Peek().resume();

            Screen sc = cstate.Peek();
            return sc;
        }

        public void changeCur(Screen sc)
        {
            goBack();
            goForward(sc);
        }

        public void goForward(Screen sc)
        {
            if (cstate.Count > 0)
                cstate.Peek().pause();

            InputHandler.flush();

            Game1.Instance.Components.Add(sc);

            cstate.Push(sc);
            sc.start();
        }

        public void clear()
        {
            while (cstate.Count > 0)
            {
                cstate.Pop().stop();
            }
        }

        public void reset(Screen sc)
        {
            clear();
            goForward(sc);
        }
    }
}
