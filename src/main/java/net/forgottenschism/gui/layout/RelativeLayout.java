package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class RelativeLayout extends AbstractLayout
{
	private static final Logger logger = LoggerFactory.getLogger(RelativeLayout.class);

	protected void layout()
	{
		if(getChildren()==null)
			return;

		for(Control control : getChildren())
			layout(control);
	}

	private void layout(Control control)
	{
		layoutRelatively(new Position2d(0, 0), getSize(), control);
	}

	@Override
	public Size2d getPreferredSize()
	{
		return null;
	}
}
