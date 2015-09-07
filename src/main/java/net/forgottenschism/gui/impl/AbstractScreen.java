package net.forgottenschism.gui.impl;

import net.forgottenschism.engine.ScreenManager;
import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.Screen;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.Window;
import net.forgottenschism.gui.event.KeyEvent;
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
	private GameContainer gameContainer;
	private ScreenManager screenManager;

    public AbstractScreen()
    {
        windows = new LinkedList<>();
        enabled = false;
        visible = false;
        size = new Size2d(0, 0);

        Window mainWindow = new WindowImpl(this, true);
		showWindow(mainWindow);
    }

	@Override
	public void setScreenManager(ScreenManager screenManager)
	{
		this.screenManager = screenManager;
	}

	protected void exitGame()
	{
		screenManager.exitGame();
	}

	@Override
	public void addControl(Control control)
	{
		getMainWindow().addControl(control);
	}

	@Override
	public void removeControl(Control control)
	{
		getMainWindow().removeControl(control);
	}

	@Override
	public void removeAllControl()
	{
		getMainWindow().removeAllControl();
	}

	@Override
	public void init(GameContainer gameContainer)
	{
		this.gameContainer = gameContainer;
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
    public void showWindow(Window window)
    {
        Window activeWindow = getActiveWindow();

        if(activeWindow!=null)
            activeWindow.setFocus(false);

		if(!windows.contains(window))
			windows.add(window);
		else
		{
			//Put back on top
			windows.remove(window);
			windows.add(window);
		}

		centerWindowOnScreen(window);

        window.setFocus(true);
    }

	private void centerWindowOnScreen(Window window)
	{
		Size2d windowSize = window.getSize();
		Size2d screenSize = getScreenSize();
		int xPosition = (screenSize.getWidth()-windowSize.getWidth())/2;
		int yPosition = (screenSize.getHeight()-windowSize.getHeight())/2;

		window.setPosition(new Position2d(xPosition, yPosition));
	}

    @Override
    public void closeWindow(Window window)
    {
		window.setFocus(false);
		windows.remove(window);

        if(!windows.isEmpty())
            getActiveWindow().setFocus(true);
    }

    private Window getActiveWindow()
    {
        if(windows.isEmpty())
            return null;

        return windows.get(windows.size()-1);
    }

    protected Window getMainWindow()
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
    public final void keyReleased(KeyEvent keyEvent)
    {
        Window activeWindow = getActiveWindow();

        boolean dispatchEventToActiveWindow = screenKeyReleased(keyEvent);

        if(dispatchEventToActiveWindow && activeWindow!=null)
            activeWindow.keyReleased(keyEvent);
    }

    /**
     * Meant to be overridden by a Screen to listen to keyReleased events.
     *
     * @return true if the event should then be dispatched to the Screen's active window, false otherwise.
     */
    public boolean screenKeyReleased(KeyEvent keyEvent)
    {
        return true;
    }

    @Override
    public final void keyPressed(KeyEvent keyEvent)
    {
        Window activeWindow = getActiveWindow();

        boolean dispatchEventToActiveWindow = screenKeyPressed(keyEvent);

        if(dispatchEventToActiveWindow && activeWindow!=null)
            activeWindow.keyPressed(keyEvent);
    }

    /**
     * Meant to be overridden by a Screen to listen to keyPressed events.
     *
     * @return true if the event should then be dispatched to the Screen's active window, false otherwise.
     */
    public boolean screenKeyPressed(KeyEvent keyEvent)
    {
        return true;
    }

    @Override
    public final void render(Graphics graphics)
    {
        if(!visible)
            return;

        for(Window window : windows)
			window.render(graphics);
	}

    @Override
    public final void update(int delta)
    {
        if(!enabled)
            return;

        for(Window window : windows)
			window.update(delta);
	}
}
