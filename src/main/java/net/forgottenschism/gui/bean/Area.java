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

	public int getDistanceFromEdge(Position2d position, Direction2d direction)
	{
		if(direction==Direction2d.UP)
			return position.getY();
		else if(direction==Direction2d.DOWN)
			return size.getHeight()-position.getY();
		else if(direction==Direction2d.LEFT)
			return position.getX();
		else
			return size.getWidth()-position.getX();
	}

	public boolean contains(Area area)
	{
		Position2d areaPosition = area.getPosition();
		Position2d areaBottomRight = area.getBottomRight();
		Position2d thisBottomRight = getBottomRight();

		return areaPosition.greaterThanOrEqualTo(position) && areaBottomRight.lessThanOrEqualTo(thisBottomRight);
	}

	public boolean contains(Position2d position)
	{
		Position2d areaPosition = this.position;

		return position.greaterThanOrEqualTo(areaPosition) && position.lessThanOrEqualTo(getBottomRight());
	}

	public boolean overlaps(Area area)
	{
		return contains(area.getTopLeft()) || contains(area.getTopRight()) || contains(area.getBottomLeft()) ||
				contains(area.getBottomRight());
	}

	public Position2d getTopLeft()
	{
		return position;
	}

	public Position2d getTopRight()
	{
		return new Position2d(position.getX()+size.getWidth(), position.getY());
	}

	public Position2d getBottomLeft()
	{
		return new Position2d(position.getX(), position.getY()+size.getHeight());
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
