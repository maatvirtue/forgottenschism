package net.forgottenschism.gui.focus;

import net.forgottenschism.gui.Size2d;
import net.forgottenschism.gui.focus.impl.AbstractFocusCycleRoot;
import org.newdawn.slick.Graphics;

public class WindowFocusCycleRoot extends AbstractFocusCycleRoot
{
	@Override
	protected void renderControl(Graphics graphics)
	{
		//Do nothing
	}

	@Override
	public Size2d getPreferredSize()
	{
		return null;
	}
}
