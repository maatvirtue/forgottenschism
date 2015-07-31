package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Size2d;
import org.newdawn.slick.GameContainer;
import org.newdawn.slick.Graphics;
import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;

public abstract class AbstractControl extends AbstractGuiComponent implements Control
{
	private boolean enabled;
	private boolean visible;
	private Image controlCanvas;
	private Graphics controlGraphics;

	public AbstractControl()
	{
		visible = true;
		enabled = true;
		setFocus(false);
		setPosition(new Position2d(0, 0));
		setSize(new Size2d(0, 0));
	}

	public abstract void renderControl(GameContainer container, Graphics graphics);

	private void refreshCanvas()
	{
		try
		{
			Size2d size = getSize();

			controlCanvas = new Image(size.getWidth(), size.getHeight());
			controlGraphics = controlCanvas.getGraphics();
		}
		catch(SlickException e)
		{
			throw new IllegalArgumentException(e);
		}
	}

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);

		refreshCanvas();
	}

	@Override
	public final void render(GameContainer container, Graphics graphics)
	{
		Position2d position = getPosition();

		this.renderControl(container, controlGraphics);

		controlGraphics.flush();
		graphics.drawImage(controlCanvas, position.getX(), position.getY());
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
	public void setFocus(boolean focus)
	{
		if(canHaveFocus())
			super.setFocus(focus);
	}

	@Override
	public void keyReleased(int key, char character)
	{
		//Default implementation is to do nothing
	}

	@Override
	public void keyPressed(int key, char character)
	{
		//Default implementation is to do nothing
	}

	@Override
	public void update(GameContainer container, int delta)
	{
		//Default implementation is to do nothing
	}
}
