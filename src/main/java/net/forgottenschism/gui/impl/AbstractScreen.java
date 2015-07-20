package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Screen;
import net.forgottenschism.gui.Size2d;
import net.forgottenschism.gui.Window;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;

import java.util.LinkedList;
import java.util.List;

public abstract class AbstractScreen implements Screen
{
    private List<Window> windows;
    private boolean enabled;
    private boolean visible;
    private Size2d size;

    public AbstractScreen()
    {
        windows = new LinkedList<>();
        enabled = false;
        visible = false;

        Window mainWindow = new WindowImpl(this, true);

        displayNewWindow(mainWindow);
    }

    @Override
    public void setScreenSize(Size2d size)
    {
        this.size = size;

        getMainWindow().setSize(size);
    }

    @Override
    public Size2d getScreenSize()
    {
        return size;
    }

    @Override
    public void displayNewWindow(Window window)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.setFocus(false);

        windows.add(window);

        window.setFocus(true);
    }

    @Override
    public void closeWindow(Window window)
    {
        windows.remove(window);

        if(!windows.isEmpty())
            getActiveWindow().setFocus(true);
    }

    private Window getActiveWindow()
    {
        if(windows.isEmpty())
            return null;

        return windows.get(windows.size());
    }

    private Window getMainWindow()
    {
        return windows.get(0);
    }

    @Override
    public void enterScreen()
    {
        enabled = true;
        visible = true;
    }

    @Override
    public void pauseScreen()
    {
        enabled = false;
        visible = false;
    }

    @Override
    public void resumeScreen()
    {
        enabled = true;
        visible = true;
    }

    @Override
    public void leaveScreen()
    {
        enabled = false;
        visible = false;
    }

    @Override
    public void keyReleased(int key, char character)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.keyReleased(key, character);
    }

    @Override
    public void keyPressed(int key, char character)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.keyPressed(key, character);
    }

    @Override
    public void render(GameContainer container, Graphics graphics)
    {
        if(!visible)
            return;

        for(Window window: windows)
            window.render(container, graphics);
    }

    @Override
    public void update(GameContainer container, int delta)
    {
        if(!enabled)
            return;

        for(Window window: windows)
            window.update(container, delta);
    }
}
