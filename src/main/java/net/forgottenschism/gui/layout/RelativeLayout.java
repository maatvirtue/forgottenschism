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
		if(control.getLayoutParameters()==null || !(control.getLayoutParameters() instanceof RelativeLayoutParameters))
		{
			logger.error("Control has no RelativeLayoutParameters: "+control);
			applyDefaultLayout(control);
			return;
		}

		RelativeLayoutParameters layoutParameters = (RelativeLayoutParameters) control.getLayoutParameters();

		applyWidth(control, layoutParameters.getWidth());
		applyHeight(control, layoutParameters.getHeight());

		//Position applied after size because of CENTER that has to know the control's size
		applyHorizontalPosition(control, layoutParameters.getHorizontalPosition());
		applyVerticalPosition(control, layoutParameters.getVerticalPosition());
	}

	private int getRatio(int value, int percent)
	{
		return percent*value/100;
	}

	private void applyHorizontalPosition(Control control, RelativeMeasure horizontal)
	{
		if(horizontal==null)
		{
			applyDefaultHorizontalPosition(control);
			return;
		}

		RelativePosition relativePosition = horizontal.getRelativePosition();
		GraphicMeasure measure = horizontal.getMeasure();
		Size2d layoutSize = getSize();
		Position2d controlPosition = control.getPosition();
		Size2d controlSize = control.getSize();

		if(relativePosition==RelativePosition.LEFT)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setX(measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setX(getRatio(layoutSize.getWidth(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.RIGHT)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setX(layoutSize.getWidth()-measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setX(layoutSize.getWidth()-getRatio(layoutSize.getWidth(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.CENTER)
			controlPosition.setX((layoutSize.getWidth()-controlSize.getWidth())/2);

		control.setPosition(controlPosition);
	}

	private void applyVerticalPosition(Control control, RelativeMeasure vertical)
	{
		if(vertical==null)
		{
			applyDefaultVerticalPosition(control);
			return;
		}

		RelativePosition relativePosition = vertical.getRelativePosition();
		GraphicMeasure measure = vertical.getMeasure();
		Size2d layoutSize = getSize();
		Position2d controlPosition = control.getPosition();
		Size2d controlSize = control.getSize();

		if(relativePosition==RelativePosition.TOP)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setY(measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setY(getRatio(layoutSize.getHeight(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.BOTTOM)
		{
			if(measure.getUnit()==GraphicalUnit.PIXEL)
				controlPosition.setY(layoutSize.getHeight()-measure.getValue());
			else if(measure.getUnit()==GraphicalUnit.PERCENT)
				controlPosition.setY(layoutSize.getHeight()-getRatio(layoutSize.getHeight(), measure.getValue()));
		}
		else if(relativePosition==RelativePosition.CENTER)
			controlPosition.setY((layoutSize.getHeight()-controlSize.getHeight())/2);

		control.setPosition(controlPosition);
	}

	private void applyWidth(Control control, GraphicMeasure width)
	{
		if(width==null)
		{
			applyDefaultWidth(control);
			return;
		}

		Size2d controlSize = control.getSize();
		Size2d layoutSize = getSize();

		if(width.getUnit()==GraphicalUnit.PIXEL)
			controlSize.setWidth(width.getValue());
		else if(width.getUnit()==GraphicalUnit.PERCENT)
			controlSize.setWidth(width.getValue()*layoutSize.getWidth());

		control.setSize(controlSize);
	}

	private void applyHeight(Control control, GraphicMeasure height)
	{
		if(height==null)
		{
			applyDefaultHeight(control);
			return;
		}

		Size2d controlSize = control.getSize();
		Size2d layoutSize = getSize();

		if(height.getUnit()==GraphicalUnit.PIXEL)
			controlSize.setHeight(height.getValue());
		else if(height.getUnit()==GraphicalUnit.PERCENT)
			controlSize.setHeight(height.getValue()*layoutSize.getHeight());

		control.setSize(controlSize);
	}

	private void applyDefaultLayout(Control control)
	{
		applyDefaultPosition(control);
		applyDefaultSize(control);
	}

	private void applyDefaultSize(Control control)
	{
		applyDefaultWidth(control);
		applyDefaultHeight(control);
	}

	private void applyDefaultWidth(Control control)
	{
		Size2d size = control.getSize();
		Size2d preferredSize = control.getPreferredSize();

		if(preferredSize!=null)
			size.setWidth(preferredSize.getWidth());
		else
			size.setWidth(0);

		control.setSize(size);
	}

	private void applyDefaultHeight(Control control)
	{
		Size2d size = control.getSize();
		Size2d preferredSize = control.getPreferredSize();

		if(preferredSize!=null)
			size.setHeight(preferredSize.getHeight());
		else
			size.setHeight(0);

		control.setSize(size);
	}

	private void applyDefaultPosition(Control control)
	{
		applyDefaultHorizontalPosition(control);
		applyDefaultVerticalPosition(control);
	}

	private void applyDefaultHorizontalPosition(Control control)
	{
		Position2d position = control.getPosition();

		position.setX(0);

		control.setPosition(position);
	}

	private void applyDefaultVerticalPosition(Control control)
	{
		Position2d position = control.getPosition();

		position.setY(0);

		control.setPosition(position);
	}
}
