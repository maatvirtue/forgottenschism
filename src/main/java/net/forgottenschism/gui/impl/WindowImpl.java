package net.forgottenschism.gui.impl;

import net.forgottenschism.engine.GraphicsGenerator;
import net.forgottenschism.gui.*;
import org.newdawn.slick.Color;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Image;

public class WindowImpl extends AbstractGuiComponent implements Window
{
    private static final Position2d DEFAULT_POSITION = new Position2d(10, 10);
    private static final Size2d DEFAULT_SIZE = new Size2d(100, 100);

    private FocusManager focusManager;
    private Screen parentScreen;
    private boolean hasFocus;
    private boolean isMainWindow;
    private Image background;

    public WindowImpl(Screen parentScreen)
    {
        this(parentScreen, false);
    }

    public WindowImpl(Screen parentScreen, boolean isMainWindow)
    {
        this.parentScreen = parentScreen;
        this.isMainWindow = isMainWindow;
        hasFocus = false;

        if(isMainWindow)
        {
            setPosition(new Position2d(0, 0));
            setSize(parentScreen.getScreenSize());
        }
        else
        {
            setPosition(DEFAULT_POSITION);
            setSize(DEFAULT_SIZE);
        }

        focusManager = new FocusManagerImpl();
    }

    @Override
    public void setSize(Size2d size)
    {
        super.setSize(size);

        refreshBackground();
    }

    private void refreshBackground()
    {
        Size2d size = getSize();

        background = GraphicsGenerator.getSolidColorImage(size.getWidth(), size.getHeight(), Color.black);
    }

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
    public boolean isMainWindow()
    {
        return isMainWindow;
    }

    @Override
    public void show()
    {
        if(!isMainWindow)
            parentScreen.displayNewWindow(this);
    }

    @Override
    public void close()
    {
        if(!isMainWindow)
            parentScreen.closeWindow(this);
    }

    @Override
    public void addControl(Control control)
    {
        focusManager.addControl(control);
    }

    @Override
    public void removeControl(Control control)
    {
        focusManager.removeControl(control);
    }

    @Override
    public void keyReleased(int key, char character)
    {
        focusManager.keyReleased(key, character);
    }

    @Override
    public void keyPressed(int key, char character)
    {
        focusManager.keyPressed(key, character);
    }

    @Override
    public void update(GameContainer container, int delta)
    {
        focusManager.update(container, delta);
    }

    @Override
    public void render(GameContainer container, Graphics graphics)
    {
        Position2d position = getPosition();

        if(isMainWindow)
        {
            graphics.drawImage(background, position.getX(), position.getY());
        }
    }
}
