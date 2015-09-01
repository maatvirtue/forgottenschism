package net.forgottenschism.world;

import net.forgottenschism.gui.bean.Size2d;

import java.util.HashMap;
import java.util.Random;

public class Map
{
	private java.util.Map<Coordinate, Tile> tileMap;
	private Size2d size;

	public Map()
	{
		this(50, 50);
	}

	public Map(int width, int height)
	{
		size = new Size2d(width, height);
		tileMap = new HashMap<>(width*height);

		generateMap(size.getWidth(), size.getHeight());
	}

	public Tile getTile(Coordinate coordinate)
	{
		return tileMap.get(coordinate);
	}

	private void generateMap(int width, int height)
	{
		Random random = new Random();

		for(int e = 0; e<width; e++)
			for(int i = 0; i<height; i++)
			{
				if(random.nextBoolean())
					tileMap.put(new Coordinate(i, e), new Tile(Terrain.BLUE));
				else
					tileMap.put(new Coordinate(i, e), new Tile(Terrain.RED));
			}
	}

	public Size2d getSize()
	{
		return size;
	}
}
