package net.forgottenschism.fileformat;

import net.forgottenschism.world.Terrain;

public class TileFileFormat
{
	private int positionX;
	private int positionY;
	private Terrain terrain;

	public TileFileFormat()
	{
		this(0, 0, Terrain.BLUE);
	}

	public TileFileFormat(int positionX, int positionY, Terrain terrain)
	{
		this.positionX = positionX;
		this.positionY = positionY;
		this.terrain = terrain;
	}

	public int getPositionX()
	{
		return positionX;
	}

	public void setPositionX(int positionX)
	{
		this.positionX = positionX;
	}

	public int getPositionY()
	{
		return positionY;
	}

	public void setPositionY(int positionY)
	{
		this.positionY = positionY;
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
