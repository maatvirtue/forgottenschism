package net.forgottenschism.util;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import net.forgottenschism.exception.ForgottenschismException;
import net.forgottenschism.fileformat.RegionMapFileFormat;
import net.forgottenschism.fileformat.TileFileFormat;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.world.Coordinate;
import net.forgottenschism.world.RegionMap;
import net.forgottenschism.world.Tile;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;

public class MapUtil
{
	public static void save(RegionMap map, File saveFile) throws ForgottenschismException
	{
		Gson gson = new GsonBuilder().setPrettyPrinting().create();

		try
		{
			try(FileWriter fileWriter = new FileWriter(saveFile))
			{
				fileWriter.write(gson.toJson(toFileFormat(map)));
			}
		}
		catch(IOException exception)
		{
			throw new ForgottenschismException("Error saving", exception);
		}
	}

	private static RegionMapFileFormat toFileFormat(RegionMap regionMap)
	{
		Size2d mapSize = regionMap.getSize();
		RegionMapFileFormat regionMapFileFormat = new RegionMapFileFormat(mapSize);
		Coordinate coordinate;
		Tile tile;

		for(int y = 0; y<mapSize.getHeight(); y++)
			for(int x = 0; x<mapSize.getWidth(); x++)
			{
				coordinate = new Coordinate(x, y);
				tile = regionMap.getTile(coordinate);

				regionMapFileFormat.addTile(new TileFileFormat(x, y, tile.getTerrain()));
			}

		return regionMapFileFormat;
	}
}
