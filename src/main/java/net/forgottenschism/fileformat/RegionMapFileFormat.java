package net.forgottenschism.fileformat;

import net.forgottenschism.gui.bean.Size2d;

import java.util.LinkedList;
import java.util.List;

public class RegionMapFileFormat
{
	private List<TileFileFormat> tiles;
	private Size2d size;

	public RegionMapFileFormat()
	{
		this(new Size2d(0, 0));
	}

	public RegionMapFileFormat(Size2d size)
	{
		this.size = size;
		tiles = new LinkedList<>();
	}

	public void addTile(TileFileFormat tile)
	{
		tiles.add(tile);
	}

	public List<TileFileFormat> getTiles()
	{
		return tiles;
	}

	public void setTiles(List<TileFileFormat> tiles)
	{
		this.tiles = tiles;
	}

	public Size2d getSize()
	{
		return size;
	}

	public void setSize(Size2d size)
	{
		this.size = size;
	}
}
