package net.forgottenschism.gui.impl;

import net.forgottenschism.engine.GraphicsGenerator;
import net.forgottenschism.gui.*;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.KeyboardFocusManager;
import net.forgottenschism.gui.focus.WindowFocusCycleRoot;
import net.forgottenschism.gui.focus.impl.KeyboardFocusManagerImpl;
import net.forgottenschism.gui.layout.AbsoluteLayout;
import net.forgottenschism.gui.layout.Layout;
import net.forgottenschism.gui.theme.ColorTheme;
import net.forgottenschism.gui.theme.ColorThemeElement;

import org.newdawn.slick.*;

public class WindowImpl extends AbstractGuiComponent implements Window
{
	private static final Size2d DEFAULT_SIZE = new Size2d(400, 400);

	private KeyboardFocusManager keyboardFocusManager;
	private Screen parentScreen;
	private boolean isMainWindow;
	private Image background;
	private Color backgroundColor;
	private Image windowCanvas;
	private Graphics windowGraphics;
	private Layout layout;
	private FocusCycleRoot focusCycleRoot;

	public WindowImpl(Screen parentScreen)
	{
		this(parentScreen, false);
	}

	public WindowImpl(Screen parentScreen, boolean isMainWindow)
	{
		this.parentScreen = parentScreen;
		this.isMainWindow = isMainWindow;
		this.backgroundColor = ColorTheme.getDefaultColorTheme().getColor(ColorThemeElement.WINDOW_DEFAULT_BACKGROUND);

		focusCycleRoot = new WindowFocusCycleRoot(); //has to be initialized before KeyboardFocusManager
		layout = new AbsoluteLayout(); //has to be initialized before setSize()
		keyboardFocusManager = new KeyboardFocusManagerImpl(this);

		layout.setKeyboardFocusManager(keyboardFocusManager);
		focusCycleRoot.setKeyboardFocusManager(keyboardFocusManager);
		focusCycleRoot.addControl(layout);

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

			if(windowGraphics!=null)
				windowGraphics.destroy();

			if(windowCanvas!=null && !windowCanvas.isDestroyed())
				windowCanvas.destroy();

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
		keyboardFocusManager.focusDefaultControl();

		if(!isMainWindow)
			parentScreen.showWindow(this);
	}

	@Override
	public void close()
	{
		if(!isMainWindow)
			parentScreen.closeWindow(this);
	}

	@Override
	public void setLayout(Layout layout)
	{
		this.layout = layout;

		layout.setKeyboardFocusManager(keyboardFocusManager);

		focusCycleRoot.removeAllChildren();
		focusCycleRoot.addControl(this.layout);
	}

	@Override
	public Layout getLayout()
	{
		return layout;
	}

	@Override
	public void addControl(Control control)
	{
		layout.addControl(control);
	}

	@Override
	public void removeControl(Control control)
	{
		layout.removeControl(control);
	}

	@Override
	public void removeAllControl()
	{
		layout.removeAllChildren();
	}

	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		keyboardFocusManager.dispatchKeyEvent(keyEvent);
	}

	@Override
	public void keyPressed(KeyEvent keyEvent)
	{
		keyboardFocusManager.dispatchKeyEvent(keyEvent);
	}

	@Override
	public void update(int delta)
	{
		layout.update(delta);
	}

	@Override
	public void render(Graphics graphics)
	{
		Position2d position = getPosition();

		renderWindow(windowGraphics);

		windowGraphics.flush();
		graphics.drawImage(windowCanvas, position.getX(), position.getY());
	}

	private void renderWindow(Graphics graphics)
	{
		graphics.drawImage(background, 0, 0);

		layout.render(graphics);
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

		layout.setSize(size);
	}

	@Override
	public FocusCycleRoot getFocusCycleRoot()
	{
		return focusCycleRoot;
	}
}
