package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.gui.impl.AbstractControlGroup;

import org.newdawn.slick.Graphics;

public class AbsoluteLayout extends AbstractControlGroup implements Layout
{
	@Override
	protected void renderControl(Graphics graphics)
	{
		for(Control control : getChildren())
		{
			control.render(graphics);
		}
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
