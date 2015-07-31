package net.forgottenschism.gui.control;

import net.forgottenschism.engine.GameComponent;
import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Size2d;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.world.Terrain;
import net.forgottenschism.world.Tile;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.HashMap;
import java.util.Map;
import java.util.Random;

public class MapControl extends AbstractControl
{
	private static final Logger logger = LoggerFactory.getLogger(MapControl.class);
	private static final int MAP_SIZE_X = 12;
	private static final int MAP_SIZE_Y = 12;
	private static final int TERRAIN_WIDTH = 100;
	private static final int TERRAIN_HEIGHT = 100;

	private Map<Position2d, Tile> map;

	public MapControl()
	{
		map = new HashMap<>();

		generateMap();
	}

	@Override
	public boolean canHaveFocus()
	{
		return true;
	}

	@Override
	public Size2d getPreferredSize()
	{
		return null;
	}

	private void generateMap()
	{
		Terrain blue = loadTerrain("terrain/blue.png");
		Terrain red = loadTerrain("terrain/red.png");
		Random random = new Random();

		for(int e = 0; e<MAP_SIZE_Y; e++)
			for(int i = 0; i<MAP_SIZE_X; i++)
			{
				if(random.nextBoolean())
					map.put(new Position2d(i, e), new Tile(blue));
				else
					map.put(new Position2d(i, e), new Tile(red));
			}
	}

	private static Terrain loadTerrain(String resourcePath)
	{
		try
		{
			Image image = new Image(resourcePath);

			image.rotate(90);

			return new Terrain(image);
		}
		catch(SlickException exception)
		{
			throw new RuntimeException(exception);
		}
	}

	private static Position2d getPixelPositionFromTilePosition(Position2d tilePosition)
	{
		int pixelPositionX;
		int pixelPositionY;

		pixelPositionX = (int) (tilePosition.getX()*TERRAIN_WIDTH-0.25*TERRAIN_WIDTH*tilePosition.getX());
		pixelPositionY = (int) (tilePosition.getY()*TERRAIN_HEIGHT-0.5*TERRAIN_HEIGHT*(tilePosition.getX()%2));

		return new Position2d(pixelPositionX, pixelPositionY);
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		Tile tile;
		Position2d tilePosition;
		Position2d renderPosition;

		for(int e = 0; e<MAP_SIZE_Y; e++)
			for(int i = 0; i<MAP_SIZE_X; i++)
			{
				tilePosition = new Position2d(i, e);
				tile = map.get(tilePosition);
				renderPosition = getPixelPositionFromTilePosition(tilePosition);

				graphics.drawImage(tile.getTerrain().getImage(), renderPosition.getX(), renderPosition.getY());

				graphics.drawString(""+i+", "+e, renderPosition.getX()+TERRAIN_WIDTH/2,
						renderPosition.getY()+TERRAIN_HEIGHT/2);
			}
	}
}
