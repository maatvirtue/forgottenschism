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
