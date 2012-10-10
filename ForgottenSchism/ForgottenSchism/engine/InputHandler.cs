using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace ForgottenSchism.engine
{
    public class InputHandler : Microsoft.Xna.Framework.GameComponent
    {
        static KeyboardState keyState;
        static KeyboardState lastState;
        static int hold = 0;

        public static KeyboardState keyboardState
        {
            get { return keyState; }
        }

        public static bool isKeyLetter(Keys key)
        {
            return key >= Keys.A && key <= Keys.Z;
        }

        public static bool isKeyDigit(Keys key)
        {
            return (key >= Keys.D0 && key <= Keys.D9) || (key >= Keys.NumPad0 && key <= Keys.NumPad9);
        }

        public static KeyboardState lastKeyboardState
        {
            get { return lastState; }
        }

        public InputHandler(Game game): base(game)
        {
            keyState = Keyboard.GetState();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (lastState == keyState)
                hold++;
            else
                hold = 0;

            lastState = keyState;
            keyState = Keyboard.GetState();
            base.Update(gameTime);
        }

        public static void flush()
        {
            lastState = keyState;
        }

        public static bool keyPressed(Keys key)
        {
            return keyState.IsKeyDown(key) && lastState.IsKeyUp(key);
        }

        public static List<Keys> keysReleased()
        {
            List<Keys> ret = new List<Keys>();

            foreach (Keys k in lastKeyboardState.GetPressedKeys())
            {
                if (keyState.IsKeyUp(k))
                    ret.Add(k);
            }

            return ret;
        }

        public static char keyChar(Keys key)
        {
            if (key == Keys.Space)
                return ' ';
            else if (key == Keys.OemMinus)
                return '-';
            else if (isKeyDigit(key))
                return key.ToString().ElementAt<char>(1);
            else if (isKeyLetter(key))
                return key.ToString().ElementAt<char>(0);
            else
                return '\0';
        }

        public static bool keyDown(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        public static bool keyReleased(Keys key)
        {
            return keyState.IsKeyUp(key) && lastState.IsKeyDown(key);
        }

        public static bool keyHeld(Keys key)
        {
            if (hold > 10 && keyState.IsKeyDown(key))
                return true;
            else
                return false;
        }
    }
}
