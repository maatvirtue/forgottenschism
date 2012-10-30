using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;

namespace ForgottenSchism.engine
{
    public class GameState
    {
        static GameState currentState;
         
        public bool saved;
        
        public Army mainArmy;
        public Character mainChar;
        public Point mainCharPos;
        public Fog gen;

        public GameState()
        {
            saved = false;
        }

        public void save(String path)
        {
            //
        }

        public void load(String path)
        {
            //
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
