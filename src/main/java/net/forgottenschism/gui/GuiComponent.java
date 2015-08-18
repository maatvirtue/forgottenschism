package net.forgottenschism.gui;

import net.forgottenschism.engine.GameComponent;
import net.forgottenschism.gui.bean.Area;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;

public interface GuiComponent extends GameComponent, InputCapable
{
    boolean hasFocus();

    void setFocus(boolean focus);

    Position2d getPosition();

    void setPosition(Position2d position);

    Size2d getSize();

    void setSize(Size2d size);

    Area getArea();
}
