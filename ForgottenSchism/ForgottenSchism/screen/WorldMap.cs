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
        DialogYN yn_battle;
        bool di;
        Point dnp;
        Point lp;

        public WorldMap()
        {
            di = false;

            GameState.CurrentState.mainArmy.MainCharUnit.Deployed = false;
            freemode = false;

            cm.ArrowEnable = false;

            yn_battle = new DialogYN("Enter battle?");
            yn_battle.Position = new Vector2(100, 100);
            yn_battle.chose = dialog_ret_battle;
            yn_battle.Enabled = false;
            yn_battle.Visible = false;
            cm.add(yn_battle);
            cm.addLastDraw(yn_battle);

            map = new Map(Content.Instance.gen);
            map.ArrowEnabled = false;
            map.SelectionEnabled = false;
            map.Fog = GameState.CurrentState.gen;
            map.changeCurp = changeCurp;
            map.CharLs.Add(GameState.CurrentState.mainCharPos, Graphic.getSprite(GameState.CurrentState.mainChar));
            map.focus(GameState.CurrentState.mainCharPos.X, GameState.CurrentState.mainCharPos.Y);
            cm.add(map);

            lp = GameState.CurrentState.mainCharPos;

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

        private void dialog_ret_battle(object o, EventArgs e)
        {
            di = false;

            map.Enabled = true;
            yn_battle.Visible = false;
            yn_battle.Enabled = false;

            if ((bool)(((EventArgObject)e).o))
            {
                GameState.CurrentState.saved = false;

                lp = GameState.CurrentState.mainCharPos;

                map.CharLs.Remove(GameState.CurrentState.mainCharPos);
                map.CharLs.Add(dnp, Graphic.getSprite(GameState.CurrentState.mainChar));

                System.Console.Out.WriteLine(GameState.CurrentState.mainCharPos+" "+dnp);
                City.CitySide atts=City.opposed(City.move2side(GameState.CurrentState.mainCharPos, dnp));

                GameState.CurrentState.mainCharPos = dnp;

                changeCurp(this, new EventArgObject(new Point(dnp.X, dnp.Y)));

                clearFog(dnp);

                map.focus(dnp.X, dnp.Y);

                Tile t = Content.Instance.gen.get(dnp.X, dnp.Y);

                if (t.isRegion())
                    StateManager.Instance.goForward(new Region(t.Region, atts, true));
            }
        }

        private void dialog_show_battle(object o, EventArgs e)
        {
            di = true;

            map.Enabled = false;
            yn_battle.Visible = true;
            yn_battle.Enabled = true;
        }

        private void moveChar(Point np)
        {
            Tilemap tm=Content.Instance.gen;

            if (np.X < 0 || np.X >= tm.NumX || np.Y < 0 || np.Y >= tm.NumY)
                return;

            Tile t=tm.get(np.X, np.Y);

            if (t.Type != Tile.TileType.ROADS && t.Type != Tile.TileType.CITY)
                return;

            if (GameState.CurrentState.citymap["gen"].isCity(np.X, np.Y) && GameState.CurrentState.citymap["gen"].get(np.X, np.Y).Owner=="ennemy")
            {
                dnp = np;
                dialog_show_battle(null, null);
                return;
            }

            lp = GameState.CurrentState.mainCharPos;

            GameState.CurrentState.saved = false;

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

            if (Content.Instance.gen.CityMap.isCity(p.X, p.Y) && !GameState.CurrentState.gen.get(p.X, p.Y))
            {
                lbl_city.Visible = true;

                lbl_cityName.Text = GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Name;
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

            if (yn_battle.Enabled)
                yn_battle.HandleInput(gameTime);

            if (di)
                return;

            if (InputHandler.keyReleased(Keys.Escape))
                StateManager.Instance.goForward(new PauseMenu());
            else if (InputHandler.keyReleased(Keys.A))
                StateManager.Instance.goForward(new ArmyManage());
            else if (InputHandler.keyReleased(Keys.Enter))
            {
                Point p = GameState.CurrentState.mainCharPos;
                Tile t = Content.Instance.gen.get(p.X, p.Y);

                City.CitySide atts = City.opposed(City.move2side(lp, GameState.CurrentState.mainCharPos));

                if(t.isRegion())
                    StateManager.Instance.goForward(new Region(t.Region, atts, true));
            }
            else if (InputHandler.keyReleased(Keys.M))
            {
                freemode = !freemode;

                Point p = GameState.CurrentState.mainCharPos;

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
