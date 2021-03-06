package net.forgottenschism.gui.bean;

/**
 * A pixel position on the GUI.
 */
public class Position2d
{
	private int x;
	private int y;

	public Position2d()
	{
		this(0, 0);
	}

	public Position2d(Position2d position)
	{
		this(position.getX(), position.getY());
	}

	public Position2d(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public void add(Position2d displacement)
	{
		add(displacement.getX(), displacement.getY());
	}

	public void add(int displacementX, int displacementY)
	{
		this.x += displacementX;
		this.y += displacementY;
	}

	public void substract(Position2d displacement)
	{
		substract(displacement.getX(), displacement.getY());
	}

	public void substract(int displacementX, int displacementY)
	{
		this.x -= displacementX;
		this.y -= displacementY;
	}

	/**
	 * A position is greater than another position if its X and Y coordinates are greater.
	 */
	public boolean greaterThan(Position2d position)
	{
		return x>position.getX() && y>position.getY();
	}

	/**
	 * A position is less than another position if its X and Y coordinates are less than the other position's coordinate.
	 */
	public boolean lessThan(Position2d position)
	{
		return x<position.getX() && y<position.getY();
	}

	/**
	 * A position is greater than or equal to another position if its X and Y coordinates are greater than or equal to the
	 * other position's.
	 */
	public boolean greaterThanOrEqualTo(Position2d position)
	{
		return x>=position.getX() && y>=position.getY();
	}

	/**
	 * A position is less than or equal to another position if its X and Y coordinates are less than or equal to the
	 * other position's.
	 */
	public boolean lessThanOrEqualTo(Position2d position)
	{
		return x<=position.getX() && y<=position.getY();
	}

	public void incrementX()
	{
		x++;
	}

	public void incrementY()
	{
		y++;
	}

	public void decrementX()
	{
		x--;
	}

	public void decrementY()
	{
		y--;
	}

	public int getValueByOrientation(Orientation2d orientation)
	{
		if(orientation==Orientation2d.HORIZONTAL)
			return x;
		else
			return y;
	}

	public void setValueByOrientation(Orientation2d orientation, int value)
	{
		if(orientation==Orientation2d.HORIZONTAL)
			x = value;
		else
			y = value;
	}

	@Override
	public boolean equals(Object object)
	{
		if(object==null)
			return false;

		if(!(object instanceof Position2d))
			return false;

		Position2d position = (Position2d) object;

		return position.getX()==x && position.getY()==y;
	}

	@Override
	public int hashCode()
	{
		return (1/2)*(x+y)*(x+y+1)+y;
	}

	@Override
	public String toString()
	{
		return "[x: "+x+", y: "+y+"]";
	}

	public int getX()
	{
		return x;
	}

	public void setX(int x)
	{
		this.x = x;
	}

	public int getY()
	{
		return y;
	}

	public void setY(int y)
	{
		this.y = y;
	}
}
