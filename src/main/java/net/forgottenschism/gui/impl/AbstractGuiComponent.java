package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.GuiComponent;
import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Size2d;

public abstract class AbstractGuiComponent implements GuiComponent
{
    private boolean hasFocus;
    private Position2d position;
    private Size2d size;

    @Override
    public boolean hasFocus()
    {
        return hasFocus;
    }

    @Override
    public void setFocus(boolean focus)
    {
        hasFocus = focus;
    }

    @Override
    public Position2d getPosition()
    {
        return position;
    }

    @Override
    public void setPosition(Position2d position)
    {
        this.position = position;
    }

    @Override
    public Size2d getSize()
    {
        return size;
    }

    @Override
    public void setSize(Size2d size)
    {
        this.size = size;
    }
}
