using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace ForgottenSchism.world
{
    public class Tile
    {
        public enum TileType {PLAIN, FOREST, MOUNTAIN, WATER, CITY, ROADS};
        
        TileType type;

        public Tile()
        {
            type = TileType.PLAIN;
        }

        public Tile(TileType t)
        {
            type = t;
        }

        public TileType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
