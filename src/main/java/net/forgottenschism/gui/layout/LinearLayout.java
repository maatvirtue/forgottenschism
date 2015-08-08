package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;
import net.forgottenschism.util.GenericUtil;

import java.util.List;

public class LinearLayout extends AbstractLayout
{
	private static final Orientation2d DEFAULT_ORIENTATION = Orientation2d.VERTICAL;
	private static final int DEFAULT_DIVIDER_LENGTH = 0;

	private Orientation2d orientation;
	private int defaultDividerLength;

	public LinearLayout()
	{
		this(DEFAULT_ORIENTATION, DEFAULT_DIVIDER_LENGTH);
	}

	public LinearLayout(int defaultDividerLength)
	{
		this(DEFAULT_ORIENTATION, defaultDividerLength);
	}

	public LinearLayout(Orientation2d orientation)
	{
		this(orientation, DEFAULT_DIVIDER_LENGTH);
	}

	public LinearLayout(Orientation2d orientation, int defaultDividerLength)
	{
		this.defaultDividerLength = defaultDividerLength;
		this.orientation = orientation;
	}

	@Override
	public Size2d getPreferredSize()
	{
		int length = 0;
		int maxOtherOrientationLength = 0;
		Orientation2d otherOrientation = Orientation2d.getOtherOrientation(orientation);
		Size2d controlPreferredSize;

		for(Control control: getChildren())
		{
			controlPreferredSize = control.getPreferredSize();

			if(controlPreferredSize!=null)
			{
				length += controlPreferredSize.getValueByOrientation(orientation);

				maxOtherOrientationLength = GenericUtil.maximum(maxOtherOrientationLength,
						controlPreferredSize.getValueByOrientation(otherOrientation));
			}
		}

		length += defaultDividerLength*(getChildren().size()-1);

		Size2d preferredSize = new Size2d(0, 0);

		preferredSize.setValueByOrientation(orientation, length);
		preferredSize.setValueByOrientation(otherOrientation, maxOtherOrientationLength);

		return preferredSize;
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
