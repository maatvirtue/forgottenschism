package net.forgottenschism.gui;

import net.forgottenschism.engine.GameComponent;
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

public class MapControl implements GameComponent
{
    private static final Logger logger = LoggerFactory.getLogger(MapControl.class);
    private static final int MAP_SIZE_X = 10;
    private static final int MAP_SIZE_Y = 10;
    private static final int TERRAIN_WIDTH = 100;
    private static final int TERRAIN_HEIGHT = 100;
    private static final int CONTROL_DISPLAY_OFFSET_X = 0;
    private static final int CONTROL_DISPLAY_OFFSET_Y = (int)(-0.25*TERRAIN_HEIGHT);

    private boolean first;
    private Map<Position2d, Tile> map;

    public MapControl()
    {
        map = new HashMap<>();
        first = true;

        generateMap();
    }

    private void generateMap()
    {
        Terrain blue = loadTerrain("terrain/blue.png");
        Terrain red = loadTerrain("terrain/red.png");
        Random random = new Random();

        for(int e=0; e<MAP_SIZE_Y; e++)
            for(int i=0; i<MAP_SIZE_X; i++)
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
            return new Terrain(new Image(resourcePath));
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

        pixelPositionX = (int)(tilePosition.getX()*TERRAIN_WIDTH - 0.5*TERRAIN_HEIGHT*(tilePosition.getY()%2));
        pixelPositionY = (int)(tilePosition.getY()*TERRAIN_HEIGHT - 0.25*TERRAIN_HEIGHT*tilePosition.getY());

        return new Position2d(pixelPositionX, pixelPositionY);
    }

    @Override
    public void update(GameContainer container, int delta)
    {
        //Do nothing
    }

    @Override
    public void render(GameContainer container, Graphics graphics)
    {
        Tile tile;
        Position2d tilePosition;
        Position2d renderPosition;

        for(int e=0; e<MAP_SIZE_Y; e++)
            for(int i=0; i<MAP_SIZE_X; i++)
            {
                tilePosition = new Position2d(i, e);
                tile = map.get(tilePosition);
                renderPosition = getPixelPositionFromTilePosition(tilePosition);

                if(first)
                    System.out.println("tilePosition: " + tilePosition + " renderPosition: " + renderPosition);

                graphics.drawImage(tile.getTerrain().getImage(),
                        CONTROL_DISPLAY_OFFSET_X + renderPosition.getX(),
                        CONTROL_DISPLAY_OFFSET_Y + renderPosition.getY());
            }

        first = false;
    }
}
