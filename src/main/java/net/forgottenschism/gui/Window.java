package net.forgottenschism.gui;

public interface Window extends GuiComponent
{
    boolean isMainWindow();
    void show();
    void close();
    void addControl(Control control);
    void removeControl(Control control);
}
