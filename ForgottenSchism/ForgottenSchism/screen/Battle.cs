using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class Battle: Screen
    {
        Map map;
        Tilemap tm;
        CharMap cmap;
        bool freemode;
        Label lbl_sel;
        Label lbl_enter;
        Point scp;

        public Battle(Unit m, Unit e)
        {
            tm=new Tilemap("battle");

            cmap = new CharMap(tm);

            map = new Map(tm);
            map.ArrowEnabled = true;
            map.SelectionEnabled = false;
            map.changeCurp = changeCurp;
            map.curSelection = sel;
            map.focus(5, 6);
            cm.add(map);

            lbl_enter = new Label("Enter");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            lbl_sel = new Label("Select Unit");
            lbl_sel.Color = Color.White;
            lbl_sel.Position = new Vector2(550, 450);
            cm.add(lbl_sel);

            deploy(m, true);
            deploy(e, false);

            cmap.update(map);

            freemode = true;
            cm.ArrowEnable = false;
        }

        private void changeCurp(object o, EventArgs e)
        {
            Point p = (Point)(((EventArgObject)e).o);

            //
        }

        private void moveChar(Point np)
        {
            if (np.X < 0 || np.X >= tm.NumX || np.Y < 0 || np.Y >= tm.NumY)
                return;

            Tile t = tm.get(np.X, np.Y);

            if (t.Type == Tile.TileType.WATER || t.Type == Tile.TileType.MOUNTAIN)
                return;

            if (cmap.isChar(np.X, np.Y))
                return;

            cmap.move(scp.X, scp.Y, np.X, np.Y);
            cmap.update(map);

            scp = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            map.focus(np.X, np.Y);
        }

        private void sel(object o, EventArgs e)
        {
            Point p = (Point)((EventArgObject)e).o;

            if (!cmap.isChar(p.X, p.Y)||cmap.get(p.X, p.Y).Organization!="main")
                return;

            lbl_enter.Text = "Esc";
            lbl_sel.Text = "Unselect Unit";

            freemode = false;
            map.ArrowEnabled = false;
            map.focus(p.X, p.Y);

            scp = p;
        }

        private void deploy(Unit u, bool m)
        {
            Point off;

            if (m)
                off = new Point(4, 5);
            else
                off = new Point(4, 0);

            for (int i = 0; i < 4; i++)
                for (int e = 0; e < 4; e++)
                    if (u.isChar(i, e))
                    {
                        if (m)
                            u.get(i, e).Organization = "main";

                        cmap.set(off.X + i, off.Y + e, u.get(i, e));
                    }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!freemode)
            {
                if (InputHandler.keyReleased(Keys.Up))
                {
                    Point cp = scp;

                    moveChar(new Point(cp.X, --cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Down))
                {
                    Point cp = scp;

                    moveChar(new Point(cp.X, ++cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Left))
                {
                    Point cp = scp;

                    moveChar(new Point(--cp.X, cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Right))
                {
                    Point cp = scp;

                    moveChar(new Point(++cp.X, cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    lbl_enter.Text = "Enter";
                    lbl_sel.Text = "Select Unit";

                    freemode = true;
                    map.ArrowEnabled = true;
                }
            }
            else
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    StateManager.Instance.goBack();
                }
            }
        }
    }
}
