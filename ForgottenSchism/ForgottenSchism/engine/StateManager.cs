using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.screen;

namespace ForgottenSchism.engine
{
    public class StateManager
    {
        Stack<Screen> cstate;

        public StateManager()
        {
            cstate = new Stack<Screen>();
        }

        private void print()
        {
            foreach(Screen sc in cstate)
                System.Console.Out.Write(sc+" ");

            System.Console.Out.WriteLine();
        }

        public StateManager(Screen sc)
        {
            cstate = new Stack<Screen>();
            goForward(sc);
        }

        public Screen State
        {
            get { return cstate.Peek(); }
        }

        public Screen goBack()
        {
            cstate.Peek().stop();

            InputHandler.flush();

            cstate.Pop();
            cstate.Peek().resume();

            Screen sc=cstate.Peek();

            print();

            return sc;
        }

        public void goForward(Screen sc)
        {
            if(cstate.Count>0)
                cstate.Peek().pause();

            InputHandler.flush();

            cstate.Push(sc);
            sc.start();

            print();
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
