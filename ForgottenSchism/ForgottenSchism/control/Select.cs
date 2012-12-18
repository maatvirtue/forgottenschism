using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;

namespace ForgottenSchism.control
{
    class Select: Control
    {
        /// <summary>
        /// Unselected Left Arrow Texture
        /// </summary>
        Texture2D ula;

        /// <summary>
        /// Selected Left Arrow Texture
        /// </summary>
        Texture2D sla;

        /// <summary>
        /// Unselected Right Arrow Texture
        /// </summary>
        Texture2D ura;

        /// <summary>
        /// Selected Right Arrow Texture
        /// </summary>
        Texture2D sra;

        /// <summary>
        /// Activated Right Arrow Texture
        /// </summary>
        Texture2D ara;


        /// <summary>
        /// Activated Left Arrow Texture
        /// </summary>
        Texture2D ala;

        SpriteFont font;
        int dt;
        bool r;

        /// <summary>
        /// Element List
        /// </summary>
        List<String> els;

        /// <summary>
        /// Curent selected element index
        /// </summary>
        int sel;

        public EventHandler selectionChanged;

        public Select()
        {
            dt = 0;
            els = new List<String>();
            sel = 0;

            font = Content.Graphics.Instance.DefaultFont;

            loadContent();
        }

        public void add(String e)
        {
            els.Add(e);

            if (font.MeasureString(e).X+48 > Size.X)
                Size = new Vector2(font.MeasureString(e).X + 48, Size.Y);

            if (font.MeasureString(e).Y > Size.Y)
                Size = new Vector2(Size.X, font.MeasureString(e).Y);
        }

        public String SelectedValue
        {
            get { if(els.Count!=0) return els[sel]; else return null; }
        }

        public int Selection
        {
            get { return sel; }
        }

        private void loadContent()
        {
            ura = Graphic.Instance.arrowRight(20, 20, Color.Blue);
            sra = Graphic.Instance.arrowRight(20, 20, ColorTheme.Default.getColor(true, true));
            ula = Graphic.Instance.arrowLeft(20, 20, Color.Blue);
            sla = Graphic.Instance.arrowLeft(20, 20, ColorTheme.Default.getColor(true, true));
            ala = Graphic.Instance.arrowLeft(20, 20, ColorTheme.Default.ActionColor);
            ara = Graphic.Instance.arrowRight(20, 20, ColorTheme.Default.ActionColor);

            Size=new Vector2(48, 20);
        }

        public override void handleInput(GameTime gameTime)
        {
            if (InputHandler.keyReleased(Keys.Right))
            {
                sel++;
                if (sel > els.Count - 1)
                    sel = 0;
            }
            else if(InputHandler.keyReleased(Keys.Left))
            {
                sel--;
                if (sel < 0)
                    sel = els.Count - 1;
            }

            if (InputHandler.keyDown(Keys.Right))
            {
                dt = 10;
                r = true;
            }
            else if (InputHandler.keyDown(Keys.Left))
            {
                dt = 10;
                r = false;
            }
            else
            {
                if(dt>0)
                    dt--;
            }

            if (InputHandler.arrowReleased() && selectionChanged != null)
                selectionChanged(this, new EventArgObject(els[sel]));

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            if (els.Count == 0)
                els.Add(" ");

            if(HasFocus)
                if(dt>0)
                    if(!r)
                        Graphic.Instance.SB.Draw(ala, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
                    else
                        Graphic.Instance.SB.Draw(sla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
                else
                    Graphic.Instance.SB.Draw(sla, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);
            else
                Graphic.Instance.SB.Draw(ula, new Rectangle((int)Position.X, (int)Position.Y, 20, (int)Size.Y), Color.White);

            Graphic.Instance.SB.DrawString(font, els[sel], new Vector2(Position.X + 23, Position.Y), Color.White);

            if (HasFocus)
                if (dt > 0)
                    if (r)
                        Graphic.Instance.SB.Draw(ara, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
                    else
                        Graphic.Instance.SB.Draw(sra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
                else
                    Graphic.Instance.SB.Draw(sra, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
            else
                Graphic.Instance.SB.Draw(ura, new Rectangle((int)(Position.X + (Size.X - 20)), (int)Position.Y, 20, (int)Size.Y), Color.White);
        }
    }
}
