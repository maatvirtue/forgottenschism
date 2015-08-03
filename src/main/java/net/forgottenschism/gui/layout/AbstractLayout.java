package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.impl.AbstractControlGroup;
import org.newdawn.slick.Graphics;

public abstract class AbstractLayout extends AbstractControlGroup implements Layout
{
	protected abstract void layout();

	@Override
	protected void renderControl(Graphics graphics)
	{
		for(Control control : getChildren())
		{
			control.render(graphics);
		}
	}

	@Override
	public void setSize(Size2d size)
	{
		super.setSize(size);

		layout();
	}

	@Override
	public boolean isFocusable()
	{
		return false;
	}

	@Override
	public Size2d getPreferredSize()
	{
		return null;
	}
}
