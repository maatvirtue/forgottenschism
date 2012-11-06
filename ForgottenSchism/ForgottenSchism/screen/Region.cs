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
        Label lbl_enter;
        bool freemode;
        Point scp;
        Tilemap tm;
        UnitMap umap;
        Point mainBase;
        CityMap cmap;

        public Region(Tilemap ftm, City.CitySide attSide, bool att)
        {
            tm = ftm;
            cmap = GameState.CurrentState.citymap[tm.Name];
            
            GameState.CurrentState.mainArmy.MainCharUnit.Deployed = true;

            City.CitySide ms;

            if (att)
                ms = attSide;
            else
                ms = City.opposed(attSide);

            System.Console.Out.WriteLine("Side: " + ms);

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
            umap.update(map);
            
            freemode = true;

            cm.ArrowEnable = false;

            lbl_city = new Label("City");
            lbl_city.Color = Color.Blue;
            lbl_city.Position = new Vector2(50, 400);
            lbl_city.Visible = false;
            cm.add(lbl_city);

            lbl_cityName = new Label("");
            lbl_cityName.Color = Color.White;
            lbl_cityName.Position = new Vector2(100, 400);
            lbl_cityName.Visible = false;
            cm.add(lbl_cityName);

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

        private Point getMainBase(City.CitySide mainSide)
        {
            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isCity(i, e) && cmap.get(i, e).Side == mainSide)
                        return new Point(i, e);

            return new Point(0, 0);
        }

        private void sel(object o, EventArgs e)
        {
            Point p=(Point)((EventArgObject)e).o;

            if (!umap.isUnit(p.X, p.Y))
                return;

            lbl_enter.Text = "Esc";
            lbl_sel.Text = "Unselect Unit";

            freemode = false;
            map.ArrowEnabled = false;
            map.focus(p.X, p.Y);

            scp = p;
        }

        private bool deploy(object s, object o)
        {
            if (o == null || umap.isUnit(mainBase.X, mainBase.Y))
                return false;

            Unit u = (Unit)o;

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

            if (!umap.canMove(np.X, np.Y, "main"))
                return;

            umap.move(scp.X, scp.Y, np.X, np.Y);
            umap.update(map);

            scp = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            map.focus(np.X, np.Y);
        }

        private void changeCurp(object o, EventArgs e)
        {
            Point p = (Point)(((EventArgObject)e).o);

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
                    Point p=GameState.CurrentState.mainCharPos;
                    GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Owner = "main";

                    StateManager.Instance.goBack();
                }
            }
        }
    }
}
