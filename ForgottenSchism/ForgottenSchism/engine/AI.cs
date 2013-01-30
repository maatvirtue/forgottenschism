using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using ForgottenSchism.world;
using ForgottenSchism.screen;
using ForgottenSchism.control;

namespace ForgottenSchism.engine
{
    public class AI
    {
        public enum Steps { FIND, MOVE, TARGET, ACTION };
        Steps currStep;

        UnitMap unitMap;
        Tilemap tileMap;
        String organization;
        Map map;

        bool enabled;

        public AI(UnitMap umap, Tilemap tm, String org, Map m)
        {
            unitMap = umap;
            tileMap = tm;
            organization = org;
            map = m;

            currStep = Steps.FIND;
            enabled = false;
        }

        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }

        public void update(GameTime gameTime)
        {
            if (Enabled)
            {

            }
        }
    }
}
