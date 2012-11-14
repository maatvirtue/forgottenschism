using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class Region : Screen
    {
        Map map;
        Label lbl_sel;
        Label lbl_city;
        Label lbl_cityName;
        Label lbl_mov;
        Label lbl_movText;
        Label lbl_enter;
        Label lbl_esc;
        Label lbl_escText;
        Label lbl_e;
        Label lbl_eText;
        bool freemode;
        bool battle;
        Point scp;
        Tilemap tm;
        UnitMap umap;
        Point mainBase;
        CityMap cmap;
        Point rp;
        int rm;

        public Region(Tilemap ftm, City.CitySide attSide, bool att, int ef)
        {
            rp = new Point(-1, -1);

            battle = false;

            tm = ftm;
            cmap = GameState.CurrentState.citymap[tm.Name];
            
            GameState.CurrentState.mainArmy.MainCharUnit.Deployed = true;

            City.CitySide ms;
            City.CitySide es;

            if (att)
            {
                ms = attSide;
                es = City.opposed(attSide);
            }
            else
            {
                ms = City.opposed(attSide);
                es = attSide;
            }

            mainBase = getMainBase(ms);

            scp = new Point(mainBase.X, mainBase.Y);

            map = new Map(tm);
            map.ArrowEnabled = true;
            map.SelectionEnabled = false;
            map.changeCurp = changeCurp;
            map.curSelection = sel;
            map.focus(scp.X, scp.Y);
            cm.add(map);

            umap = new UnitMap(tm);
            umap.add(scp.X, scp.Y, GameState.CurrentState.mainArmy.MainCharUnit);

            genEnnemy(es, ef);

            umap.update(map);
            
            freemode = true;

            cm.ArrowEnable = false;

            lbl_esc = new Label("Esc");
            lbl_esc.Color = Color.Blue;
            lbl_esc.Position = new Vector2(450, 500);
            lbl_esc.Visible = false;
            cm.add(lbl_esc);

            lbl_escText = new Label("Cancel Movement");
            lbl_escText.Color = Color.White;
            lbl_escText.Position = new Vector2(525, 500);
            lbl_escText.Visible = false;
            cm.add(lbl_escText);

            lbl_city = new Label("City");
            lbl_city.Color = Color.Blue;
            lbl_city.Position = new Vector2(50, 400);
            lbl_city.Visible = false;
            cm.add(lbl_city);

            lbl_mov = new Label("Movement");
            lbl_mov.Color = Color.Yellow;
            lbl_mov.Position = new Vector2(50, 450);
            cm.add(lbl_mov);

            umap.get(scp.X, scp.Y).resetMovement();

            lbl_movText = new Label("");
            lbl_movText.Color = Color.White;
            lbl_movText.Position = new Vector2(150, 450);
            cm.add(lbl_movText);

            lbl_cityName = new Label("");
            lbl_cityName.Color = Color.White;
            lbl_cityName.Position = new Vector2(100, 400);
            lbl_cityName.Visible = false;
            cm.add(lbl_cityName);

            lbl_e = new Label("E");
            lbl_e.Color = Color.Blue;
            lbl_e.Position = new Vector2(450, 400);
            cm.add(lbl_e);

            lbl_eText = new Label("End Turn");
            lbl_eText.Color = Color.White;
            lbl_eText.Position = new Vector2(550, 400);
            cm.add(lbl_eText);

            Label lbl_a = new Label("A");
            lbl_a.Color = Color.Blue;
            lbl_a.Position = new Vector2(450, 425);
            cm.add(lbl_a);

            Label lbl_mode = new Label("Army Screen");
            lbl_mode.Color = Color.White;
            lbl_mode.Position = new Vector2(550, 425);
            cm.add(lbl_mode);

            lbl_enter = new Label("Enter");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            lbl_sel = new Label("Select Unit");
            lbl_sel.Color = Color.White;
            lbl_sel.Position = new Vector2(550, 450);
            cm.add(lbl_sel);

            changeCurp(this, new EventArgObject(new Point(scp.X, scp.Y)));
        }

        private void genEnnemy(City.CitySide eside, int ef)
        {
            if (ef == 0)
                return;

            Character w = new Caster("DEVIL");
            w.Organization = "ennemy";
            Unit u = new Unit(w);
            u.Organization = "ennemy";

            int x = 0;
            int y = 0;

            ef--;

            while (ef > 0)
            {
                if (!u.isChar(x, y))
                {
                    u.set(x, y, w);
                    ef--;
                }

                x++;

                if (x > 3)
                {
                    y++;
                    x = 0;
                }
            }

            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isCity(i, e) && cmap.get(i, e).Side == eside)
                        umap.add(i, e, u.clone());
        }

        private Point getMainBase(City.CitySide mainSide)
        {
            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isCity(i, e) && cmap.get(i, e).Side == mainSide)
                        return new Point(i, e);

            return new Point(tm.StartingPosition.X, tm.StartingPosition.Y);
        }

        private void sel(object o, EventArgs e)
        {
            if (freemode)
            {
                InputHandler.flush();

                Point p = (Point)((EventArgObject)e).o;

                if (!umap.isUnit(p.X, p.Y))
                    return;

                if (umap.get(p.X, p.Y).movement <= 0)
                    return;

                rm = umap.get(p.X, p.Y).movement;

                lbl_sel.Text = "Confirm Movement";
                lbl_esc.Visible = true;
                lbl_escText.Visible = true;
                lbl_e.Visible = false;
                lbl_eText.Visible = false;

                freemode = false;
                map.ArrowEnabled = false;
                map.focus(p.X, p.Y);

                scp = p;
                rp = p;
            }
        }

        private bool deploy(object s, object o)
        {
            if (o == null || umap.isUnit(mainBase.X, mainBase.Y))
                return false;

            Unit u = (Unit)o;

            u.resetMovement();

            umap.add(mainBase.X, mainBase.Y, u);
            umap.update(map);

            return true;
        }
        
        private void moveUnit(Point np)
        {
            if (np.X < 0 || np.X >= tm.NumX || np.Y < 0 || np.Y >= tm.NumY)
                return;

            Tile t = tm.get(np.X, np.Y);

            if (t.Type == Tile.TileType.WATER || t.Type == Tile.TileType.MOUNTAIN)
                return;

            if (umap.ennemy(np.X, np.Y))
            {
                rp = new Point(-1, -1);
                umap.get(scp.X, scp.Y).movement = 0;
                freemode = true;

                lbl_e.Visible = true;
                lbl_eText.Visible = true;
                lbl_esc.Visible = false;
                lbl_escText.Visible = false;
                lbl_sel.Text = "Select Unit";
                map.ArrowEnabled = true;

                battle = false;

                StateManager.Instance.goForward(new Battle(umap.get(scp.X, scp.Y), umap.get(np.X, np.Y)));
                return;
            }

            if (!umap.canMove(np.X, np.Y, "main"))
                return;

            if (umap.get(scp.X, scp.Y).movement <= 0)
                return;

            umap.get(scp.X, scp.Y).movement--;

            umap.move(scp.X, scp.Y, np.X, np.Y);
            umap.update(map);

            scp = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            map.focus(np.X, np.Y);
        }

        private void turn()
        {
            Unit[] b;

            foreach (String str in umap.getAllOrg())
                if (str != "main")
                {
                    b=AI.region(umap, tm, str);

                    if (b != null)
                    {
                        battle = true;
                        StateManager.Instance.goForward(new Battle(b[0], b[1]));
                        return;
                    }
                }

            battle = false;

            umap.update(map);

            umap.resetAllMovement();
            changeCurp(this, new EventArgObject(scp));
        }

        public override void resume()
        {
            base.resume();

            umap.remDeadUnit();
            umap.update(map);

            if (battle)
                turn();

            if (umap.countUnitOrg("ennemy") == 0)
            {
                StateManager.Instance.goBack();
                return;
            }

            changeCurp(this, new EventArgObject(new Point(scp.X, scp.Y)));
        }

        private void changeCurp(object o, EventArgs e)
        {
            Point p = (Point)(((EventArgObject)e).o);

            if (umap.isUnit(p.X, p.Y)&&umap.get(p.X, p.Y).Organization=="main")
            {
                lbl_mov.Visible = true;
                lbl_movText.Text = umap.get(p.X, p.Y).movement.ToString();
                lbl_movText.Visible = true;
            }
            else
            {
                lbl_mov.Visible = false;
                lbl_movText.Visible = false;
            }

            if (tm.CityMap.isCity(p.X, p.Y))
            {
                lbl_city.Visible = true;

                lbl_cityName.Text = tm.CityMap.get(p.X, p.Y).Name;
                lbl_cityName.Visible = true;
            }
            else
            {
                lbl_city.Visible = false;
                lbl_cityName.Visible = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.A))
            {
                ArmyManage a = new ArmyManage();

                a.deploy = deploy;

                StateManager.Instance.goForward(a);
            }

            if (!freemode)
            {
                if (InputHandler.keyReleased(Keys.Enter))
                {
                    rp = new Point(-1, -1);
                    freemode = true;

                    lbl_e.Visible = true;
                    lbl_eText.Visible = true;
                    lbl_esc.Visible = false;
                    lbl_escText.Visible = false;
                    lbl_sel.Text = "Select Unit";
                    map.ArrowEnabled = true;
                }

                if (InputHandler.keyReleased(Keys.Up))
                {
                    Point cp = scp;

                    moveUnit(new Point(cp.X, --cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Down))
                {
                    Point cp = scp;

                    moveUnit(new Point(cp.X, ++cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Left))
                {
                    Point cp = scp;

                    moveUnit(new Point(--cp.X, cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Right))
                {
                    Point cp = scp;

                    moveUnit(new Point(++cp.X, cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    umap.move(scp.X, scp.Y, rp.X, rp.Y);
                    umap.update(map);
                    map.focus(rp.X, rp.Y);
                    scp = new Point(rp.X, rp.Y);
                    rp = new Point(-1, -1);
                    umap.get(scp.X, scp.Y).movement = rm;
                    freemode = true;

                    lbl_e.Visible = true;
                    lbl_eText.Visible = true;
                    lbl_esc.Visible = false;
                    lbl_escText.Visible = false;
                    lbl_sel.Text = "Select Unit";
                    map.ArrowEnabled = true;

                    changeCurp(this, new EventArgObject(scp));
                }
            }
            else
            {
                if (InputHandler.keyReleased(Keys.E))
                {
                    turn();
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    Point p=GameState.CurrentState.mainCharPos;
                    GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Owner = "main";
                    GameState.CurrentState.citymap["gen"].get(p.X, p.Y).EnnemyFactor = 0;

                    StateManager.Instance.goBack();
                }
            }
        }
    }
}
