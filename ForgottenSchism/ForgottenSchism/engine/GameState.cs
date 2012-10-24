using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    class GameState
    {
        static GameState currentState;

        public bool saved;
        public Character mainChar;
        public Army mainArmy;
        public Fog gen;

        public GameState()
        {
            saved = false;
        }

        public static GameState CurrentState
        {
            get
            {
                if (currentState == null)
                    currentState = new GameState();

                return currentState;
            }

            set { currentState = value; }
        }
    }
}
