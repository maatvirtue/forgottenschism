package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.bean.*;
import net.forgottenschism.gui.impl.AbstractControlGroup;
import net.forgottenschism.util.GenericUtil;
import org.newdawn.slick.Graphics;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public abstract class AbstractLayout extends AbstractControlGroup implements Layout
{
	private static final Logger logger = LoggerFactory.getLogger(AbstractLayout.class);

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

	protected static void layoutRelatively(Position2d referencePosition, Size2d regionSize, Control control)
	{
		if(control.getLayoutParameters()==null || !(control.getLayoutParameters() instanceof RelativeLayoutParameters))
		{
			applyDefaultLayout(control);
		}
		else
		{
			RelativeLayoutParameters layoutParameters = (RelativeLayoutParameters) control.getLayoutParameters();

			applyWidth(regionSize, control, layoutParameters.getWidth());
			applyHeight(regionSize, control, layoutParameters.getHeight());

			//Position applied after size because of CENTER that has to know the control's size
			applyHorizontalPosition(regionSize, control, layoutParameters.getHorizontalPosition());
			applyVerticalPosition(regionSize, control, layoutParameters.getVerticalPosition());
		}

		control.getPosition().add(referencePosition);
	}

	private static void applyHorizontalPosition(Size2d regionSize, Control control, RelativeMeasure horizontal)
	{
		if(horizontal==null)
		{
			applyDefaultHorizontalPosition(control);
			return;
		}

		RelativePosition relativePosition = horizontal.getRelativePosition();
		GraphicMeasure measure = horizontal.getMeasure();
		Position2d controlPosition = control.getPosition();
		Size2d controlSize = control.getSize();

		if(relativePosition==RelativePosition.LEFT)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setX(measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setX(GenericUtil.getRatio(regionSize.getWidth(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.RIGHT)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setX(regionSize.getWidth()-measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setX(regionSize.getWidth()-GenericUtil.getRatio(regionSize.getWidth(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.CENTER)
			controlPosition.setX((regionSize.getWidth()-controlSize.getWidth())/2);

		control.setPosition(controlPosition);
	}

	private static void applyVerticalPosition(Size2d regionSize, Control control, RelativeMeasure vertical)
	{
		if(vertical==null)
		{
			applyDefaultVerticalPosition(control);
			return;
		}

		RelativePosition relativePosition = vertical.getRelativePosition();
		GraphicMeasure measure = vertical.getMeasure();
		Position2d controlPosition = control.getPosition();
		Size2d controlSize = control.getSize();

		if(relativePosition==RelativePosition.TOP)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setY(measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setY(GenericUtil.getRatio(regionSize.getHeight(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.BOTTOM)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setY(regionSize.getHeight()-measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setY(regionSize.getHeight()-GenericUtil.getRatio(regionSize.getHeight(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.CENTER)
			controlPosition.setY((regionSize.getHeight()-controlSize.getHeight())/2);

		control.setPosition(controlPosition);
	}

	private static void applyWidth(Size2d regionSize, Control control, GraphicMeasure width)
	{
		if(width==null)
		{
			applyDefaultWidth(control);
			return;
		}

		Size2d controlSize = control.getSize();

		if(width.getUnit()==GraphicalUnit.PIXEL)
			controlSize.setWidth(width.getValue());
		else if(width.getUnit()==GraphicalUnit.PERCENT)
			controlSize.setWidth(width.getValue()*regionSize.getWidth());

		control.setSize(controlSize);
	}

	private static void applyHeight(Size2d regionSize, Control control, GraphicMeasure height)
	{
		if(height==null)
		{
			applyDefaultHeight(control);
			return;
		}

		Size2d controlSize = control.getSize();

		if(height.getUnit()==GraphicalUnit.PIXEL)
			controlSize.setHeight(height.getValue());
		else if(height.getUnit()==GraphicalUnit.PERCENT)
			controlSize.setHeight(height.getValue()*regionSize.getHeight());

		control.setSize(controlSize);
	}

	private static void applyDefaultLayout(Control control)
	{
		applyDefaultPosition(control);
		applyDefaultSize(control);
	}

	private static void applyDefaultSize(Control control)
	{
		applyDefaultWidth(control);
		applyDefaultHeight(control);
	}

	private static void applyDefaultWidth(Control control)
	{
		Size2d size = control.getSize();
		Size2d preferredSize = control.getPreferredSize();

		if(preferredSize!=null)
			size.setWidth(preferredSize.getWidth());
		else
			size.setWidth(0);

		control.setSize(size);
	}

	private static void applyDefaultHeight(Control control)
	{
		Size2d size = control.getSize();
		Size2d preferredSize = control.getPreferredSize();

		if(preferredSize!=null)
			size.setHeight(preferredSize.getHeight());
		else
			size.setHeight(0);

		control.setSize(size);
	}

	private static void applyDefaultPosition(Control control)
	{
		applyDefaultHorizontalPosition(control);
		applyDefaultVerticalPosition(control);
	}

	private static void applyDefaultHorizontalPosition(Control control)
	{
		Position2d position = control.getPosition();

		position.setX(0);

		control.setPosition(position);
	}

	private static void applyDefaultVerticalPosition(Control control)
	{
		Position2d position = control.getPosition();

		position.setY(0);

		control.setPosition(position);
	}
}
