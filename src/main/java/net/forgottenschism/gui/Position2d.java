package net.forgottenschism.gui;

public class Position2d
{
    private int x;
    private int y;

    public Position2d()
    {
        this(0, 0);
    }

    public Position2d(int x, int y)
    {
        this.x = x;
        this.y = y;
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
