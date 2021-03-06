﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.engine;
using ForgottenSchism.control;
using ForgottenSchism.world;
using Microsoft.Xna.Framework.Media;

namespace ForgottenSchism.screen
{
    public class Region : Screen
    {
        /// <summary>
        /// called when Region battle is over
        /// </summary>
        public EventHandler done;

        Map map;
        Label lbl_sel;
        Label lbl_city;
        Label lbl_cityName;
        Label lbl_unit;
        Label lbl_unitName;
        Label lbl_mov;
        Label lbl_movText;
        Label lbl_enter;
        Label lbl_esc;
        Label lbl_escText;
        Label lbl_e;
        Label lbl_eText;
        Label lbl_v;
        Label lbl_vAction;
        bool freemode;
        Point scp;
        Point p;
        Tilemap tm;
        UnitMap umap;
        Point mainBase;
        CityMap cmap;
        Point rp;
        int rm;
        Objective goal;
        AI ai;

        int enemyFactor;

        /// <summary>
        /// Position of the cursor at the end of turn
        /// </summary>
        Point endTurnP;

        Label lbl_armyTurn;
        Label lbl_battleOutcome;

        /// <summary>
        /// List of AI org that have to do their turns
        /// </summary>
        List<String> orgls;

        /// <summary>
        /// If we won the Objective in the Battle Screen
        /// </summary>
        public bool win;

        /// <summary>
        /// Number of turn left to defend city (Objective)
        /// </summary>
        int wntl;

        public Region(Tilemap ftm, City.CitySide attSide, bool att, int ef, Objective fgoal)
        {
            MainWindow.BackgroundImage = Content.Graphics.Instance.Images.background.bg_smallMenu;

            enemyFactor = ef;

            goal = fgoal;
            win = false;

            if (goal.Type == Objective.Objective_Type.DEFEND_CITY)
                wntl = goal.Turns;

            rp = new Point(-1, -1);
            endTurnP = new Point(-1, -1);
            p = new Point(-1, -1);

            //battle = false;

            orgls = new List<string>();

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

            setOwnership(ms, es, "enemy");

            cmap.get(mainBase.X, mainBase.Y).Owner = "main";

            scp = new Point(mainBase.X, mainBase.Y);

            map = new Map(tm);
            map.ArrowEnabled = true;
            map.SelectionEnabled = false;
            map.changeCurp = changeCurp;
            map.curSelection = sel;
            map.focus(scp.X, scp.Y);
            MainWindow.add(map);

            umap = new UnitMap(tm);
            umap.ShowMisc = true;
            umap.add(scp.X, scp.Y, GameState.CurrentState.mainArmy.MainCharUnit);

            genEnnemy(es, ef);

            umap.update(map);
            
            freemode = true;

            lbl_city = new Label("City");
            lbl_city.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_city.Position = new Vector2(50, 410);
            lbl_city.Visible = false;
            MainWindow.add(lbl_city);

            lbl_cityName = new Label("");
            lbl_cityName.Position = new Vector2(100, 410);
            lbl_cityName.Visible = false;
            MainWindow.add(lbl_cityName);

            lbl_unit = new Label("Unit Name");
            lbl_unit.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_unit.Position = new Vector2(50, 440);
            MainWindow.add(lbl_unit);

            lbl_unitName = new Label("");
            lbl_unitName.Position = new Vector2(150, 440);
            MainWindow.add(lbl_unitName);

            lbl_mov = new Label("Movement");
            lbl_mov.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_mov.Position = new Vector2(50, 470);
            MainWindow.add(lbl_mov);

            umap.get(scp.X, scp.Y).resetMovement();

            lbl_movText = new Label("");
            lbl_movText.Position = new Vector2(150, 470);
            MainWindow.add(lbl_movText);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_v.Position = new Vector2(50, 500);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Unit");
            lbl_vAction.Position = new Vector2(80, 500);
            MainWindow.add(lbl_vAction);

            lbl_e = new Label("E");
            lbl_e.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_e.Position = new Vector2(400, 410);
            MainWindow.add(lbl_e);

            lbl_eText = new Label("End Turn");
            lbl_eText.Position = new Vector2(430, 410);
            MainWindow.add(lbl_eText);

            Label lbl_a = new Label("A");
            lbl_a.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_a.Position = new Vector2(400, 440);
            MainWindow.add(lbl_a);

            Label lbl_mode = new Label("Army Screen");
            lbl_mode.Position = new Vector2(430, 440);
            MainWindow.add(lbl_mode);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_enter.Position = new Vector2(400, 470);
            MainWindow.add(lbl_enter);

            lbl_sel = new Label("Select Unit");
            lbl_sel.Position = new Vector2(480, 470);
            MainWindow.add(lbl_sel);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_esc.Position = new Vector2(400, 500);
            lbl_esc.Visible = false;
            MainWindow.add(lbl_esc);

            lbl_escText = new Label("Cancel Movement");
            lbl_escText.Position = new Vector2(460, 500);
            lbl_escText.Visible = false;
            MainWindow.add(lbl_escText);

            lbl_armyTurn = new Label("ONWARDS TROOPS!");
            lbl_armyTurn.Font = Content.Graphics.Instance.TurnFont;
            lbl_armyTurn.center(50);
            lbl_armyTurn.doneShowing = armyTurnDone;
            MainWindow.add(lbl_armyTurn);

            lbl_battleOutcome = new Label("VICTORY!");
            lbl_battleOutcome.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.CONTROL;
            lbl_battleOutcome.Font = Content.Graphics.Instance.TurnFont;
            lbl_battleOutcome.center(50);
            lbl_battleOutcome.doneShowing = endOfBattle;
            lbl_battleOutcome.Visible = false;
            MainWindow.add(lbl_battleOutcome);

            //map.CurLs.Add(new Point(5, 3), Content.Graphics.Instance.Images.gui.cursorRed);

            changeCurp(this, new EventArgObject(new Point(scp.X, scp.Y)));

            /*enemyTurn = false;

            lastTimeAction = new TimeSpan(0);

            battleOutcome = false;
            regionEnd = false;*/

            
            ai = new AI();
            ai.set(map, tm, umap);
            ai.done = ai_done;
        }

        public String RegionName
        {
            get { return tm.Name; }
        }

        public override void start()
        {
            base.start();

            MediaPlayer.Stop();
        }

        private void setOwnership(City.CitySide mside, City.CitySide eside, String eorg)
        {
            for (int i = 0; i < cmap.NumX; i++)
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isCity(i, e))
                        if (cmap.get(i, e).Side == eside)
                            cmap.get(i, e).Owner = eorg;
                        else if (cmap.get(i, e).Side == mside)
                            cmap.get(i, e).Owner = "main";
        }

        private void genEnnemy(City.CitySide eside, int ef)
        {
            Array ctype_val = Enum.GetValues(typeof(Character.Class_Type));
            Random rand = new Random();
            Character.Class_Type r = (Character.Class_Type)ctype_val.GetValue(rand.Next(ctype_val.Length));

            List<VirtualUnit> vuls;

            if (Content.Instance.emap.ContainsKey(tm.Name) && Content.Instance.emap[tm.Name].Count>0)
                vuls = Content.Instance.emap[tm.Name];
            else
            {
                vuls = new List<VirtualUnit>();
                vuls.Add(new VirtualUnit(ef, ef, r, "enemy", "DUMMY"));
            }

            if (ef == 0)
            {
                List<VirtualUnit> tmp = new List<VirtualUnit>();

                for (int i = 0; i < (vuls.Count / 2); i++)
                    tmp.Add(vuls[i]);

                vuls = tmp;
            }

            int vi = 0;

            for (int i = 0; i < cmap.NumX; i++)
            {
                for (int e = 0; e < cmap.NumY; e++)
                    if (cmap.isCity(i, e) && cmap.get(i, e).Side == eside)
                    {
                        umap.add(i, e, vuls[vi].gen());

                        vi++;

                        if (vi > vuls.Count - 1)
                            break;
                    }

                if (vi > vuls.Count - 1)
                    break;
            }
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

                if (!umap.isUnit(p.X, p.Y)||umap.get(p.X, p.Y).Organization!="main")
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

                //battle = false;

                StateManager.Instance.goForward(new Battle(umap.get(scp.X, scp.Y), umap.get(np.X, np.Y), this, goal));
                return;
            }

            if (!umap.canMove(np.X, np.Y, "main"))
                return;

            if (umap.get(scp.X, scp.Y).movement <= 0)
                return;

            umap.get(scp.X, scp.Y).movement--;

            umap.move(scp.X, scp.Y, np.X, np.Y);
            umap.update(map);

            if (cmap.isCity(np.X, np.Y))
                cmap.get(np.X, np.Y).Owner = "main";

            scp = np;

            changeCurp(this, new EventArgObject(new Point(np.X, np.Y)));

            map.focus(np.X, np.Y);
        }

        private void ai_done(object o, EventArgs e)
        {
            if (orgls.Count > 0)
            {
                ai.region(this, orgls[0]);
                orgls.Remove(orgls[0]);
            }
            else
            {
                umap.resetAllMovement();
                map.focus(endTurnP.X, endTurnP.Y);
                changeCurp(this, new EventArgObject(endTurnP));

                umap.update(map);

                lbl_armyTurn.Text = "YOUR TURN";
                lbl_armyTurn.Color = Color.Blue;
                lbl_armyTurn.center();
                lbl_armyTurn.visibleTemp(1000);
            }
        }

        private void armyTurnDone(object o, EventArgs e)
        {
            if (lbl_armyTurn.Text == "ENEMY TURN")
            {
                turn();
            }
            else
            {
                MainWindow.InputEnabled = true;
            }
        }

        private void endOfBattle(object o, EventArgs e)
        {
            if (lbl_battleOutcome.Text == "DEFEAT!")
                StateManager.Instance.goForward(new GameOver());
            else
            {
                if (enemyFactor == 1)
                    GameState.CurrentState.alignment++;
                else if(enemyFactor == 2)
                    GameState.CurrentState.alignment--;

                if (enemyFactor > 0)
                {
                    Story s = new Story(tm.Name + "End");
                    s.Done = region_end;

                    StateManager.Instance.goForward(s);
                }
                else
                    region_end(this, null);
            }
        }

        private void turn()
        {
            if (win)
                return;

            wntl--;

            int wc = checkWin();

            if (wc == 0)
            {
                lbl_battleOutcome.Text = "VICTORY!";
                lbl_battleOutcome.center(50);
                lbl_battleOutcome.Color = Color.Blue;
                lbl_battleOutcome.visibleTemp(2000);

                MainWindow.InputEnabled = false;
                return;
            }
            else if (wc == 1)
            {
                lbl_battleOutcome.Text = "DEFEAT!";
                lbl_battleOutcome.center(50);
                lbl_battleOutcome.Color = Color.Red;
                lbl_battleOutcome.visibleTemp(2000);

                MainWindow.InputEnabled = false;
                return;
            }

            orgls.Clear();

            foreach (String str in umap.getAllOrg())
                if (str != "main")
                    orgls.Add(str);

            if (orgls.Count != 0)
            {
                ai.region(this, orgls[0]);
                orgls.Remove(orgls[0]);
            }
            else
                ai_done(this, null);
        }

        /// <summary>
        /// Check if the player Win, Loses or none yet.
        /// </summary>
        /// <returns>0-Win, 1-Fail, 2-none</returns>
        private int checkWin()
        {
            int wc = -1;

            if (goal.Type == Objective.Objective_Type.DEFEAT_ALL)
            {
                if (!umap.isOrg("enemy"))
                    wc = 0;
                else
                    wc = 2;
            }
            else if (goal.Type == Objective.Objective_Type.CAPTURE_CITY)
            {
                if (cmap.get(goal.City.X, goal.City.Y).Owner == "main")
                    wc = 0;
                else
                    wc = 2;
            }
            else if (goal.Type == Objective.Objective_Type.DEFEND_CITY)
            {
                if (cmap.get(goal.City.X, goal.City.Y).Owner != "main")
                    wc = 1;
                else if (wntl <= 0)
                    wc = 0;
                else
                    wc = 2;
            }

            if (wc==0)
            {
                Point p = GameState.CurrentState.mainCharPos;
                GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Owner = "main";
                GameState.CurrentState.citymap["gen"].get(p.X, p.Y).EnnemyFactor = 0;


            }

            return wc;
        }

        public override void resume()
        {
            base.resume();

            umap.remDeadUnit();
            umap.update(map);

            if (win)
                return;

            int wc=checkWin();

            if (wc == 0)
            {
                lbl_battleOutcome.Text = "VICTORY!";
                lbl_battleOutcome.center(50);
                lbl_battleOutcome.Color = Color.Blue;
                lbl_battleOutcome.visibleTemp(2000);

                MainWindow.InputEnabled = false;
                return;
            }
            else if (wc == 1)
            {
                lbl_battleOutcome.Text = "DEFEAT!";
                lbl_battleOutcome.center(50);
                lbl_battleOutcome.Color = Color.Red;
                lbl_battleOutcome.visibleTemp(2000);

                MainWindow.InputEnabled = false;
                return;
            }

            if (umap.countUnitOrg("enemy") == 0)
            {
                Point p = GameState.CurrentState.mainCharPos;
                GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Owner = "main";
                GameState.CurrentState.citymap["gen"].get(p.X, p.Y).EnnemyFactor = 0;

                //battleOutcome = true;
                return;
            }

            changeCurp(this, new EventArgObject(new Point(scp.X, scp.Y)));
        }

        private void region_start(object o, EventArgs e)
        {
            StateManager.Instance.goBack();
            lbl_armyTurn.visibleTemp(2000);
        }

        private void region_end(object o, EventArgs e)
        {
            if (tm.Name == "Prophet Shrine")
            {
                Character.Stats.Traits mod = new Character.Stats.Traits();
                mod.norm();

                Item i = new Item("The Prophet's Amulet", Item.Item_Type.CONSUMABLE, 0, mod);

                GameState.CurrentState.mainArmy.MainCharUnit.Inventory.Items.Add(i);
            }

            StateManager.Instance.reset(new WorldMap());
        }

        private void changeCurp(object o, EventArgs e)
        {
            p = (Point)(((EventArgObject)e).o);

            if (umap.isUnit(p.X, p.Y))
            {
                lbl_mov.Visible = true;
                lbl_movText.Text = umap.get(p.X, p.Y).movement.ToString();
                lbl_movText.Visible = true;

                lbl_unit.Visible = true;
                lbl_unitName.Text = umap.get(p.X, p.Y).Name.ToString();
                lbl_unitName.Visible = true;

                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
            }
            else
            {
                lbl_mov.Visible = false;
                lbl_movText.Visible = false;

                lbl_unit.Visible = false;
                lbl_unitName.Visible = false;

                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
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

            if (ai.Active)
            {
                ai.Update(gameTime);
                return;
            }

            if (lbl_armyTurn.Visible && lbl_armyTurn.Text == "ONWARDS TROOPS!" && MainWindow.InputEnabled)
            {
                MainWindow.InputEnabled = false;

                if (enemyFactor > 0)
                {
                    Story ss = new Story(tm.Name + "Intro");
                    ss.Done = region_start;

                    StateManager.Instance.goForward(ss);
                }
                else
                {
                    lbl_armyTurn.visibleTemp(2000);
                }
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
                if (InputHandler.keyReleased(Keys.A))
                {
                    ArmyManage a = new ArmyManage();

                    a.deploy = deploy;

                    StateManager.Instance.goForward(a);
                }

                if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
                {
                    StateManager.Instance.goForward(new UnitManage(umap.get(p.X, p.Y)));
                }

                if (InputHandler.keyReleased(Keys.E))
                {
                    endTurnP = p;
                    //enemyTurn = true;

                    MainWindow.InputEnabled = false;
                    lbl_armyTurn.Text = "ENEMY TURN";
                    lbl_armyTurn.Color = Color.Red;
                    lbl_armyTurn.center(50);
                    lbl_armyTurn.visibleTemp(1000);

                    //lastTimeAction = gameTime.TotalGameTime + TimeSpan.FromMilliseconds(500);
                }

                /*if (InputHandler.keyReleased(Keys.Escape))
                {
                    Point p=GameState.CurrentState.mainCharPos;
                    GameState.CurrentState.citymap["gen"].get(p.X, p.Y).Owner = "main";
                    GameState.CurrentState.citymap["gen"].get(p.X, p.Y).EnnemyFactor = 0;

                    if (enemyFactor == 1)
                        GameState.CurrentState.alignment++;
                    else if (enemyFactor == 2)
                        GameState.CurrentState.alignment--;

                    StateManager.Instance.goBack();
                }*/
            }
        }
    }
}
