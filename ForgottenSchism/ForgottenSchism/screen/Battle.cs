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

        Unit ally;
        Unit enemy;
        
        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_v;
        Label lbl_vAction;
        Label lbl_esc;
        Label lbl_escAction;

        Label lbl_moved;
        Label lbl_enemyTurn;

        Point scp;
        Point p;
        Point returnP;

        public Battle(Unit m, Unit e)
        {
            ally = m;
            enemy = e;
            tm=new Tilemap("battle");

            cmap = new CharMap(tm);

            map = new Map(tm);
            map.ArrowEnabled = true;
            map.SelectionEnabled = false;
            map.changeCurp = changeCurp;
            map.curSelection = sel;
            map.focus(5, 6);
            cm.add(map);

            lbl_moved = new Label("MOVED");
            lbl_moved.Color = Color.Gold;
            lbl_moved.Position = new Vector2(450, 420);
            lbl_moved.Visible = false;
            cm.add(lbl_moved);

            lbl_enemyTurn = new Label("ENEMY TURN");
            lbl_enemyTurn.Color = Color.Red;
            lbl_enemyTurn.Position = new Vector2(50, 420);
            lbl_enemyTurn.Visible = false;
            cm.add(lbl_enemyTurn);

            lbl_enter = new Label("ENTER");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            lbl_enterAction = new Label("Select Unit");
            lbl_enterAction.Color = Color.White;
            lbl_enterAction.Position = new Vector2(550, 450);
            cm.add(lbl_enterAction);

            lbl_v = new Label("V");
            lbl_v.Color = Color.Blue;
            lbl_v.Position = new Vector2(50, 450);
            cm.add(lbl_v);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Color = Color.White;
            lbl_vAction.Position = new Vector2(80, 450);
            cm.add(lbl_vAction);

            lbl_esc = new Label("ESC");
            lbl_esc.Color = Color.Blue;
            lbl_esc.Position = new Vector2(450, 480);
            lbl_esc.Visible = false;
            cm.add(lbl_esc);

            lbl_escAction = new Label("Cancel Movement");
            lbl_escAction.Color = Color.White;
            lbl_escAction.Position = new Vector2(500, 480);
            lbl_escAction.Visible = false;
            cm.add(lbl_escAction);

            deploy(m, true);
            deploy(e, false);

            cmap.update(map);

            freemode = true;
            cm.ArrowEnable = false;
        }

        private void changeCurp(object o, EventArgs e)
        {
            p = (Point)(((EventArgObject)e).o);

            if (cmap.isChar(p.X, p.Y) && freemode)
            {
                if (cmap.get(p.X, p.Y).Moved)
                    lbl_moved.Visible = true;
                else
                    lbl_moved.Visible = false;
                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
            }
            else
            {
                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
                lbl_moved.Visible = false;
            }
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

        private void moveChar(Character c, Point np)
        {
            for (int x = 0; x < tm.NumX; x++)
            {
                for (int y = 0; y < tm.NumY; y++)
                {
                    if (cmap.get(x, y) == c)
                        scp = new Point(x, y);
                }
            }

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
            if(freemode)
            {
                InputHandler.flush();

                p = (Point)((EventArgObject)e).o;

                if (!cmap.isChar(p.X, p.Y)||cmap.get(p.X, p.Y).Organization!="main"||cmap.get(p.X, p.Y).Moved)
                    return;

                lbl_enterAction.Text = "Confirm Move";

                lbl_esc.Visible = true;
                lbl_escAction.Visible = true;

                freemode = false;
                map.ArrowEnabled = false;
                map.focus(p.X, p.Y);

                scp = p;
                returnP = p;

                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
            }
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
                    lbl_enterAction.Text = "Select Unit";

                    lbl_esc.Visible = false;
                    lbl_escAction.Visible = false;

                    moveChar(returnP);

                    freemode = true;
                    map.ArrowEnabled = true;
                    map.Enabled = true;

                    lbl_v.Visible = true;
                    lbl_vAction.Visible = true;
                }

                if (InputHandler.keyReleased(Keys.Enter))
                {
                    lbl_enterAction.Text = "Select Unit";
                    
                    lbl_esc.Visible = false;
                    lbl_escAction.Visible = false;

                    freemode = true;
                    map.ArrowEnabled = true;
                    map.Enabled = true;

                    lbl_v.Visible = true;
                    lbl_vAction.Visible = true;

                    cmap.get(p.X, p.Y).Moved = true;
                    lbl_moved.Visible = true;
                }
            }
            else
            {
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    StateManager.Instance.goBack();
                }

                if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
                {
                    StateManager.Instance.goForward(new CharManage(cmap.get(p.X, p.Y)));
                }

                if (InputHandler.keyReleased(Keys.E))
                {
                    foreach (Character c in ally.Characters)
                    {
                        c.Moved = false;
                    }

                    enemyMove();

                    if (cmap.isChar(p.X, p.Y))
                    {
                        lbl_moved.Visible = false;
                    }
                }
            }
        }

        public void enemyMove()
        {
            foreach (Character c in enemy.Characters)
            {

            }
        }
    }
}
