package net.forgottenschism.world;

/**
 * A coordinate on the map (a specific tile).
 */
public class Coordinate
{
	private int x;
	private int y;

	public Coordinate()
	{
		this(0, 0);
	}

	public Coordinate(int x, int y)
	{
		this.x = x;
		this.y = y;
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

	@Override
	public boolean equals(Object object)
	{
		if(object==null)
			return false;

		if(!(object instanceof Coordinate))
			return false;

		Coordinate coordinate = (Coordinate) object;

		return coordinate.getX()==x && coordinate.getY()==y;
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
