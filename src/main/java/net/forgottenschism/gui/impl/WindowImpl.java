package net.forgottenschism.gui.impl;

import net.forgottenschism.engine.GraphicsGenerator;
import net.forgottenschism.gui.*;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;
import org.newdawn.slick.*;

public class WindowImpl extends AbstractGuiComponent implements Window
{
	private static final Size2d DEFAULT_SIZE = new Size2d(400, 400);

	private FocusManager focusManager;
	private Screen parentScreen;
	private boolean isMainWindow;
	private Image background;
	private Color backgroundColor;

	private Image windowCanvas;
	private Graphics windowGraphics;

	public WindowImpl(Screen parentScreen)
	{
		this(parentScreen, false);
	}

	public WindowImpl(Screen parentScreen, boolean isMainWindow)
	{
		this.parentScreen = parentScreen;
		this.isMainWindow = isMainWindow;
		this.backgroundColor = ColorTheme.getDefaultColorTheme().getColor(ColorThemeElement.WINDOW_DEFAULT_BACKGROUND);

		if(isMainWindow)
		{
			setPosition(new Position2d(0, 0));
			setSize(parentScreen.getScreenSize());
		}
		else
		{
			setPosition(getCenteredPosition(parentScreen.getScreenSize(), DEFAULT_SIZE));
			setSize(DEFAULT_SIZE);
		}

		focusManager = new FocusManagerImpl();
	}

	private Position2d getCenteredPosition(Size2d screenSize, Size2d windowSize)
	{
		Position2d position = new Position2d();

		position.setX((screenSize.getWidth()-windowSize.getWidth())/2);
		position.setY((screenSize.getHeight()-windowSize.getHeight())/2);

		return position;
	}

	private void refreshCanvas()
	{
		try
		{
			Size2d size = getSize();

			windowCanvas = new Image(size.getWidth(), size.getHeight());
			windowGraphics = windowCanvas.getGraphics();
		}
		catch(SlickException e)
		{
			throw new IllegalArgumentException(e);
		}
	}

	private void refreshBackground()
	{
		Size2d size = getSize();

		background = GraphicsGenerator.getSolidColorImage(size.getWidth(), size.getHeight(), backgroundColor);
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

		graphics.drawImage(background, position.getX(), position.getY());

		focusManager.render(container, windowGraphics);

		windowGraphics.flush();
		graphics.drawImage(windowCanvas, position.getX(), position.getY());
	}

	public Color getBackgroundColor()
	{
		return backgroundColor;
	}

	public void setBackgroundColor(Color backgroundColor)
	{
		this.backgroundColor = backgroundColor;

		refreshBackground();
	}

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);

		refreshBackground();
		refreshCanvas();
	}
}
