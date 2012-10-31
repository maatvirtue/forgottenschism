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
    public class WorldMap: Screen
    {
        Map map;
        Label lbl_city;
        Label lbl_cityName;
        bool freemode;

        public WorldMap()
        {
            freemode = false;

            cm.ArrowEnable = false;

            map = new Map(Content.Instance.gen);
            map.ArrowEnabled = false;
            map.SelectionEnabled = false;
            map.Fog = GameState.CurrentState.gen;
            map.changeCurp = changeCurp;
            map.CharLs.Add(GameState.CurrentState.mainCharPos, Graphic.getSprite(GameState.CurrentState.mainChar));
            map.focus(GameState.CurrentState.mainCharPos.X, GameState.CurrentState.mainCharPos.Y);
            cm.add(map);

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
            lbl_a.Color=Color.Blue;
            lbl_a.Position=new Vector2(450, 400);
            cm.add(lbl_a);

            Label lbl_army = new Label("Army Screen");
            lbl_army.Color = Color.White;
            lbl_army.Position = new Vector2(550, 400);
            cm.add(lbl_army);

            Label lbl_m = new Label("M");
            lbl_m.Color = Color.Blue;
            lbl_m.Position = new Vector2(450, 425);
            cm.add(lbl_m);

            Label lbl_mode = new Label("View/Move mode");
            lbl_mode.Color = Color.White;
            lbl_mode.Position = new Vector2(550, 425);
            cm.add(lbl_mode);

            Label lbl_enter = new Label("Enter");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(450, 450);
            cm.add(lbl_enter);

            Label lbl_reg = new Label("Enter Region");
            lbl_reg.Color = Color.White;
            lbl_reg.Position = new Vector2(550, 450);
            cm.add(lbl_reg);

            Point p = GameState.CurrentState.mainCharPos;
            changeCurp(this, new EventArgObject(new Point(p.X, p.Y)));
        }

        private void moveChar(Point np)
        {
            Tilemap tm=Content.Instance.gen;

            if (np.X < 0 || np.X >= tm.NumX || np.Y < 0 || np.Y >= tm.NumY)
                return;

            Tile t=tm.get(np.X, np.Y);

            if (t.Type != Tile.TileType.ROADS && t.Type != Tile.TileType.CITY)
                return;

            map.CharLs.Remove(GameState.CurrentState.mainCharPos);
            map.CharLs.Add(np, Graphic.getSprite(GameState.CurrentState.mainChar));

            GameState.CurrentState.mainCharPos = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            clearFog(np);

            map.focus(np.X, np.Y);
        }

        private void clearFog(Point p)
        {
            Fog fog=GameState.CurrentState.gen;

            for (int i = -1; i <= 1; i++)
                for (int e = -1; e <= 1; e++)
                    if(i+p.X>=0&&e+p.Y>=0&&i+p.X<fog.NumX&&e+p.Y<fog.NumY)
                        fog.set(i+p.X, e+p.Y, false);
        }

        private void changeCurp(object o, EventArgs e)
        {
            Point p=(Point)(((EventArgObject)e).o);

            City c=Content.Instance.gen.get(p.X, p.Y).City;

            if (c != null && !GameState.CurrentState.gen.get(p.X, p.Y))
            {
                lbl_city.Visible = true;

                lbl_cityName.Text = c.Name;
                lbl_cityName.Visible = true;
            }
            else
            {
                lbl_city.Visible = false;
                lbl_cityName.Visible = false;
            }
        }

        private void changeRegion(object o, EventArgs e)
        {
            StateManager.Instance.goForward(new Region());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goForward(new PauseMenu());
            else if (InputHandler.keyReleased(Keys.A))
                StateManager.Instance.goForward(new ArmyManage());
            else if (InputHandler.keyReleased(Keys.Enter))
                StateManager.Instance.goForward(new Region());
            else if (InputHandler.keyReleased(Keys.M))
            {
                freemode = !freemode;

                Point p=GameState.CurrentState.mainCharPos;

                map.focus(p.X, p.Y);

                map.ArrowEnabled = freemode;
            }

            if (!freemode)
            {
                if (InputHandler.keyReleased(Keys.Up))
                {
                    Point cp = GameState.CurrentState.mainCharPos;

                    moveChar(new Point(cp.X, --cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Down))
                {
                    Point cp = GameState.CurrentState.mainCharPos;

                    moveChar(new Point(cp.X, ++cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Left))
                {
                    Point cp = GameState.CurrentState.mainCharPos;

                    moveChar(new Point(--cp.X, cp.Y));
                }

                if (InputHandler.keyReleased(Keys.Right))
                {
                    Point cp = GameState.CurrentState.mainCharPos;

                    moveChar(new Point(++cp.X, cp.Y));
                }
            }
        }
    }
}
