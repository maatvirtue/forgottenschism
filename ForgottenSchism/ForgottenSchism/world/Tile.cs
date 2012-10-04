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
        Tilemap region;
        City city;

        public Tile()
        {
            type = TileType.PLAIN;
        }

        public Tile(TileType t)
        {
            type = t;
        }

        public Tile(TileType t, Tilemap fr)
        {
            type = t;
            region = fr;
        }

        public TileType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Tilemap Region
        {
            get { return region; }
            set { region = value; }
        }

        public City City
        {
            get { return city; }
            set { city = value; }
        }
    }
}
