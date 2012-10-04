using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    public class TextBox: Control
    {
        Color fg;
        Color bg;
        Color border;
        Color curc;
        Texture2D tborder;
        Texture2D tbg;
        Texture2D tcur;
        char[] txt;
        SpriteFont font;
        String fonturl;
        int curp;
        int total;
        int cw;
        int ch;

        public TextBox(Game1 game, int ftotal): base(game)
        {
            fg = Color.Black;
            bg = Color.White;
            border = Color.DarkRed;
            curc = Color.Black;
            curp = 0;
            fonturl = "";
            TabStop = true;
            total = ftotal;
            txt=new char[ftotal];

            for (int i = 0; i < ftotal; i++)
                txt[i] = ' ';
        }

        public char[] Text
        {
            set { txt = value; }
            get { return txt; }
        }

        public String FontUrl
        {
            set { fonturl = value; }
        }
        
        public Color CurColor
        {
            set { curc = value; }
        }

        public Color TextColor
        {
            set { fg = value; }
        }

        public Color BgColor
        {
            set { bg = value; }
        }

        public Color BorderColor
        {
            set { border = value; }
        }

        public override void loadContent()
        {
            base.LoadContent();

            if (fonturl == "")
                fonturl = "font\\mono12norm";

            font = game.Content.Load<SpriteFont>(@fonturl);

            cw = (int)font.MeasureString("M").X;
            ch = (int)font.MeasureString("M").Y;

            Size = new Vector2((total * cw)+(6*2), ch+(6*2)); //1px space, 2px cur, 1px space, 2px border

            tborder = Graphic.rect(game, (int)Size.X, (int)Size.Y, border);

            tbg = Graphic.rect(game, (int)Size.X - 4, (int)Size.Y - 4, bg);
            tcur = Graphic.rect(game, cw, 2, curc);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            float t = 0;

            if (HasFocus)
                t = 1;

            game.sb.Draw(tborder, new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y), Color.White*t);
            game.sb.Draw(tbg, new Rectangle((int)Position.X+2, (int)Position.Y+2, (int)Size.X-4, (int)Size.Y-4), Color.White);
            game.sb.Draw(tcur, new Rectangle((int)(Position.X + 6 + (curp * cw)), (int)(Position.Y + 5 + ch + 2), cw, 2), Color.White);
            game.sb.DrawString(font, new String(txt), new Vector2(Position.X+6, Position.Y+6), fg);
        }

        public override void HandleInput(GameTime gameTime)
        {
            foreach (Keys k in InputHandler.keysReleased())
            {
                System.Console.Out.WriteLine(k);

                if(k==Keys.Left&&curp>=1)
                    curp--;
                else if (k == Keys.Right&&curp<=total-2)
                    curp++; 
                else if (k == Keys.Back)
                {
                    if (txt[curp] != ' ')
                        txt[curp] = ' ';
                    else
                    {
                        if (curp != 0)
                            curp--;

                        txt[curp]=' ';
                    }
                }
                else if (InputHandler.isKeyLetter(k) || InputHandler.isKeyDigit(k) || k == Keys.OemMinus || k == Keys.Space)
                {
                    if(k==Keys.Space)
                        txt[curp] =' ';
                    else if (k == Keys.OemMinus)
                        txt[curp] = '-';
                    else
                        txt[curp] = k.ToString().ElementAt(0);

                    if (curp < total - 1)
                    {
                        curp++;
                    }
                }
            }
        }
    }
}
