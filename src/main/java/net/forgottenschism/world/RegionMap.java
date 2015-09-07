package net.forgottenschism.world;

import net.forgottenschism.gui.bean.Size2d;

import java.util.HashMap;
import java.util.Map;

public class RegionMap
{
	private static final Terrain DEFAULT_TERRAIN = Terrain.BLUE;

	private Map<Coordinate, Tile> tileMap;
	private Size2d size;

	public RegionMap()
	{
		this(50, 50);
	}

	public RegionMap(int width, int height)
	{
		this(width, height, DEFAULT_TERRAIN);
	}

	public RegionMap(int width, int height, Terrain terrain)
	{
		size = new Size2d(width, height);
		tileMap = new HashMap<>(width*height);

		for(int y = 0; y<height; y++)
			for(int x = 0; x<width; x++)
				tileMap.put(new Coordinate(x, y), new Tile(terrain));
	}

	public Tile getTile(Coordinate coordinate)
	{
		return tileMap.get(coordinate);
	}

	public void putTile(Coordinate coordinate, Tile tile)
	{
		tileMap.put(coordinate, tile);
	}

	public Size2d getSize()
	{
		return size;
	}
}
