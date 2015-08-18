package net.forgottenschism.world;

import net.forgottenschism.gui.bean.Size2d;

public class Tile
{
	public static final Size2d SIZE = new Size2d(100, 100);
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
