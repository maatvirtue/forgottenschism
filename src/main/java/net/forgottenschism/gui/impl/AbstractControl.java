package net.forgottenschism.gui.impl;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.Position2d;
import net.forgottenschism.gui.Size2d;
import org.newdawn.slick.GameContainer;

public abstract class AbstractControl extends AbstractGuiComponent implements Control
{
	private boolean enabled;
	private boolean visible;

	public AbstractControl()
	{
		visible = true;
		enabled = true;
		setFocus(false);
		setPosition(new Position2d(0, 0));
		setSize(new Size2d(0, 0));
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
