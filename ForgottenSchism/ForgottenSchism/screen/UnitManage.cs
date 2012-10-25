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
        Label lbl_unitMng;
        Label lbl_v;
        Label lbl_vAction;
        Map map_unitGrid;

        Unit unit;

        Boolean selected;

        public UnitManage(Unit selectUnit)
        {
            selected = false;

            unit = selectUnit;

            lbl_unitMng = new Label("Unit Management");
            lbl_unitMng.Color = Color.Gold;
            lbl_unitMng.Position = new Vector2(50, 30);

            lbl_v = new Label("V");
            lbl_v.Color = Color.Blue;
            lbl_v.Position = new Vector2(50, 500);

            lbl_vAction = new Label("View Character");
            lbl_vAction.Color = Color.White;
            lbl_vAction.Position = new Vector2(80, 500);

            map_unitGrid = new Map(new Tilemap(5, 5), 5, 5);
            map_unitGrid.Position = new Vector2(60, 60);

            for (int x = 0; x < map_unitGrid.NumX; x++)
            {
                for (int y = 0; y < map_unitGrid.NumY; y++)
                {
                    if (unit.isChar(x, y))
                    {
                        map_unitGrid.CharLs.Add(new Point(x, y), Graphic.getSprite(GameState.CurrentState.mainChar));
                    }
                }
            }

            cm.add(lbl_unitMng);
            cm.add(lbl_v);
            cm.add(lbl_vAction);
            cm.add(map_unitGrid);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (unit.isChar(map_unitGrid.CursorPos.X, map_unitGrid.CursorPos.Y))
            {
                selected = true;
                lbl_v.Visible = true;
                lbl_vAction.Visible = true;
            }
            else
            {
                selected = false;
                lbl_v.Visible = false;
                lbl_vAction.Visible = false;
            }

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
            if (InputHandler.keyReleased(Keys.V) && selected)
            {
                StateManager.Instance.goForward(new CharManage(unit.get(map_unitGrid.CursorPos.X, map_unitGrid.CursorPos.Y)));
            }
        }
    }
}
