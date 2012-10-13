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
        String regionName;
        City city;

        public Tile()
        {
            type = TileType.PLAIN;
            regionName = "";
        }

        public Tile(TileType t)
        {
            type = t;
            regionName = "";
        }

        public Tile(TileType t, Tilemap fr)
        {
            type = t;
            region = fr;
            regionName = "";
        }

        public Tile(TileType t, String frn)
        {
            type = t;
            region = null;
            regionName = frn;
        }

        public String RegionName
        {
            get
            {
                if (region != null)
                    return region.Name;
                else
                    return regionName;
            }

            set
            {
                if (region == null)
                    regionName = value;
                else
                    region.Name = value;
            }
        }

        public TileType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Tilemap Region
        {
            get
            {
                if (region == null && regionName != "")
                {
                    region = new Tilemap(regionName);
                    return region;
                }
                else
                    return region;
            }
            set { region = value; }
        }

        public City City
        {
            get { return city; }
            set { city = value; }
        }
    }
}
