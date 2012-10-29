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

        Label lbl_unitCapacity;
        Label lbl_currentUnit;
        Label lbl_slash;
        Label lbl_maxUnit;

        Label lbl_name;
        Label lbl_charName;
        Label lbl_a;
        Label lbl_aAction;
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

        Boolean selected;

        DialogYN yn_deleteUnit;

        public UnitManage(Army a, int selectedUnit)
        {
            yn_deleteUnit = new DialogYN("Removing this character will also \n remove the unit. Are you sure?");
            yn_deleteUnit.Position = new Vector2(150, 100);
            yn_deleteUnit.Enabled = false;
            yn_deleteUnit.Visible = false;
            yn_deleteUnit.chose = dialog_ret;
            cm.addLastDraw(yn_deleteUnit);

            p = new Point(2, 2);
            sel = new Point(-1, -1);
            selected = false;

            army = a;
            unit = a.Units[selectedUnit];

            charCount = unit.Characters.Count;

            lbl_unitMng = new Label("Unit Management");
            lbl_unitMng.Color = Color.Gold;
            lbl_unitMng.Position = new Vector2(50, 30);

            lbl_unitCapacity = new Label("Unit Capacity:");
            lbl_unitCapacity.Color = Color.Brown;
            lbl_unitCapacity.Position = new Vector2(90, 80);

            lbl_currentUnit = new Label(charCount.ToString());
            lbl_currentUnit.Color = Color.White;
            lbl_currentUnit.Position = new Vector2(220, 80);

            lbl_slash = new Label("/");
            lbl_slash.Color = Color.Brown;
            lbl_slash.Position = new Vector2(235, 80);

            lbl_maxUnit = new Label(MAXCHAR.ToString());
            lbl_maxUnit.Color = Color.White;
            lbl_maxUnit.Position = new Vector2(245, 80);

            lbl_name = new Label("Name");
            lbl_name.Color = Color.Blue;
            lbl_name.Position = new Vector2(50, 400);

            lbl_charName = new Label("");
            lbl_charName.Color = Color.White;
            lbl_charName.Position = new Vector2(115, 400);

            lbl_a = new Label("A");
            lbl_a.Color = Color.Blue;
            lbl_a.Position = new Vector2(400, 430);
            lbl_a.Visible = false;

            lbl_aAction = new Label("Add Character");
            lbl_aAction.Color = Color.White;
            lbl_aAction.Position = new Vector2(430, 430);
            lbl_aAction.Visible = false;

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
            cm.add(lbl_unitCapacity);
            cm.add(lbl_currentUnit);
            cm.add(lbl_slash);
            cm.add(lbl_maxUnit);
            cm.add(lbl_name);
            cm.add(lbl_a);
            cm.add(lbl_aAction);
            cm.add(lbl_enter);
            cm.add(lbl_enterAction);
            cm.add(lbl_r);
            cm.add(lbl_rAction);
            cm.add(lbl_charName);
            cm.add(lbl_v);
            cm.add(lbl_vAction);
            cm.add(map_unitGrid);
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

        public void visible()
        {
            selected = true;
            lbl_name.Visible = true;
            lbl_charName.Text = unit.get(p.X, p.Y).Name;
            lbl_charName.Visible = true;
            lbl_enter.Visible = true;
            lbl_enterAction.Visible = true;
            lbl_r.Visible = true;
            lbl_rAction.Visible = true;
            lbl_v.Visible = true;
            lbl_vAction.Visible = true;

            lbl_a.Visible = false;
            lbl_aAction.Visible = false;
        }

        public void invisible()
        {
            selected = false;
            lbl_name.Visible = false;
            lbl_charName.Visible = false;
            lbl_enter.Visible = false;
            lbl_enterAction.Visible = false;
            lbl_r.Visible = false;
            lbl_rAction.Visible = false;
            lbl_v.Visible = false;
            lbl_vAction.Visible = false;

            if (sel == new Point(-1, -1) && army.Standby.Count > 0 && charCount < MAXCHAR)
            {
                lbl_a.Visible = true;
                lbl_aAction.Visible = true;
            }
        }

        private void changeCurp(object o, EventArgs e)
        {
            p = (Point)(((EventArgObject)e).o);

            if (unit.isChar(p.X, p.Y) && sel == new Point(-1, -1))
            {
                visible();
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

                invisible();
            }
            else if (unit.isChar(p.X, p.Y))
            {
                visible();
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

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
            if (InputHandler.keyReleased(Keys.V) && selected)
            {
                StateManager.Instance.goForward(new CharManage(unit.get(p.X, p.Y)));
            }
            if (InputHandler.keyReleased(Keys.R) && selected)
            {
                if (charCount == 1)
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
                    invisible();
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
                army.Standby.Add(unit.get(p.X, p.Y));
                unit.delete(p.X, p.Y);
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
    }
}
