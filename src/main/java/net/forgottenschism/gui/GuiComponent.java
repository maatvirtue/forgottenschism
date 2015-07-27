package net.forgottenschism.gui;

import net.forgottenschism.engine.GameComponent;

public interface GuiComponent extends GameComponent, InputCapable
{
    boolean hasFocus();

    void setFocus(boolean focus);

    Position2d getPosition();

    void setPosition(Position2d position);

    Size2d getSize();

    void setSize(Size2d size);
}
