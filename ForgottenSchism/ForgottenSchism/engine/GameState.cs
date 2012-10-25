using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    class GameState
    {
        static GameState currentState;

        public bool saved;
        public Character mainChar;
        public Army mainArmy;
        public Point mainCharPos;
        public Fog gen;
        public Point genPos;

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
