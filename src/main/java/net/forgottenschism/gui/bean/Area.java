package net.forgottenschism.gui.bean;

public class Area
{
	private Position2d position;
	private Size2d size;

	public Area(Position2d position, Size2d size)
	{
		this.position = position;
		this.size = size;
	}

	public boolean contains(Area area)
	{
		Position2d areaPosition = area.getPosition();
		Position2d areaBottomRight = area.getBottomRight();
		Position2d thisBottomRight = getBottomRight();

		return areaPosition.greaterOrEqualThan(position) && areaBottomRight.smallerOrEqualThan(thisBottomRight);
	}

	public boolean contains(Position2d position)
	{
		return position.greaterOrEqualThan(position) && position.smallerOrEqualThan(getBottomRight());
	}

	public boolean overlaps(Area area)
	{
		return contains(area.getPosition()) || contains(area.getBottomRight());
	}

	public Position2d getBottomRight()
	{
		return new Position2d(position.getX()+size.getWidth(), position.getY()+size.getHeight());
	}

	public Position2d getPosition()
	{
		return position;
	}

	public void setPosition(Position2d position)
	{
		this.position = position;
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
