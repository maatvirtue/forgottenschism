﻿using System;
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
        Map map_unitGrid;

        public UnitManage(Unit selectUnit)
        {
            lbl_unitMng = new Label("Unit Management");
            lbl_unitMng.Color = Color.Gold;
            lbl_unitMng.Position = new Vector2(50, 30);

            map_unitGrid = new Map(new Tilemap(5, 5), 5, 5);
            map_unitGrid.Position = new Vector2(60, 60);

            cm.add(lbl_unitMng);
            cm.add(map_unitGrid);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputHandler.keyReleased(Keys.Escape))
            {
                StateManager.Instance.goBack();
            }
        }
    }
}
