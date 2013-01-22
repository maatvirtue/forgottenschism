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
        private static readonly TimeSpan intervalBetweenAction = TimeSpan.FromMilliseconds(500);
        private static readonly TimeSpan battleOutcomeDuration = TimeSpan.FromMilliseconds(2000);
        private TimeSpan lastTimeAction;

        Map map;
        Tilemap tm;
        CharMap cmap;
        Boolean freemode;
        Boolean actionMode;
        Boolean targetMode;
        Boolean spellMode;
        Boolean allyTurn;
        Boolean displayPlayerTurn;

        Unit ally;
        Unit enemy;

        Label lbl_moveLeft;
        Label lbl_move;
        
        Label lbl_name;
        Label lbl_charName;

        Label lbl_lvl;
        Label lbl_charLvl;
        Label lbl_exp;
        Label lbl_charExp;

        Label lbl_hp;
        Label lbl_curHp;
        Label lbl_hpSlash;
        Label lbl_maxHp;

        Label lbl_mp;
        Label lbl_curMp;
        Label lbl_mpSlash;
        Label lbl_maxMp;

        Label lbl_enter;
        Label lbl_enterAction;
        Label lbl_v;
        Label lbl_vAction;
        Label lbl_esc;
        Label lbl_escAction;
        Label lbl_e;
        Label lbl_eAction;

        Label lbl_moved;
        Label lbl_enemyTurn;

        Label lbl_turnCount;

        Label lbl_dmg;

        Label lbl_armyTurn;
        Label lbl_battleOutcome;

        Point scp;
        Point p;
        Point returnP;
        Point endTurnP;

        Spell selectedSpell;

        Label lbl_actions;
        Menu menu_actions;

        List<Point> targetableChar;

        Boolean gameOver;
        Boolean defeat;

        int turnCount = 1;

        public Battle(Unit m, Unit e)
        {
            ally = m;
            enemy = e;
            tm=new Tilemap("battle");

            cmap = new CharMap(tm);
            cmap.ShowMisc = true;

            map = new Map(tm);
            map.ArrowEnabled = true;
            map.SelectionEnabled = false;
            map.changeCurp = changeCurp;
            map.curSelection = sel;
            map.focus(5, 6);
            MainWindow.add(map);

            lbl_actions = new Label("Actions");
            lbl_actions.Color = Color.Gold;
            lbl_actions.Position = new Vector2(280, 390);
            lbl_actions.Visible = false;
            MainWindow.add(lbl_actions);

            menu_actions = new Menu(5);
            menu_actions.Position = new Vector2(280, 390);
            menu_actions.Visible = false;
            MainWindow.add(menu_actions);
            menu_actions.Enabled = false;
            menu_actions.ArrowEnabled = false;

            lbl_moved = new Label("MOVED");
            lbl_moved.Color = Color.Gold;
            lbl_moved.Position = new Vector2(520, 414);
            lbl_moved.Visible = false;
            MainWindow.add(lbl_moved);

            lbl_enemyTurn = new Label("DAMAGE");
            lbl_enemyTurn.Color = Color.Red;
            lbl_enemyTurn.Position = new Vector2(50, 50/*420*/);
            lbl_enemyTurn.Visible = false;
            MainWindow.add(lbl_enemyTurn);

            lbl_name = new Label("Name");
            lbl_name.Color = Color.Brown;
            lbl_name.Position = new Vector2(50, 390);
            MainWindow.add(lbl_name);

            lbl_charName = new Label("Derp");
            lbl_charName.Color = Color.White;
            lbl_charName.Position = new Vector2(110, 390);
            MainWindow.add(lbl_charName);

            lbl_lvl = new Label("Level");
            lbl_lvl.Color = Color.Brown;
            lbl_lvl.Position = new Vector2(50, 420);
            MainWindow.add(lbl_lvl);

            lbl_charLvl = new Label("20");
            lbl_charLvl.Color = Color.White;
            lbl_charLvl.Position = new Vector2(110, 420);
            MainWindow.add(lbl_charLvl);

            lbl_exp = new Label("Exp");
            lbl_exp.Color = Color.Brown;
            lbl_exp.Position = new Vector2(150, 420);
            MainWindow.add(lbl_exp);

            lbl_charExp = new Label("42");
            lbl_charExp.Color = Color.White;
            lbl_charExp.Position = new Vector2(200, 420);
            MainWindow.add(lbl_charExp);

            lbl_hp = new Label("HP");
            lbl_hp.Color = Color.Brown;
            lbl_hp.Position = new Vector2(50, 450);
            MainWindow.add(lbl_hp);

            lbl_curHp = new Label("100");
            lbl_curHp.Color = Color.White;
            lbl_curHp.Position = new Vector2(90, 450);
            MainWindow.add(lbl_curHp);

            lbl_hpSlash = new Label("/");
            lbl_hpSlash.Color = Color.Brown;
            lbl_hpSlash.Position = new Vector2(140, 450);
            MainWindow.add(lbl_hpSlash);

            lbl_maxHp = new Label("100");
            lbl_maxHp.Color = Color.White;
            lbl_maxHp.Position = new Vector2(160, 450);
            MainWindow.add(lbl_maxHp);

            lbl_mp = new Label("MP");
            lbl_mp.Color = Color.Brown;
            lbl_mp.Position = new Vector2(50, 480);
            MainWindow.add(lbl_mp);

            lbl_curMp = new Label("50");
            lbl_curMp.Color = Color.White;
            lbl_curMp.Position = new Vector2(90, 480);
            MainWindow.add(lbl_curMp);

            lbl_mpSlash = new Label("/");
            lbl_mpSlash.Color = Color.Brown;
            lbl_mpSlash.Position = new Vector2(140, 480);
            MainWindow.add(lbl_mpSlash);

            lbl_maxMp = new Label("50");
            lbl_maxMp.Color = Color.White;
            lbl_maxMp.Position = new Vector2(160, 480);
            MainWindow.add(lbl_maxMp);

            lbl_moveLeft = new Label("Move Left");
            lbl_moveLeft.Color = Color.Brown;
            lbl_moveLeft.Position = new Vector2(50, 510);
            MainWindow.add(lbl_moveLeft);

            lbl_move = new Label("");
            lbl_move.Color = Color.White;
            lbl_move.Position = new Vector2(150, 510);
            MainWindow.add(lbl_move);

            lbl_enter = new Label("ENTER");
            lbl_enter.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_enter.Position = new Vector2(520, 462);
            MainWindow.add(lbl_enter);

            lbl_enterAction = new Label("Select Unit");
            lbl_enterAction.Color = Color.White;
            lbl_enterAction.Position = new Vector2(600, 462);
            MainWindow.add(lbl_enterAction);

            lbl_v = new Label("V");
            lbl_v.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_v.Position = new Vector2(520, 438);
            MainWindow.add(lbl_v);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Color = Color.White;
            lbl_vAction.Position = new Vector2(550, 438);
            MainWindow.add(lbl_vAction);

            lbl_esc = new Label("ESC");
            lbl_esc.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_esc.Position = new Vector2(520, 486);
            lbl_esc.Visible = false;
            MainWindow.add(lbl_esc);

            lbl_escAction = new Label("Cancel Movement");
            lbl_escAction.Color = Color.White;
            lbl_escAction.Position = new Vector2(570, 486);
            lbl_escAction.Visible = false;
            MainWindow.add(lbl_escAction);

            lbl_e = new Label("E");
            lbl_e.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
            lbl_e.Position = new Vector2(520, 510);
            MainWindow.add(lbl_e);

            lbl_eAction = new Label("End Turn");
            lbl_eAction.Color = Color.White;
            lbl_eAction.Position = new Vector2(550, 510);
            MainWindow.add(lbl_eAction);

            lbl_turnCount = new Label("Turn: " + turnCount + " / 10");
            lbl_turnCount.Color = Color.Gold;
            lbl_turnCount.Position = new Vector2(520, 390);
            MainWindow.add(lbl_turnCount);

            lbl_dmg = new Label("");
            lbl_dmg.Color = Color.Red;
            lbl_dmg.Position = new Vector2(0, 0);
            lbl_dmg.Visible = false;
            MainWindow.add(lbl_dmg);

            lbl_armyTurn = new Label("TO BATTLE, COMRADES!");
            lbl_armyTurn.Font = Content.Graphics.Instance.TurnFont;
            lbl_armyTurn.Position = new Vector2(50, 50);
            MainWindow.add(lbl_armyTurn);

            lbl_battleOutcome = new Label("");
            lbl_battleOutcome.Font = Content.Graphics.Instance.TurnFont;
            lbl_battleOutcome.Position = new Vector2(50, 50);
            lbl_battleOutcome.Visible = false;
            MainWindow.add(lbl_battleOutcome);

            deploy(m, true);
            deploy(e, false);

            cmap.update(map);

            freemode = true;
            actionMode = false;
            targetMode = false;
            spellMode = false;
            allyTurn = true;
            

            changeCurp(null, new EventArgObject(new Point(5, 6)));
            scp = new Point(5, 6);
            endTurnP = new Point(5, 6);

            lastTimeAction = new TimeSpan(0);

            gameOver = false;
            defeat = false;
        }

        public void showCharLabels()
        {
            lbl_name.Visible = true;
            lbl_charName.Text = cmap.get(p.X, p.Y).Name;
            lbl_charName.Visible = true;

            lbl_lvl.Visible = true;
            lbl_charLvl.Text = cmap.get(p.X, p.Y).Lvl.ToString();
            lbl_charLvl.Visible = true;
            lbl_exp.Visible = true;
            lbl_charExp.Text = cmap.get(p.X, p.Y).Exp.ToString();
            lbl_charExp.Visible = true;
            
            lbl_hp.Visible = true;
            lbl_curHp.Text = cmap.get(p.X, p.Y).stats.hp.ToString();
            lbl_curHp.Visible = true;
            lbl_hpSlash.Visible = true;
            lbl_maxHp.Text = cmap.get(p.X, p.Y).stats.maxHp.ToString();
            lbl_maxHp.Visible = true;

            lbl_mp.Visible = true;
            lbl_curMp.Text = cmap.get(p.X, p.Y).stats.mana.ToString();
            lbl_curMp.Visible = true;
            lbl_mpSlash.Visible = true;
            lbl_maxMp.Text = cmap.get(p.X, p.Y).stats.maxMana.ToString();
            lbl_maxMp.Visible = true;
            
            lbl_moveLeft.Visible = true;
            lbl_move.Text = cmap.get(p.X, p.Y).stats.movement.ToString();
            lbl_move.Visible = true;
        }

        public void hideCharLabels()
        {
            lbl_name.Visible = false;
            lbl_charName.Visible = false;

            lbl_lvl.Visible = false;
            lbl_charLvl.Visible = false;
            lbl_exp.Visible = false;
            lbl_charExp.Visible = false;

            lbl_hp.Visible = false;
            lbl_curHp.Visible = false;
            lbl_hpSlash.Visible = false;
            lbl_maxHp.Visible = false;

            lbl_mp.Visible = false;
            lbl_curMp.Visible = false;
            lbl_mpSlash.Visible = false;
            lbl_maxMp.Visible = false;

            lbl_move.Visible = false;
            lbl_moveLeft.Visible = false;
        }

        private void changeCurp(object o, EventArgs e)
        {
            p = (Point)(((EventArgObject)e).o);

            if (cmap.isChar(p.X, p.Y) && freemode)
            {
                showCharLabels();

                lbl_v.Visible = true;
                lbl_vAction.Visible = true;

                if (cmap.get(p.X, p.Y).Organization == "main")
                {
                    lbl_enter.Visible = true;
                    lbl_enterAction.Visible = true;
                }
                else
                {
                    lbl_enter.Visible = false;
                    lbl_enterAction.Visible = false;
                }

                if (cmap.get(p.X, p.Y).stats.movement <= 0)
                {
                    lbl_moved.Visible = true;
                    lbl_enter.Visible = false;
                    lbl_enterAction.Visible = false;
                }
                else
                {
                    lbl_moved.Visible = false;
                    lbl_enter.Visible = true;
                    lbl_enterAction.Visible = true;
                }
            }
            else if (p == scp)
            {
                if(cmap.isChar(scp.X, scp.Y))
                    showCharLabels();
            }
            else
            {
                lbl_v.Visible = false;
                lbl_vAction.Visible = false;

                lbl_enter.Visible = false;
                lbl_enterAction.Visible = false;

                lbl_moved.Visible = false;

                hideCharLabels();
            }
        }

        private void moveChar(Point np)
        {
            if (cmap.get(scp.X, scp.Y).stats.movement <= 0)
                return;

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
            
            cmap.get(scp.X, scp.Y).stats.movement--;
            lbl_move.Text = cmap.get(scp.X, scp.Y).stats.movement.ToString();

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

                if (!cmap.isChar(p.X, p.Y)||cmap.get(p.X, p.Y).Organization!="main"||cmap.get(p.X, p.Y).stats.movement <= 0)
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

                lbl_e.Visible = false;
                lbl_eAction.Visible = false;
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

        private void disableLink(Link l)
        {
            l.Enabled = false;
            l.GEnable=false;
        }

        private void setEnabled()
        {
            Character c = cmap.get(p.X, p.Y);
            bool targetable = false;
            bool castable = false;
            targetableChar = new List<Point>();

            if (c is Fighter || c is Scout)
            {
                if (cmap.isChar(p.X - 1, p.Y) && cmap.get(p.X - 1, p.Y).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 1) && cmap.get(p.X, p.Y - 1).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y) && cmap.get(p.X + 1, p.Y).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 1) && cmap.get(p.X, p.Y + 1).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 1));
                }
            }
            else if (c is Archer)
            {
                if (cmap.isChar(p.X - 1, p.Y + 1) && cmap.get(p.X - 1, p.Y + 1).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y + 1));
                }
                if (cmap.isChar(p.X - 1, p.Y - 1) && cmap.get(p.X - 1, p.Y - 1).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y + 1) && cmap.get(p.X + 1, p.Y + 1).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y + 1));
                }
                if (cmap.isChar(p.X + 1, p.Y - 1) && cmap.get(p.X + 1, p.Y - 1).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y - 1));
                }
                if (cmap.isChar(p.X - 2, p.Y) && cmap.get(p.X - 2, p.Y).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X - 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 2) && cmap.get(p.X, p.Y - 2).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 2));
                }
                if (cmap.isChar(p.X + 2, p.Y) && cmap.get(p.X + 2, p.Y).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X + 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 2) && cmap.get(p.X, p.Y + 2).Organization == "ennemy")
                {
                    targetable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 2));
                }
            }
            else if (c is Healer)
            {
                if (cmap.isChar(p.X - 1, p.Y) && cmap.get(p.X - 1, p.Y).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 1) && cmap.get(p.X, p.Y - 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y) && cmap.get(p.X + 1, p.Y).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 1) && cmap.get(p.X, p.Y + 1).Organization == "main")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 1));
                }
            }
            else if (c is Caster)
            {
                if (cmap.isChar(p.X - 1, p.Y) && cmap.get(p.X - 1, p.Y).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 1) && cmap.get(p.X, p.Y - 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y) && cmap.get(p.X + 1, p.Y).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 1) && cmap.get(p.X, p.Y + 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 1));
                }
                if (cmap.isChar(p.X - 1, p.Y + 1) && cmap.get(p.X - 1, p.Y + 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y + 1));
                }
                if (cmap.isChar(p.X - 1, p.Y - 1) && cmap.get(p.X - 1, p.Y - 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 1, p.Y - 1));
                }
                if (cmap.isChar(p.X + 1, p.Y + 1) && cmap.get(p.X + 1, p.Y + 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y + 1));
                }
                if (cmap.isChar(p.X + 1, p.Y - 1) && cmap.get(p.X + 1, p.Y - 1).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 1, p.Y - 1));
                }
                if (cmap.isChar(p.X - 2, p.Y) && cmap.get(p.X - 2, p.Y).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X - 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y - 2) && cmap.get(p.X, p.Y - 2).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y - 2));
                }
                if (cmap.isChar(p.X + 2, p.Y) && cmap.get(p.X + 2, p.Y).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X + 2, p.Y));
                }
                if (cmap.isChar(p.X, p.Y + 2) && cmap.get(p.X, p.Y + 2).Organization == "ennemy")
                {
                    castable = true;
                    targetableChar.Add(new Point(p.X, p.Y + 2));
                }
            }

            Link lnk_att = menu_actions.getLink("Attack");
            Link lnk_spell;
            Link lnk_item = menu_actions.getLink("Items");

            if ((lnk_spell = menu_actions.getLink("Spell")) == null)
                lnk_spell = menu_actions.getLink("Heal");

            if (lnk_att != null)
            {
                if (targetable)
                {
                    lnk_att.Enabled = true;
                    lnk_att.GEnable=true;
                }
                else
                {
                    lnk_att.Enabled = false;
                    lnk_att.GEnable=false;
                }
            }

            if (lnk_spell != null)
            {
                if (castable)
                {
                    lnk_spell.Enabled = true;
                    lnk_spell.GEnable=true;
                }
                else
                {
                    lnk_spell.Enabled = false;
                    lnk_spell.GEnable=false;
                }
            }

            if (lnk_item != null)
            {
                lnk_item.Enabled = false;
                lnk_item.GEnable=false;
            }
        }

        private Boolean turn(GameTime gameTime)
        {
            Boolean dun = false;

            foreach (String str in cmap.getAllOrg())
                if (str != "main")
                    dun = AI.battle(cmap, tm, str, map, ally, enemy, lbl_dmg, ref gameOver, ref defeat);

            lastTimeAction = gameTime.TotalGameTime;
            cmap.update(map);

            if (gameOver)
            {
                lbl_battleOutcome.Text = "A HERO HAS FALLEN...";
                lbl_battleOutcome.Color = Color.Red;
                lbl_battleOutcome.Visible = true;
                return dun;
            }
            else if (defeat)
            {
                lbl_battleOutcome.Text = "DEFEAT";
                lbl_battleOutcome.Color = Color.Red;
                lbl_battleOutcome.Visible = true;
                return dun;
            }

            if (!dun)
                return dun;

            cmap.resetAllMovement("main");

            if (cmap.isChar(p.X, p.Y))
            {
                showCharLabels();
                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
            }
            else
            {
                hideCharLabels();
                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
            }

            return dun;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (lbl_battleOutcome.Visible)
            {
                if (lastTimeAction + battleOutcomeDuration < gameTime.TotalGameTime)
                {
                    if(gameOver)
                        StateManager.Instance.goForward(new GameOver());
                    else
                        StateManager.Instance.goBack();
                }
                return;
            }

            if (lastTimeAction == new TimeSpan(0))
            {
                lastTimeAction = gameTime.TotalGameTime;
                return;
            }

            if (lbl_armyTurn.Visible)
            {
                if (lastTimeAction + intervalBetweenAction < gameTime.TotalGameTime)
                    lbl_armyTurn.Visible = false;
                return;
            }

            if (!allyTurn)
            {
                displayPlayerTurn = true;
                if (lastTimeAction + intervalBetweenAction < gameTime.TotalGameTime)
                    allyTurn = turn(gameTime);
                return;
            }
            else if (displayPlayerTurn)
            {
                displayPlayerTurn = false;
                
                lbl_moved.Visible = false;

                if (turnCount >= 10)
                {
                    lbl_battleOutcome.Text = "BATTLE END";
                    lbl_battleOutcome.Visible = true;
                    lastTimeAction = gameTime.TotalGameTime;
                    return;
                }
                else
                {
                    turnCount++;
                    lbl_turnCount.Text = "Turn: " + turnCount + " / 10";
                }

                map.CurLs.Clear();
                lbl_dmg.Visible = false;

                map.changeCursor(endTurnP);

                lbl_armyTurn.Text = "YOUR TURN";
                lbl_armyTurn.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
                lbl_armyTurn.Visible = true;

                MainWindow.InputEnabled = true;

                return;
            }

            if (lastTimeAction + intervalBetweenAction < gameTime.TotalGameTime)
                lbl_dmg.Visible = false;

            if (targetMode)
            {
                if (InputHandler.keyReleased(Keys.Down) || InputHandler.keyReleased(Keys.Up))
                {
                    map.CurLs.Clear();
                    foreach (Point c in targetableChar)
                    {
                        map.changeCurp(this, new EventArgObject(c));
                        if (c == targetableChar[menu_actions.Selected])
                        {
                            map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                        }
                        else
                            map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursor);
                    }
                    map.changeCurp(this, new EventArgObject(targetableChar[menu_actions.Selected]));
                    showCharLabels();
                }
                if (InputHandler.keyReleased(Keys.Enter))
                {
                    Character m=cmap.get(scp.X, scp.Y);
                    Character t=cmap.get(p.X, p.Y);

                    String dmg;

                    if (m is Fighter)
                        dmg = ((Fighter)m).attack(t);
                    else if (m is Archer)
                        dmg = ((Archer)m).attack(t);
                    else if (m is Scout)
                        dmg = ((Scout)m).attack(t);
                    else if (m is Healer)
                        dmg = ((Healer)m).heal(t).ToString();
                    else if (m is Caster)
                        dmg = ((Caster)m).attack(t, selectedSpell);
                    else
                        dmg = "Cant"; //missingno

                    lbl_dmg.Text = dmg;
                    lbl_dmg.Position = new Vector2(p.X * 64 - map.getTlc.X * 64, p.Y * 64 - map.getTlc.Y * 64 + 20);
                    lbl_dmg.Visible = true;

                    lastTimeAction = gameTime.TotalGameTime;

                    if (dmg != "miss" || dmg != "Cant")
                    {
                        if (m is Healer)
                            cmap.get(scp.X, scp.Y).gainExp(cmap.get(p.X, p.Y));

                        if (t.stats.hp <= 0)
                        {
                            cmap.get(scp.X, scp.Y).gainExp(cmap.get(p.X, p.Y));

                            enemy.delete(t.Position.X, t.Position.Y);
                            cmap.set(p.X, p.Y, null);
                            cmap.update(map);

                            if (enemy.Characters.Count <= 0)
                            {
                                lbl_battleOutcome.Text = "VICTORY!";
                                lbl_battleOutcome.LabelFun = ColorTheme.LabelColorTheme.LabelFunction.BOLD;
                                lbl_battleOutcome.Visible = true;

                                lastTimeAction = gameTime.TotalGameTime;
                                return;
                            }
                        }
                    }

                    lbl_enemyTurn.Text = dmg;

                    map.changeCurp(this, new EventArgObject(scp));

                    lbl_enterAction.Text = "Select Unit";

                    lbl_escAction.Text = "Cancel Move";
                    lbl_esc.Visible = false;
                    lbl_escAction.Visible = false;

                    freemode = true;
                    map.ArrowEnabled = true;
                    map.Enabled = true;

                    lbl_v.Visible = true;
                    lbl_vAction.Visible = true;

                    lbl_e.Visible = true;
                    lbl_eAction.Visible = true;

                    cmap.get(p.X, p.Y).stats.movement = 0;
                    lbl_move.Text = cmap.get(scp.X, scp.Y).stats.movement.ToString();
                    lbl_moved.Visible = true;

                    map.TabStop = true;
                    map.HasFocus = true;

                    menu_actions.clear();
                    menu_actions.add(new Link("Attack"));
                    menu_actions.add(new Link("Spell"));
                    menu_actions.add(new Link("Items"));
                    menu_actions.add(new Link("Wait"));

                    lbl_actions.Visible = false;
                    menu_actions.Visible = false;
                    menu_actions.Enabled = false;
                    menu_actions.ArrowEnabled = false;
                    menu_actions.HasFocus = false;

                    actionMode = false;
                    targetMode = false;

                    map.CurLs.Clear();
                }
                if (InputHandler.keyReleased(Keys.Escape))
                {
                    hideCharLabels();
                    targetMode = false;

                    menu_actions.clear();
                    menu_actions.add(new Link("Attack"));
                    menu_actions.add(new Link("Spell"));
                    menu_actions.add(new Link("Items"));
                    menu_actions.add(new Link("Wait"));

                    map.changeCurp(this, new EventArgObject(scp));

                    setEnabled();

                    map.CurLs.Clear();
                }
            }
            else if (spellMode)
            {
                if (InputHandler.keyReleased(Keys.Enter))
                {
                    if (!menu_actions.getLink(menu_actions.Selected).Enabled)
                        return;

                    spellMode = false;
                    targetMode = true;

                    Caster cc = (Caster)cmap.get(scp.X, scp.Y);

                    selectedSpell = cc.getCastableSpells().getSpell(menu_actions.SelectedText);

                    targetMode = true;

                    menu_actions.clear();
                    foreach (Point point in targetableChar)
                    {
                        menu_actions.add(new Link(cmap.get(point.X, point.Y).Name));
                    }

                    map.CurLs.Clear();
                    foreach (Point c in targetableChar)
                    {
                        map.changeCurp(this, new EventArgObject(c));
                        if (c == targetableChar[menu_actions.Selected])
                            map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                        else
                            map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursor);
                    }

                    map.changeCurp(this, new EventArgObject(targetableChar[0]));
                    showCharLabels();
                }
            }
            else if (actionMode)
            {
                if (InputHandler.keyReleased(Keys.Enter))
                {
                    if (!menu_actions.getLink(menu_actions.Selected).Enabled)
                        return;

                    if (menu_actions.SelectedText == "Spell")
                    {
                        Caster c = (Caster)cmap.get(scp.X, scp.Y);

                        menu_actions.clear();

                        Link l;

                        foreach (Spell sp in c.getCastableSpells().toList())
                        {
                            l = new Link(sp.Name);

                            if (c.stats.mana < sp.ManaCost)
                                disableLink(l);

                            menu_actions.add(l);
                        }

                        setEnabled();

                        spellMode = true;
                    }
                    else if (menu_actions.SelectedText == "Attack" || menu_actions.SelectedText == "Heal")
                    {
                        targetMode = true;

                        menu_actions.clear();
                        foreach (Point point in targetableChar)
                        {
                            menu_actions.add(new Link(cmap.get(point.X, point.Y).Name));
                        }

                        map.CurLs.Clear();
                        foreach (Point c in targetableChar)
                        {
                            map.changeCurp(this, new EventArgObject(c));
                            if (c == targetableChar[menu_actions.Selected])
                                map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursorRed);
                            else
                                map.CurLs.Add(p, Content.Graphics.Instance.Images.gui.cursor);
                        }

                        map.changeCurp(this, new EventArgObject(targetableChar[0]));
                        showCharLabels();
                    }
                    else
                    {
                        lbl_enterAction.Text = "Select Unit";

                        lbl_escAction.Text = "Cancel Move";
                        lbl_esc.Visible = false;
                        lbl_escAction.Visible = false;

                        freemode = true;
                        map.ArrowEnabled = true;
                        map.Enabled = true;

                        lbl_v.Visible = true;
                        lbl_vAction.Visible = true;

                        lbl_e.Visible = true;
                        lbl_eAction.Visible = true;

                        cmap.get(p.X, p.Y).stats.movement = 0;
                        lbl_move.Text = cmap.get(scp.X, scp.Y).stats.movement.ToString();
                        lbl_moved.Visible = true;

                        map.TabStop = true;
                        map.HasFocus = true;

                        lbl_actions.Visible = false;
                        menu_actions.Visible = false;
                        menu_actions.Enabled = false;
                        menu_actions.ArrowEnabled = false;
                        menu_actions.HasFocus = false;

                        actionMode = false;
                    }
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    lbl_enterAction.Text = "Confirm Move";
                    lbl_escAction.Text = "Cancel Move";

                    map.TabStop = true;
                    map.HasFocus = true;

                    lbl_actions.Visible = false;
                    menu_actions.Visible = false;
                    menu_actions.Enabled = false;
                    menu_actions.ArrowEnabled = false;
                    menu_actions.HasFocus = false;

                    actionMode = false;
                }
            }
            else if (!freemode)
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

                    cmap.get(scp.X, scp.Y).stats.movement++;
                    moveChar(returnP);
                    cmap.get(scp.X, scp.Y).stats.movement = cmap.get(scp.X, scp.Y).stats.traits.spd / 10;
                    lbl_move.Text = cmap.get(scp.X, scp.Y).stats.movement.ToString();

                    freemode = true;
                    map.ArrowEnabled = true;
                    map.Enabled = true;

                    lbl_v.Visible = true;
                    lbl_vAction.Visible = true;

                    lbl_e.Visible = true;
                    lbl_eAction.Visible = true;
                }

                if (InputHandler.keyReleased(Keys.Enter))
                {
                    lbl_enterAction.Text = "Select Action";
                    lbl_escAction.Text = "Cancel Action";

                    map.TabStop = false;
                    map.HasFocus = false;

                    menu_actions.clear();

                    Character c = cmap.get(scp.X, scp.Y);

                    if (!(c is Healer))
                        menu_actions.add(new Link("Attack"));

                    if (c is Healer)
                        menu_actions.add(new Link("Heal"));
                    else
                        menu_actions.add(new Link("Spell"));

                    menu_actions.add(new Link("Items"));
                    menu_actions.add(new Link("Wait"));

                    setEnabled();

                    lbl_actions.Visible = true;
                    menu_actions.Visible = true;
                    menu_actions.Enabled = true;
                    menu_actions.ArrowEnabled = true;
                    menu_actions.HasFocus = true;

                    actionMode = true;
                }
            }
            else
            {
                cmap.update(map);

                if (InputHandler.keyReleased(Keys.V) && lbl_v.Visible)
                {
                    StateManager.Instance.goForward(new CharManage(cmap.get(p.X, p.Y), null));
                }

                if (InputHandler.keyReleased(Keys.E))
                {
                    foreach (Character c in ally.Characters)
                    {
                        c.stats.movement = c.stats.traits.spd / 10;
                    }

                    if (cmap.isChar(p.X, p.Y))
                    {
                        lbl_moved.Visible = false;
                        lbl_move.Text = cmap.get(p.X, p.Y).stats.movement.ToString();
                    }

                    endTurnP = p;

                    allyTurn = false;

                    lbl_armyTurn.Text = "ENEMY TURN";
                    lbl_armyTurn.Color = Color.Red;
                    lbl_armyTurn.Visible = true;

                    MainWindow.InputEnabled = false;

                    lastTimeAction = gameTime.TotalGameTime;
                }
            }
        }
    }
}
