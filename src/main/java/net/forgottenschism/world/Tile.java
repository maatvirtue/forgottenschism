package net.forgottenschism.world;

import net.forgottenschism.gui.Position2d;

public class Tile
{
    private Terrain terrain;

    public Tile(Terrain terrain)
    {
        this.terrain = terrain;
    }

    @Override
    public String toString()
    {
        return "[terrain: "+terrain.toString()+"]";
    }

    public Terrain getTerrain()
    {
        return terrain;
    }

    public void setTerrain(Terrain terrain)
    {
        this.terrain = terrain;
    }
}
