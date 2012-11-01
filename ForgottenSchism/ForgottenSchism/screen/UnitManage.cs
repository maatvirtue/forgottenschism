using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using ForgottenSchism.control;
using ForgottenSchism.engine;
using ForgottenSchism.world;

namespace ForgottenSchism.screen
{
    public class UnitManage : Screen
    {
        const int MAXCHAR = 5;
        int charCount;

        Label lbl_unitMng;

        Label lbl_mainUnit;
        Label lbl_deployed;

        Label lbl_standby;

        Label lbl_unitName;
        Label lbl_unitNameValue;

        Label lbl_unitCapacity;
        Label lbl_currentUnit;
        Label lbl_slash;
        Label lbl_maxUnit;

        Label lbl_leader;

        Label lbl_name;
        Label lbl_charName;
        Label lbl_class;
        Label lbl_charClass;
        Label lbl_a;
        Label lbl_aAction;
        Label lbl_l;
        Label lbl_lAction;
        Label lbl_n;
        Label lbl_nAction;
        Label lbl_r;
        Label lbl_rAction;
        Label lbl_v;
        Label lbl_vAction;
        Label lbl_enter;
        Label lbl_enterAction;
        Map map_unitGrid;

        Point p;
        Point sel;

        Army army;
        Unit unit;

        Character selectedUnit;
        Point selectedPos;

        Boolean selected;
        Boolean adding;

        DialogYN yn_deleteUnit;

        DialogTxt txt_renameUnit;

        Menu menu_standby;

        public UnitManage(Army a, int selectedUnit)
        {
            yn_deleteUnit = new DialogYN("Removing this character will also \n remove the unit. Are you sure?");
            yn_deleteUnit.Position = new Vector2(150, 100);
            yn_deleteUnit.Enabled = false;
            yn_deleteUnit.Visible = false;
            yn_deleteUnit.chose = dialog_ret;
            cm.addLastDraw(yn_deleteUnit);

            txt_renameUnit = new DialogTxt("Rename unit: ");
            txt_renameUnit.Position = new Vector2(150, 100);
            txt_renameUnit.Enabled = false;
            txt_renameUnit.Visible = false;
            txt_renameUnit.complete = dialog_complete;
            cm.addLastDraw(txt_renameUnit);

            p = new Point(2, 2);
            sel = new Point(-1, -1);
            selected = false;
            adding = false;

            army = a;
            unit = a.Units[selectedUnit];

            lbl_standby = new Label("Standby Units");
            lbl_standby.Color = Color.Gold;
            lbl_standby.Position = new Vector2(400, 110);

            menu_standby = new Menu(9);
            menu_standby.Position = new Vector2(400, 110);

            updateMenu();

            charCount = unit.Characters.Count;

            lbl_mainUnit = new Label("MAIN UNIT");
            lbl_mainUnit.Color = Color.Gold;
            lbl_mainUnit.Position = new Vector2(90, 55);
            if (unit.isMainUnit())
                lbl_mainUnit.Visible = true;
            else
                lbl_mainUnit.Visible = false;

            lbl_deployed = new Label("DEPLOYED");
            lbl_deployed.Color = Color.Gold;
            lbl_deployed.Position = new Vector2(400, 55);
            if (unit.Deployed)
                lbl_deployed.Visible = true;
            else
                lbl_deployed.Visible = false;

            lbl_unitMng = new Label("Unit Management");
            lbl_unitMng.Color = Color.Gold;
            lbl_unitMng.Position = new Vector2(50, 30);

            lbl_unitCapacity = new Label("Unit Capacity:");
            lbl_unitCapacity.Color = Color.Brown;
            lbl_unitCapacity.Position = new Vector2(400, 80);

            lbl_currentUnit = new Label(charCount.ToString());
            lbl_currentUnit.Color = Color.White;
            lbl_currentUnit.Position = new Vector2(530, 80);

            lbl_slash = new Label("/");
            lbl_slash.Color = Color.Brown;
            lbl_slash.Position = new Vector2(545, 80);

            lbl_maxUnit = new Label(MAXCHAR.ToString());
            lbl_maxUnit.Color = Color.White;
            lbl_maxUnit.Position = new Vector2(555, 80);

            lbl_unitName = new Label("Unit Name:");
            lbl_unitName.Color = Color.Brown;
            lbl_unitName.Position = new Vector2(90, 80);

            lbl_unitNameValue = new Label(unit.Name);
            lbl_unitNameValue.Color = Color.White;
            lbl_unitNameValue.Position = new Vector2(195, 80);

            lbl_leader = new Label("LEADER");
            lbl_leader.Color = Color.Gold;
            lbl_leader.Position = new Vector2(50, 370);

            lbl_name = new Label("Name");
            lbl_name.Color = Color.Brown;
            lbl_name.Position = new Vector2(50, 400);

            lbl_charName = new Label("");
            lbl_charName.Color = Color.White;
            lbl_charName.Position = new Vector2(115, 400);

            lbl_class = new Label("Class");
            lbl_class.Color = Color.Brown;
            lbl_class.Position = new Vector2(400, 400);

            lbl_charClass = new Label("");
            lbl_charClass.Color = Color.White;
            lbl_charClass.Position = new Vector2(460, 400);

            lbl_a = new Label("A");
            lbl_a.Color = Color.Blue;
            lbl_a.Position = new Vector2(400, 430);
            lbl_a.Visible = false;

            lbl_aAction = new Label("Add Character");
            lbl_aAction.Color = Color.White;
            lbl_aAction.Position = new Vector2(430, 430);
            lbl_aAction.Visible = false;

            lbl_l = new Label("L");
            lbl_l.Color = Color.Blue;
            lbl_l.Position = new Vector2(400, 460);
            lbl_l.Visible = false;

            lbl_lAction = new Label("Make Leader");
            lbl_lAction.Color = Color.White;
            lbl_lAction.Position = new Vector2(430, 460);
            lbl_lAction.Visible = false;

            lbl_n = new Label("N");
            lbl_n.Color = Color.Blue;
            lbl_n.Position = new Vector2(400, 490);

            lbl_nAction = new Label("Rename Unit");
            lbl_nAction.Color = Color.White;
            lbl_nAction.Position = new Vector2(430, 490);

            lbl_enter = new Label("ENTER");
            lbl_enter.Color = Color.Blue;
            lbl_enter.Position = new Vector2(50, 430);

            lbl_enterAction = new Label("Move Character");
            lbl_enterAction.Color = Color.White;
            lbl_enterAction.Position = new Vector2(130, 430);

            lbl_r = new Label("R");
            lbl_r.Color = Color.Blue;
            lbl_r.Position = new Vector2(50, 460);

            lbl_rAction = new Label("Remove Character");
            lbl_rAction.Color = Color.White;
            lbl_rAction.Position = new Vector2(80, 460);

            lbl_v = new Label("V");
            lbl_v.Color = Color.Blue;
            lbl_v.Position = new Vector2(50, 490);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Color = Color.White;
            lbl_vAction.Position = new Vector2(80, 490);

            map_unitGrid = new Map(new Tilemap(4, 4), 4, 4);
            map_unitGrid.Position = new Vector2(90, 110);

            map_unitGrid.changeCurp = changeCurp;
            map_unitGrid.curSelection = curSelection;

            updateGrid();

            if (unit.isChar(2, 2))
            {
                visible();
            }
            else
            {
                invisible();
            }

            cm.add(lbl_unitMng);
            cm.add(lbl_standby);
            cm.add(lbl_mainUnit);
            cm.add(lbl_deployed);
            cm.add(lbl_unitCapacity);
            cm.add(lbl_currentUnit);
            cm.add(lbl_slash);
            cm.add(lbl_maxUnit);
            cm.add(lbl_unitName);
            cm.add(lbl_unitNameValue);
            cm.add(lbl_leader);
            cm.add(lbl_name);
            cm.add(lbl_charName);
            cm.add(lbl_class);
            cm.add(lbl_charClass);
            cm.add(lbl_a);
            cm.add(lbl_aAction);
            cm.add(lbl_l);
            cm.add(lbl_lAction);
            cm.add(lbl_n);
            cm.add(lbl_nAction);
            cm.add(lbl_enter);
            cm.add(lbl_enterAction);
            cm.add(lbl_r);
            cm.add(lbl_rAction);
            cm.add(lbl_v);
            cm.add(lbl_vAction);
            cm.add(map_unitGrid);
            cm.add(menu_standby);
        }

        public void updateGrid()
        {
            map_unitGrid.CharLs.Clear();
            for (int x = 0; x < map_unitGrid.NumX; x++)
            {
                for (int y = 0; y < map_unitGrid.NumY; y++)
                {
                    if (unit.isChar(x, y))
                    {
                        map_unitGrid.CharLs.Add(new Point(x, y), Graphic.getSprite(unit.get(x, y)));
                    }
                }
            }
        }

        public void updateMenu()
        {
            menu_standby.clear();
            foreach (Character c in army.Standby)
            {
                menu_standby.add(new Link(c.Name));
            }
            menu_standby.Enabled = false;
            menu_standby.unfocusLink();
        }

        public void visible()
        {
            if (unit.Deployed)
            {
                lbl_r.Visible = false;
                lbl_rAction.Visible = false;

                lbl_l.Visible = false;
                lbl_lAction.Visible = false;

                if (unit.isLeader(p.X, p.Y))
                {
                    lbl_leader.Visible = true;
                }
                else
                {
                    lbl_leader.Visible = false;
                }
            }
            else
            {
                lbl_r.Visible = true;
                lbl_rAction.Visible = true;

                if (unit.isLeader(p.X, p.Y))
                {
                    lbl_l.Visible = false;
                    lbl_lAction.Visible = false;
                    lbl_leader.Visible = true;
                }
                else
                {
                    lbl_l.Visible = true;
                    lbl_lAction.Visible = true;
                    lbl_leader.Visible = false;
                }
            }
            selected = true;
            lbl_name.Visible = true;
            lbl_charName.Text = unit.get(p.X, p.Y).Name;
            lbl_charName.Visible = true;
            lbl_class.Visible = true;
            lbl_charClass.Text = unit.get(p.X, p.Y).Type.ToString();
            lbl_charClass.Visible = true;
            lbl_enter.Visible = true;
            lbl_enterAction.Text = "Move Character";
            lbl_enterAction.Visible = true;
            lbl_v.Visible = true;
            lbl_vAction.Visible = true;

            lbl_a.Visible = false;
            lbl_aAction.Visible = false;

            lbl_n.Visible = true;
            lbl_nAction.Visible = true;
        }

        public void invisible()
        {
            selected = false;
            lbl_name.Visible = false;
            lbl_charName.Visible = false;
            lbl_class.Visible = false;
            lbl_charClass.Visible = false;
            lbl_enter.Visible = false;
            lbl_enterAction.Visible = false;
            lbl_r.Visible = false;
            lbl_rAction.Visible = false;
            lbl_v.Visible = false;
            lbl_vAction.Visible = false;
            lbl_l.Visible = false;
            lbl_lAction.Visible = false;
            lbl_leader.Visible = false;

            if (sel == new Point(-1, -1) && army.Standby.Count > 0 && charCount < MAXCHAR && unit.Deployed == false)
            {
                lbl_a.Visible = true;
                lbl_aAction.Visible = true;
            }

            if (adding)
            {
                lbl_a.Visible = false;
                lbl_aAction.Visible = false;

                lbl_enter.Visible = true;
                lbl_enterAction.Text = "Add Character";
                lbl_enterAction.Visible = true;

                lbl_n.Visible = false;
                lbl_nAction.Visible = false;
            }
            else
            {
                lbl_n.Visible = true;
                lbl_nAction.Visible = true;
            }
        }

        private void changeCurp(object o, EventArgs e)
        {
            p = (Point)(((EventArgObject)e).o);

            if (unit.isChar(p.X, p.Y) && sel == new Point(-1, -1))
            {
                visible();
            }
            else if(unit.isChar(p.X, p.Y))
            {
                lbl_enter.Visible = true;
                lbl_enterAction.Visible = true;
            }
            else
            {
                invisible();
            }
        }

        private void curSelection(object o, EventArgs e)
        {
            sel = (Point)(((EventArgObject)e).o);

            if (sel != new Point(-1, -1))
            {
                if (unit.isChar(p.X, p.Y))
                {
                    selectedUnit = unit.get(p.X, p.Y);
                }
                else
                {
                    selectedUnit = null;
                }

                selectedPos = sel;
                invisible();
            }
            else if (unit.isChar(p.X, p.Y))
            {
                unit.set(selectedPos.X, selectedPos.Y, unit.get(p.X, p.Y));

                if (selectedUnit == null)
                {
                    unit.delete(p.X, p.Y);
                    invisible();
                }
                else
                {
                    unit.set(p.X, p.Y, selectedUnit);
                    selectedUnit = null;
                    visible();
                }
                selectedPos = new Point(-1, -1);
                updateGrid();
            }
            else if (selectedUnit != null)
            {
                unit.set(p.X, p.Y, selectedUnit);
                unit.delete(map_unitGrid.LastSelection.X, map_unitGrid.LastSelection.Y);
                selectedUnit = null;
                updateGrid();

                visible();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (yn_deleteUnit.Enabled)
                yn_deleteUnit.HandleInput(gameTime);
            else if (txt_renameUnit.Enabled)
                txt_renameUnit.HandleInput(gameTime);
            else
            {
                if (adding)
                {
                    menu_standby.Enabled = true;
                    map_unitGrid.Enabled = false;
                    menu_standby.HasFocus = true;
                    map_unitGrid.HasFocus = false;
                    menu_standby.refocusLink();
                }
                else
                {
                    menu_standby.Enabled = false;
                    map_unitGrid.Enabled = true;
                    menu_standby.HasFocus = false;
                    map_unitGrid.HasFocus = true;
                    menu_standby.unfocusLink();
                }

                if (InputHandler.keyReleased(Keys.Escape))
                {
                    if (adding)
                    {
                        adding = false;
                        invisible();
                    }
                    else
                        StateManager.Instance.goBack();
                }
                if (InputHandler.keyReleased(Keys.V) && selected)
                {
                    StateManager.Instance.goForward(new CharManage(unit.get(p.X, p.Y)));
                }
                if (InputHandler.keyReleased(Keys.R) && lbl_r.Visible)
                {
                    if (charCount == 1 || unit.isLeader(p.X, p.Y))
                    {
                        dialog_show(null, null);
                    }
                    else
                    {
                        army.Standby.Add(unit.get(p.X, p.Y));
                        unit.delete(p.X, p.Y);
                        charCount--;
                        lbl_currentUnit.Text = charCount.ToString();
                        updateGrid();
                        updateMenu();
                        invisible();
                    }
                }
                if (InputHandler.keyReleased(Keys.L) && selected)
                {
                    if (lbl_l.Visible)
                    {
                        unit.setLeader(p.X, p.Y);
                        visible();
                    }
                }
                if (InputHandler.keyReleased(Keys.A) && lbl_a.Visible)
                {
                    adding = true;
                    invisible();
                }
                if (InputHandler.keyReleased(Keys.Enter) && adding)
                {
                    unit.set(p.X, p.Y, army.Standby[menu_standby.Selected]);
                    army.Standby.Remove(army.Standby[menu_standby.Selected]);
                    updateMenu();
                    updateGrid();

                    charCount++;
                    lbl_currentUnit.Text = charCount.ToString();

                    adding = false;
                    menu_standby.Enabled = false;
                    map_unitGrid.Enabled = true;

                    visible();
                }
                if (InputHandler.keyReleased(Keys.N) && lbl_n.Visible)
                {
                    dialog_showTxt(null, null);
                }
            }
        }

        private void dialog_show(object sender, EventArgs e)
        {
            InputHandler.flush();
            yn_deleteUnit.Enabled = true;
            yn_deleteUnit.Visible = true;
            cm.Enabled = false;
        }

        private void dialog_ret(object sender, EventArgs e)
        {
            if ((bool)((EventArgObject)e).o)
            {
                foreach (Character c in unit.Characters)
                {
                    army.Standby.Add(c);
                }
                army.Units.Remove(unit);

                StateManager.Instance.goBack();
            }
            else
            {
                InputHandler.flush();
                yn_deleteUnit.Visible = false;
                yn_deleteUnit.Enabled = false;
                cm.Enabled = true;
            }
        }

        private void dialog_complete(object sender, EventArgs e)
        {
            String s = ((EventArgObject)e).o.ToString();
            if (s != String.Empty)
            {
                unit.Name = s;
                lbl_unitNameValue.Text = s;
            }

            InputHandler.flush();
            txt_renameUnit.Enabled = false;
            txt_renameUnit.Visible = false;
            cm.Enabled = true;
        }

        private void dialog_showTxt(object sender, EventArgs e)
        {
            InputHandler.flush();
            txt_renameUnit.Enabled = true;
            txt_renameUnit.Visible = true;
            cm.Enabled = false;

            
        }
    }
}
