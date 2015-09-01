package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.Orientation2d;
import net.forgottenschism.gui.bean.Position2d;
import net.forgottenschism.gui.bean.Size2d;

import org.apache.commons.lang3.math.NumberUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.util.List;

public class LinearLayout extends AbstractLayout
{
	private static final Logger logger = LoggerFactory.getLogger(LinearLayout.class);
	private static final Orientation2d DEFAULT_ORIENTATION = Orientation2d.VERTICAL;
	private static final int DEFAULT_DIVIDER_LENGTH = 0;

	private Orientation2d orientation;
	private int dividerLength;

	public LinearLayout()
	{
		this(DEFAULT_ORIENTATION, DEFAULT_DIVIDER_LENGTH);
	}

	public LinearLayout(int dividerLength)
	{
		this(DEFAULT_ORIENTATION, dividerLength);
	}

	public LinearLayout(Orientation2d orientation)
	{
		this(orientation, DEFAULT_DIVIDER_LENGTH);
	}

	public LinearLayout(Orientation2d orientation, int dividerLength)
	{
		this.dividerLength = dividerLength;
		this.orientation = orientation;
	}

	@Override
	public Size2d getPreferredSize()
	{
		int maxOtherOrientationLength = 0;
		Orientation2d otherOrientation = Orientation2d.getOtherOrientation(orientation);
		Size2d controlPreferredSize;

		for(Control control: getChildren())
		{
			controlPreferredSize = control.getPreferredSize();

			if(controlPreferredSize!=null)
			{
				maxOtherOrientationLength = NumberUtils.max(maxOtherOrientationLength,
						controlPreferredSize.getValueByOrientation(otherOrientation));
			}
		}

		int length;

		if(getTotalWeight(getChildren())>0)
			length = getSize().getValueByOrientation(otherOrientation);
		else
			length = getTotalLengthWithoutWeight(getChildren());

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

		int layoutLength = getSize().getValueByOrientation(orientation);
		float totalWeight = getTotalWeight(children);
		int totalLength = getTotalLengthWithoutWeight(children);
		int totalLengthToSpread = layoutLength-totalLength;
		int lengthToSpread = totalLengthToSpread;
		int laidoutControlLength;

		for(int i = 0; i<children.size(); i++)
		{
			control = children.get(i);

			laidoutControlLength = getLaidoutControlLength(control, totalLengthToSpread, totalWeight);

			applyControlSize(control, laidoutControlLength);

			controlPosition = new Position2d(0, 0);
			controlPosition.setValueByOrientation(orientation, position);
			control.setPosition(controlPosition);

			position += laidoutControlLength;

			if(hasWeight(control))
				lengthToSpread -= laidoutControlLength;

			if(i!=children.size()-1)
				position += dividerLength;
		}

		if(totalWeight>0 && lengthToSpread>0)
		{
			Control lastWeightedControl = getLastWeightedControl(children);
			Size2d lastWeightedControlSize = lastWeightedControl.getSize();

			lastWeightedControlSize.setValueByOrientation(orientation,
					lastWeightedControlSize.getValueByOrientation(orientation)+lengthToSpread);
		}
	}

	private static Control getLastWeightedControl(List<Control> controls)
	{
		Control control;

		for(int i = controls.size()-1; i>=0; i--)
		{
			control = controls.get(i);

			if(hasWeight(control))
				return control;
		}

		return null;
	}

	private static boolean hasWeight(Control control)
	{
		return control.getLayoutParameters()!=null && control.getLayoutParameters() instanceof LinearLayoutParameters
				&& ((LinearLayoutParameters) control.getLayoutParameters()).getWeight()!=null;
	}

	private int getLaidoutControlLength(Control control, int totalLengthToSpread, float totalWeight)
	{
		Size2d controlPreferredSize = control.getPreferredSize();
		float controlWeight = 0;
		int controlFixedLength = 0;

		if(control.getLayoutParameters()!=null && control.getLayoutParameters() instanceof LinearLayoutParameters)
		{
			LinearLayoutParameters layoutParameters = (LinearLayoutParameters) control.getLayoutParameters();

			if(layoutParameters.getWeight()!=null)
				controlWeight = layoutParameters.getWeight();

			if(layoutParameters.getLength()!=null)
				controlFixedLength = layoutParameters.getLength();
		}

		if(controlFixedLength>0)
			return controlFixedLength;
		else if(totalLengthToSpread>0 && controlWeight>0)
		{
			float ratio = controlWeight/totalWeight;

			return (int) (totalLengthToSpread*ratio);
		}
		else if(controlPreferredSize!=null)
			return controlPreferredSize.getValueByOrientation(orientation);
		else
			return 0;
	}

	private int getTotalLengthWithoutWeight(List<Control> controls)
	{
		int totalLength = 0;
		LinearLayoutParameters layoutParameters;

		for(Control control : controls)
		{
			layoutParameters = null;

			if(control.getLayoutParameters()!=null)
			{
				if(control.getLayoutParameters() instanceof LinearLayoutParameters)
				{
					layoutParameters = (LinearLayoutParameters) control.getLayoutParameters();
				}
				else
				{
					logger.warn("Child Control ("+control+") of LinearLayout ("+this+") has a LayoutParameter which is not a LinearLayoutParameters.");
				}
			}

			if(layoutParameters!=null && layoutParameters.getLength()!=null)
				totalLength += layoutParameters.getLength();
			else if(!hasWeight(control) && control.getPreferredSize()!=null)
				totalLength += control.getPreferredSize().getValueByOrientation(orientation);
		}

		totalLength += dividerLength*(controls.size()-1);

		return totalLength;
	}

	private float getTotalWeight(List<Control> controls)
	{
		float totalWeight = 0;
		LinearLayoutParameters layoutParameters;

		for(Control control : controls)
		{
			if(control.getLayoutParameters()==null)
				continue;

			if(!(control.getLayoutParameters() instanceof LinearLayoutParameters))
			{
				logger.warn("Child Control ("+control+") of LinearLayout ("+this+") has a LayoutParameter which is not a LinearLayoutParameters.");
				continue;
			}

			layoutParameters = (LinearLayoutParameters) control.getLayoutParameters();

			if(layoutParameters.getWeight()!=null)
				totalWeight += layoutParameters.getWeight();
		}

		return totalWeight;
	}

	private void applyControlSize(Control control, int length)
	{
		Size2d layoutSize = getSize();
		Size2d controlPreferredSize = control.getPreferredSize();
		Size2d controlSize = new Size2d();
		Orientation2d otherOrientation = Orientation2d.getOtherOrientation(orientation);

		controlSize.setValueByOrientation(orientation, length);
		controlSize.setValueByOrientation(otherOrientation,
				NumberUtils.min(layoutSize.getValueByOrientation(otherOrientation),
						controlPreferredSize.getValueByOrientation(otherOrientation)));

		control.setSize(controlSize);
	}

	public int getDividerLength()
	{
		return dividerLength;
	}

	public void setDividerLength(int dividerLength)
	{
		this.dividerLength = dividerLength;
	}
}
