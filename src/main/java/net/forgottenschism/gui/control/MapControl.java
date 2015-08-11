package net.forgottenschism.gui.control;

import net.forgottenschism.engine.GameAssets;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.world.Map;
import net.forgottenschism.world.Terrain;
import net.forgottenschism.world.Tile;

import org.newdawn.slick.Font;
import org.newdawn.slick.Graphics;

import org.newdawn.slick.Image;
import org.newdawn.slick.Input;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class MapControl extends AbstractControl
{
	private static final Logger logger = LoggerFactory.getLogger(MapControl.class);

	private Map map;
	private boolean drawingTileCoordinate;
	private Position2d cursorCoordinate;
	private Image cursorImage = GameAssets.getInstance().getTileCursor();

	public MapControl()
	{
		drawingTileCoordinate = true;
		map = new Map();
		cursorCoordinate = new Position2d(2, 1);
	}

	@Override
	public boolean isFocusable()
	{
		return true;
	}

	@Override
	public int getForwardFocusTraversalKey()
	{
		return Input.KEY_TAB;
	}

	@Override
	public int getBackwardFocusTraversalKey()
	{
		return Input.KEY_BACK;
	}

	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		if(keyEvent.getKeyCode()==Input.KEY_RIGHT && cursorCoordinate.getX()<map.getSize().getWidth()-1)
			cursorCoordinate.incrementX();
		else if(keyEvent.getKeyCode()==Input.KEY_LEFT && cursorCoordinate.getX()>0)
			cursorCoordinate.decrementX();
		else if(keyEvent.getKeyCode()==Input.KEY_UP && cursorCoordinate.getY()>0)
			cursorCoordinate.decrementY();
		else if(keyEvent.getKeyCode()==Input.KEY_DOWN && cursorCoordinate.getY()<map.getSize().getHeight()-1)
			cursorCoordinate.incrementY();
	}

	@Override
	public Size2d getPreferredSize()
	{
		return null;
	}

	private static Position2d getPixelPositionFromTilePosition(Position2d tilePosition)
	{
		int pixelPositionX;
		int pixelPositionY;
		int terrainWidth = Terrain.PIXEL_SIZE.getWidth();
		int terrainHeight = Terrain.PIXEL_SIZE.getHeight();

		pixelPositionX = (int) (tilePosition.getX()*terrainWidth-0.25*terrainWidth*tilePosition.getX());
		pixelPositionY = (int) (tilePosition.getY()*terrainHeight-0.5*terrainHeight*(tilePosition.getX()%2));

		return new Position2d(pixelPositionX, pixelPositionY);
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		Size2d mapSize = map.getSize();
		Tile tile;
		Position2d tileCoordinate;
		Position2d tileRenderPosition;
		Position2d mapOffset = new Position2d(-(Terrain.PIXEL_SIZE.getWidth()/2), 0);

		for(int e = 0; e<mapSize.getHeight(); e++)
			for(int i = 0; i<mapSize.getWidth(); i++)
			{
				tileCoordinate = new Position2d(i, e);
				tile = map.getTile(tileCoordinate);
				tileRenderPosition = getPixelPositionFromTilePosition(tileCoordinate);
				tileRenderPosition.add(mapOffset);

				if(isRenderPositionVisible(tileRenderPosition))
					drawTile(graphics, tile, tileCoordinate, tileRenderPosition);
			}
	}

	private void drawTile(Graphics graphics, Tile tile, Position2d tileCoordinate, Position2d tileRenderPosition)
	{
		graphics.drawImage(tile.getTerrain().getImage(), tileRenderPosition.getX(), tileRenderPosition.getY());

		if(drawingTileCoordinate)
			drawTileCoordinate(graphics, tileCoordinate, tileRenderPosition);

		if(tileCoordinate.equals(cursorCoordinate))
			drawCursor(graphics, tileRenderPosition);
	}

	private void drawCursor(Graphics graphics, Position2d tileRenderPosition)
	{
		graphics.drawImage(cursorImage, tileRenderPosition.getX(), tileRenderPosition.getY());
	}

	private boolean isRenderPositionVisible(Position2d position)
	{
		Size2d controlSize = getSize();

		return position.getX()<=controlSize.getWidth() && position.getY()<=controlSize.getHeight();
	}

	private static void drawTileCoordinate(Graphics graphics, Position2d tileCoordinate, Position2d tileRenderPosition)
	{
		String coordinateString = Integer.toString(tileCoordinate.getX())+", "+tileCoordinate.getY();
		Font font = graphics.getFont();
		Size2d coordinateStringSize = new Size2d(font.getWidth(coordinateString), font.getHeight(coordinateString));
		Position2d renderPosition = new Position2d();
		int terrainWidth = Terrain.PIXEL_SIZE.getWidth();
		int terrainHeight = Terrain.PIXEL_SIZE.getHeight();

		renderPosition.setX((terrainWidth-coordinateStringSize.getWidth())/2);
		renderPosition.setY((terrainHeight-coordinateStringSize.getHeight())/2);
		renderPosition.add(tileRenderPosition);

		graphics.drawString(coordinateString, renderPosition.getX(), renderPosition.getY());
	}

	public boolean isDrawingTileCoordinate()
	{
		return drawingTileCoordinate;
	}

	public void setDrawingTileCoordinate(boolean drawingTileCoordinate)
	{
		this.drawingTileCoordinate = drawingTileCoordinate;
	}
}
