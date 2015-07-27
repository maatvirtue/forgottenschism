package net.forgottenschism.gui;

public class Size2d
{
    private int width;
    private int height;

    public Size2d()
    {
        this(0, 0);
    }

    public Size2d(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    @Override
    public boolean equals(Object object)
    {
        if(object==null)
            return false;

        if(!(object instanceof Size2d))
            return false;

        Size2d size = (Size2d) object;

        return size.getWidth()==width && size.getHeight()==height;
    }

    @Override
    public int hashCode()
    {
        return (1/2)*(width+height)*(width+height+1)+height;
    }

    @Override
    public String toString()
    {
        return "[width: "+width+", height: "+height+"]";
    }

    public int getWidth()
    {
        return width;
    }

    public void setWidth(int width)
    {
        this.width = width;
    }

    public int getHeight()
    {
        return height;
    }

    public void setHeight(int height)
    {
        this.height = height;
    }
}
