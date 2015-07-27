package net.forgottenschism.gui;

import org.newdawn.slick.Color;

public interface Window extends GuiComponent
{
    boolean isMainWindow();

    void show();

    void close();

    void addControl(Control control);

    void removeControl(Control control);

    Color getBackgroundColor();

    void setBackgroundColor(Color backgroundColor);
}
