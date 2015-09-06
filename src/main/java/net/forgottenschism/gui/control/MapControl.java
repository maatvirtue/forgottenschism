package net.forgottenschism.gui.control;

import net.forgottenschism.engine.GameAssets;
import net.forgottenschism.gui.CoordinateSelectionListener;
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
import net.forgottenschism.world.RegionMap;
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
	private static final int DEFAULT_SELECTION_KEY = Input.KEY_SPACE;

	private RegionMap regionMap;
	private boolean drawingTileCoordinate;
	private Coordinate cursorCoordinate;
	private Image cursorImage = GameAssets.getInstance().getTileCursor();
	private Position2d currentMapOffset;
	private int selectionKey;
	private CoordinateSelectionListener selectionListener;

	public MapControl(RegionMap regionMap)
	{
		drawingTileCoordinate = true;
		selectionKey = DEFAULT_SELECTION_KEY;
		currentMapOffset = new Position2d(MAP_MINIMAL_OFFSET);
		cursorCoordinate = new Coordinate(2, 2);
		setRegionMap(regionMap);
	}

	public void setSelectionListener(CoordinateSelectionListener selectionListener)
	{
		this.selectionListener = selectionListener;
	}

	public void setSelectionKey(int selectionKey)
	{
		this.selectionKey = selectionKey;
	}

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);

		if(cursorCoordinate!=null)
			focus(cursorCoordinate);
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
		if(keyEvent.getKeyCode()==Input.KEY_RIGHT && cursorCoordinate.getX()<regionMap.getSize().getWidth()-1)
			cursorCoordinate.incrementX();
		else if(keyEvent.getKeyCode()==Input.KEY_LEFT && cursorCoordinate.getX()>0)
			cursorCoordinate.decrementX();
		else if(keyEvent.getKeyCode()==Input.KEY_UP && cursorCoordinate.getY()>0)
			cursorCoordinate.decrementY();
		else if(keyEvent.getKeyCode()==Input.KEY_DOWN && cursorCoordinate.getY()<regionMap.getSize().getHeight()-1)
			cursorCoordinate.incrementY();

		focus(cursorCoordinate);

		if(keyEvent.getKeyCode()==selectionKey && selectionListener!=null)
			selectionListener.handleSelect(cursorCoordinate);
	}

	public void setCursorCoordinate(Coordinate cursorCoordinate)
	{
		this.cursorCoordinate = cursorCoordinate;
	}

	public void focus(Coordinate coordinate)
	{
		Position2d tilePosition = toPixelPositionWithOffset(coordinate);
		Position2d centerOfTile = new Position2d(tilePosition);
		centerOfTile.add(new Position2d(Tile.SIZE.getWidth()/2, Tile.SIZE.getHeight()/2));

		focus(centerOfTile);
	}

	private void focus(Position2d position)
	{
		Area mapControlArea = getArea();

		if(mapControlArea.contains(position))
			currentMapOffset = getMinimalScrollMapOffset(position);
		else
			currentMapOffset = getOptimalMapOffset(position);
	}

	private Position2d getOptimalMapOffset(Position2d pointToFocus)
	{
		Size2d mapControlSize = getSize();

		Position2d newMapOffset = new Position2d(pointToFocus);
		newMapOffset.substract(mapControlSize.getWidth()/2, mapControlSize.getHeight()/2);
		newMapOffset.add(currentMapOffset);

		normalizeMapOffset(newMapOffset);

		return newMapOffset;
	}

	private Position2d getMinimalScrollMapOffset(Position2d centerOfCursorTile)
	{
		Position2d displacementToScroll = getMinimalScrollDisplacement(centerOfCursorTile);

		Position2d newMapOffset = new Position2d(currentMapOffset);
		newMapOffset.add(displacementToScroll);

		normalizeMapOffset(newMapOffset);

		return newMapOffset;
	}

	private Position2d getMinimalScrollDisplacement(Position2d centerOfCursorTile)
	{
		Area mapControlArea = getArea();
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

		return displacementToScroll;
	}

	private void normalizeMapOffset(Position2d mapOffset)
	{
		Position2d maxMapOffset = getMaxMapOffset();

		if(mapOffset.getX()<MAP_MINIMAL_OFFSET.getX())
			mapOffset.setX(MAP_MINIMAL_OFFSET.getX());
		else if(mapOffset.getX()>maxMapOffset.getX())
			mapOffset.setX(maxMapOffset.getX());

		if(mapOffset.getY()<MAP_MINIMAL_OFFSET.getY())
			mapOffset.setY(MAP_MINIMAL_OFFSET.getY());
		else if(mapOffset.getY()>maxMapOffset.getY())
			mapOffset.setY(maxMapOffset.getY());
	}

	private Position2d getMaxMapOffset()
	{
		Size2d mapSize = regionMap.getSize();
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
		Coordinate lastTileCoordinate = new Coordinate(regionMap.getSize().getWidth()-1, regionMap.getSize().getHeight()-1);
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

		Size2d mapSize = regionMap.getSize();

		for(int e = 0; e<mapSize.getHeight(); e++)
			for(int i = 0; i<mapSize.getWidth(); i++)
				drawTile(graphics, new Coordinate(i, e));

		drawCursor(graphics);
	}

	private void drawTile(Graphics graphics, Coordinate coordinate)
	{
		Tile tile = regionMap.getTile(coordinate);
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

	public RegionMap getRegionMap()
	{
		return regionMap;
	}

	public void setRegionMap(RegionMap regionMap)
	{
		if(regionMap==null)
			throw new IllegalArgumentException("map cannot be null");

		this.regionMap = regionMap;
	}
}
