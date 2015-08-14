package net.forgottenschism.gui.control;

import net.forgottenschism.engine.GameAssets;
import net.forgottenschism.gui.bean.Area;
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
	private static final Position2d MAP_OFFSET = new Position2d(-(Tile.SIZE.getWidth()/2), 0);

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
		int tileWidth = Tile.SIZE.getWidth();
		int tileHeight = Tile.SIZE.getHeight();

		pixelPositionX = (int) (tilePosition.getX()*tileWidth-0.25*tileWidth*tilePosition.getX());
		pixelPositionY = (int) (tilePosition.getY()*tileHeight-0.5*tileHeight*(tilePosition.getX()%2));

		return new Position2d(pixelPositionX, pixelPositionY);
	}

	private Position2d getPixelPositionFromTilePositionWithOffset(Position2d tilePosition)
	{
		Position2d pixelPosition = getPixelPositionFromTilePosition(tilePosition);

		pixelPosition.add(MAP_OFFSET);

		return pixelPosition;
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		Size2d mapSize = map.getSize();
		Tile tile;
		Position2d tileCoordinate;
		Position2d tileRenderPosition;
		Area tileRenderArea;

		for(int e = 0; e<mapSize.getHeight(); e++)
			for(int i = 0; i<mapSize.getWidth(); i++)
			{
				tileCoordinate = new Position2d(i, e);
				tile = map.getTile(tileCoordinate);
				tileRenderPosition = getPixelPositionFromTilePositionWithOffset(tileCoordinate);
				tileRenderArea = new Area(tileRenderPosition, Tile.SIZE);

				if(isAreaVisible(tileRenderArea))
					drawTile(graphics, tile, tileCoordinate, tileRenderPosition);
			}

		drawCursor(graphics);
	}

	private void drawTile(Graphics graphics, Tile tile, Position2d tileCoordinate, Position2d tileRenderPosition)
	{
		graphics.drawImage(tile.getTerrain().getImage(), tileRenderPosition.getX(), tileRenderPosition.getY());

		if(drawingTileCoordinate)
			drawTileCoordinate(graphics, tileCoordinate, tileRenderPosition);
	}

	private void drawCursor(Graphics graphics)
	{
		Position2d cursorRenderPosition = getPixelPositionFromTilePositionWithOffset(cursorCoordinate);
		Area cursorRenderArea = new Area(cursorRenderPosition, Tile.SIZE);

		if(isAreaVisible(cursorRenderArea))
			graphics.drawImage(cursorImage, cursorRenderPosition.getX(), cursorRenderPosition.getY());
	}

	private boolean isAreaTotalyVisible(Area area)
	{
		return getArea().contains(area);
	}

	private boolean isAreaVisible(Area area)
	{
		return getArea().overlaps(area);
	}

	private static void drawTileCoordinate(Graphics graphics, Position2d tileCoordinate, Position2d tileRenderPosition)
	{
		String coordinateString = Integer.toString(tileCoordinate.getX())+", "+tileCoordinate.getY();
		Font font = graphics.getFont();
		Size2d coordinateStringSize = new Size2d(font.getWidth(coordinateString), font.getHeight(coordinateString));
		Position2d renderPosition = new Position2d();
		int tileWidth = Tile.SIZE.getWidth();
		int tileHeight = Tile.SIZE.getHeight();

		renderPosition.setX((tileWidth-coordinateStringSize.getWidth())/2);
		renderPosition.setY((tileHeight-coordinateStringSize.getHeight())/2);
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
