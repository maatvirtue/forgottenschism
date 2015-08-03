package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.ControlGroup;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.event.KeyEvent;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.KeyboardFocusConstants;
import net.forgottenschism.gui.focus.KeyboardFocusManager;

import net.forgottenschism.gui.layout.LayoutParameters;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;

public abstract class AbstractControl extends AbstractGuiComponent implements Control
{
	private boolean enabled;
	private boolean visible;
	private Image controlCanvas;
	private Graphics controlGraphics;
	private ControlGroup parent;
	private KeyboardFocusManager keyboardFocusManager;
	private FocusCycleRoot parentFocusCycleRoot;
	private LayoutParameters layoutParameters;

	public AbstractControl()
	{
		visible = true;
		enabled = true;
		parent = null;
		layoutParameters = null;
		setFocus(false);
		setPosition(new Position2d(0, 0));
		setSize(new Size2d(0, 0));
	}

	protected abstract void renderControl(Graphics graphics);

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);

		refreshCanvas();
	}

	private void refreshCanvas()
	{
		try
		{
			Size2d size = getSize();

			if(controlGraphics!=null)
				controlGraphics.destroy();

			if(controlCanvas!=null && !controlCanvas.isDestroyed())
				controlCanvas.destroy();

			controlCanvas = new Image(size.getWidth(), size.getHeight());
			controlGraphics = controlCanvas.getGraphics();
		}
		catch(SlickException e)
		{
			throw new IllegalArgumentException(e);
		}
	}

	@Override
	public final void render(Graphics graphics)
	{
		if(!isVisible())
			return;

		Position2d position = getPosition();

		renderControl(controlGraphics);

		controlGraphics.flush();
		graphics.drawImage(controlCanvas, position.getX(), position.getY());
	}

	@Override
	public int getForwardFocusTraversalKey()
	{
		return KeyboardFocusConstants.DEFAULT_FORWARD_TRAVERSAL_KEY;
	}

	@Override
	public int getBackwardFocusTraversalKey()
	{
		return KeyboardFocusConstants.DEFAULT_BACKWARD_TRAVERSAL_KEY;
	}

	@Override
	public int getUpwardFocusTraversalKey()
	{
		return KeyboardFocusConstants.DEFAULT_UPWARD_TRAVERSAL_KEY;
	}

	@Override
	public boolean isFocusTraversalOnKeyPressed()
	{
		return false;
	}

	@Override
	public void setFocus(boolean focus)
	{
		if(isFocusable())
			super.setFocus(focus);
	}

	@Override
	public final void update(int delta)
	{
		if(!isEnabled())
			return;

		updateControl(delta);
	}

	@Override
	public final void receiveEvent(KeyEvent keyEvent)
	{
		if(!isEnabled())
			return;

		if(keyEvent.isKeyPressed())
			keyPressed(keyEvent);
		else
			keyReleased(keyEvent);
	}

	@Override
	public void keyPressed(KeyEvent keyEvent)
	{
		//Default implementation is to do nothing
	}

	@Override
	public void keyReleased(KeyEvent keyEvent)
	{
		//Default implementation is to do nothing
	}

	protected void updateControl(int delta)
	{
		//Default implementation is to do nothing
	}

	@Override
	public void setParentControl(ControlGroup parent)
	{
		this.parent = parent;
	}

	@Override
	public ControlGroup getParentControl()
	{
		return parent;
	}

	@Override
	public boolean isEnabled()
	{
		return enabled;
	}

	@Override
	public void setEnabled(boolean enabled)
	{
		this.enabled = enabled;
	}

	@Override
	public boolean isVisible()
	{
		return visible;
	}

	@Override
	public void setVisible(boolean visible)
	{
		this.visible = visible;
	}

	@Override
	public void setKeyboardFocusManager(KeyboardFocusManager keyboardFocusManager)
	{
		this.keyboardFocusManager = keyboardFocusManager;
	}

	@Override
	public KeyboardFocusManager getKeyboardFocusManager()
	{
		return keyboardFocusManager;
	}

	@Override
	public FocusCycleRoot getParentFocusCycleRoot()
	{
		return parentFocusCycleRoot;
	}

	@Override
	public void setParentFocusCycleRoot(FocusCycleRoot parentFocusCycleRoot)
	{
		this.parentFocusCycleRoot = parentFocusCycleRoot;
	}

	@Override
	public LayoutParameters getLayoutParameters()
	{
		return layoutParameters;
	}

	@Override
	public void setLayoutParameters(LayoutParameters layoutParameters)
	{
		this.layoutParameters = layoutParameters;
	}
}
