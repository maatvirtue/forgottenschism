package net.forgottenschism.gui.control;

import net.forgottenschism.engine.GameAssets;
import net.forgottenschism.gui.bean.Area;
import net.forgottenschism.gui.bean.Direction2d;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.impl.AbstractControl;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import net.forgottenschism.gui.theme.Theme;
import net.forgottenschism.world.Coordinate;
import net.forgottenschism.world.Map;
import net.forgottenschism.world.Terrain;
import net.forgottenschism.world.Tile;

import org.newdawn.slick.*;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class MapControl extends AbstractControl
{
	private static final Logger logger = LoggerFactory.getLogger(MapControl.class);
	private static final Position2d MAP_MINIMAL_OFFSET = new Position2d(Tile.SIZE.getWidth()/2, 0);
	private static final Theme THEME = Theme.getDefaultTheme();
	private static final ColorTheme COLOR_THEME = THEME.getColorTheme();

	private Map map;
	private boolean drawingTileCoordinate;
	private Coordinate cursorCoordinate;
	private Image cursorImage = GameAssets.getInstance().getTileCursor();
	private Position2d currentMapOffset;

	public MapControl()
	{
		drawingTileCoordinate = true;
		map = new Map();
		currentMapOffset = new Position2d(MAP_MINIMAL_OFFSET);
		cursorCoordinate = new Coordinate(2, 1);
	}

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);

		if(cursorCoordinate!=null)
			refreshScrollingPosition();
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

	private int getDistanceFromEdgeToScroll()
	{
		return 100;
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

		scrollIfNeeded();
	}

	private void refreshScrollingPosition()
	{
		scrollIfNeeded();
	}

	private void scrollIfNeeded()
	{
		Area mapControlArea = getArea();
		Position2d cursorPosition = toPixelPositionWithOffset(cursorCoordinate);
		Position2d centerOfCursorTile = new Position2d(cursorPosition);
		centerOfCursorTile.add(new Position2d(Tile.SIZE.getWidth()/2, Tile.SIZE.getHeight()/2));
		Position2d displacementToScroll = new Position2d(0, 0);
		int distanceFromEdge;

		if((distanceFromEdge = mapControlArea.getDistanceFromEdge(centerOfCursorTile, Direction2d.UP))<getDistanceFromEdgeToScroll())
			displacementToScroll.add(0, -1*(distanceFromEdge+getDistanceFromEdgeToScroll()));

		if((distanceFromEdge = mapControlArea.getDistanceFromEdge(centerOfCursorTile, Direction2d.DOWN))<getDistanceFromEdgeToScroll())
			displacementToScroll.add(0, distanceFromEdge+getDistanceFromEdgeToScroll());

		if((distanceFromEdge = mapControlArea.getDistanceFromEdge(centerOfCursorTile, Direction2d.LEFT))<getDistanceFromEdgeToScroll())
			displacementToScroll.add(-1*(distanceFromEdge+getDistanceFromEdgeToScroll()), 0);

		if((distanceFromEdge = mapControlArea.getDistanceFromEdge(centerOfCursorTile, Direction2d.RIGHT))<getDistanceFromEdgeToScroll())
			displacementToScroll.add(distanceFromEdge+getDistanceFromEdgeToScroll(), 0);

		Position2d maxMapOffset = getMaxMapOffset();
		Position2d newMapOffset = new Position2d(currentMapOffset);
		newMapOffset.add(displacementToScroll);

		if(newMapOffset.getX()<MAP_MINIMAL_OFFSET.getX())
			newMapOffset.setX(MAP_MINIMAL_OFFSET.getX());
		else if(newMapOffset.getX()>maxMapOffset.getX())
			newMapOffset.setX(maxMapOffset.getX());

		if(newMapOffset.getY()<MAP_MINIMAL_OFFSET.getY())
			newMapOffset.setY(MAP_MINIMAL_OFFSET.getY());
		else if(newMapOffset.getY()>maxMapOffset.getY())
			newMapOffset.setY(maxMapOffset.getY());

		currentMapOffset = newMapOffset;
	}

	private Position2d getMaxMapOffset()
	{
		Size2d mapSize = map.getSize();
		Size2d mapControlSize = getSize();

		Position2d lastPixel = toPixelPosition(new Coordinate(mapSize.getWidth()-1, mapSize.getWidth()-1));
		lastPixel.add(Tile.SIZE.getWidth(), Tile.SIZE.getHeight());

		Position2d maxMapOffset = new Position2d(lastPixel);
		maxMapOffset.substract(mapControlSize.getWidth(), mapControlSize.getHeight());
		maxMapOffset.substract(Tile.SIZE.getWidth()/2, 0);

		return maxMapOffset;
	}

	@Override
	public Size2d getPreferredSize()
	{
		Coordinate lastTileCoordinate = new Coordinate(map.getSize().getWidth()-1, map.getSize().getHeight()-1);
		Position2d lastTileRenderPosition = toPixelPosition(lastTileCoordinate);
		lastTileRenderPosition.add(MAP_MINIMAL_OFFSET);
		lastTileRenderPosition.add(new Position2d(Tile.SIZE.getWidth(), Tile.SIZE.getHeight()));

		return new Size2d(lastTileRenderPosition.getX(), lastTileRenderPosition.getY());
	}

	private static Position2d toPixelPosition(Coordinate coordinate)
	{
		int pixelPositionX;
		int pixelPositionY;
		int tileWidth = Tile.SIZE.getWidth();
		int tileHeight = Tile.SIZE.getHeight();

		pixelPositionX = (int) (coordinate.getX()*tileWidth-0.25*tileWidth*coordinate.getX());
		pixelPositionY = (int) (coordinate.getY()*tileHeight-0.5*tileHeight*(coordinate.getX()%2));

		return new Position2d(pixelPositionX, pixelPositionY);
	}

	private Position2d toPixelPositionWithOffset(Coordinate coordinate)
	{
		Position2d pixelPosition = toPixelPosition(coordinate);

		pixelPosition.substract(currentMapOffset);

		return pixelPosition;
	}

	@Override
	protected void renderControl(Graphics graphics)
	{
		graphics.setColor(Color.black);
		graphics.fillRect(0, 0, getSize().getWidth(), getSize().getHeight());

		Size2d mapSize = map.getSize();

		for(int e = 0; e<mapSize.getHeight(); e++)
			for(int i = 0; i<mapSize.getWidth(); i++)
				drawTile(graphics, new Coordinate(i, e));

		drawCursor(graphics);
	}

	private void drawTile(Graphics graphics, Coordinate coordinate)
	{
		Tile tile = map.getTile(coordinate);
		Position2d tileRenderPosition = toPixelPositionWithOffset(coordinate);
		Area tileRenderArea = new Area(tileRenderPosition, Tile.SIZE);

		if(isAreaVisible(tileRenderArea))
		{
			drawTerrain(graphics, tile.getTerrain(), tileRenderPosition);

			if(drawingTileCoordinate)
				drawTileCoordinate(graphics, coordinate, tileRenderPosition);
		}
	}

	private void drawTerrain(Graphics graphics, Terrain terrain, Position2d tileRenderPosition)
	{
		graphics.drawImage(terrain.getImage(), tileRenderPosition.getX(), tileRenderPosition.getY());
	}

	private void drawCursor(Graphics graphics)
	{
		Position2d cursorRenderPosition = toPixelPositionWithOffset(cursorCoordinate);
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

	private static void drawTileCoordinate(Graphics graphics, Coordinate tileCoordinate, Position2d tileRenderPosition)
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

		graphics.setFont(THEME.getDefaultFont());
		graphics.setColor(COLOR_THEME.getColor(ColorThemeElement.LABEL_NORMAL));
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
