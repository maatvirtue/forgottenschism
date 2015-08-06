package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.util.GenericUtil;

import java.util.List;

public class LinearLayout extends AbstractLayout
{
	private Orientation2d orientation;
	private int defaultDividerLength;

	public LinearLayout(Orientation2d orientation)
	{
		this(orientation, 0);
	}

	public LinearLayout(Orientation2d orientation, int defaultDividerLength)
	{
		this.defaultDividerLength = defaultDividerLength;
		this.orientation = orientation;
	}

	@Override
	protected void layout()
	{
		if(getChildren()==null)
			return;

		int position = 0;
		List<Control> children = getChildren();
		Control control;
		Position2d controlPosition;

		for(int i = 0; i<children.size(); i++)
		{
			control = children.get(i);

			applyControlSize(control);

			controlPosition = new Position2d(0, 0);
			controlPosition.setValueByOrientation(orientation, position);
			control.setPosition(controlPosition);

			position += control.getSize().getValueByOrientation(orientation);

			if(i!=children.size()-1)
				position += defaultDividerLength;
		}
	}

	private void applyControlSize(Control control)
	{
		Size2d layoutSize = getSize();
		Size2d controlPreferredSize = control.getPreferredSize();
		Size2d controlSize = new Size2d();
		Orientation2d otherOrientation = Orientation2d.getOtherOrientation(orientation);

		controlSize.setValueByOrientation(orientation, controlPreferredSize.getValueByOrientation(orientation));
		controlSize.setValueByOrientation(otherOrientation,
				GenericUtil.minimum(layoutSize.getValueByOrientation(otherOrientation),
						controlPreferredSize.getValueByOrientation(otherOrientation)));

		control.setSize(controlSize);
	}

	public int getDefaultDividerLength()
	{
		return defaultDividerLength;
	}

	public void setDefaultDividerLength(int defaultDividerLength)
	{
		this.defaultDividerLength = defaultDividerLength;
	}
}
