package net.forgottenschism.gui.layout;

import net.forgottenschism.gui.bean.GraphicMeasure;
import net.forgottenschism.gui.bean.GraphicalUnit;
import net.forgottenschism.gui.bean.RelativeMeasure;
import net.forgottenschism.gui.bean.RelativePosition;

public class RelativeLayoutParameters implements LayoutParameters
{
	private GraphicMeasure width;
	private GraphicMeasure height;
	private RelativeMeasure horizontalPosition;
	private RelativeMeasure verticalPosition;

	public GraphicMeasure getWidth()
	{
		return width;
	}

	public void setPosition(RelativeMeasure vertical, RelativeMeasure horizontal)
	{
		setVerticalPosition(vertical);
		setHorizontalPosition(horizontal);
	}

	public void horizontallyCentered()
	{
		setHorizontalPosition(new RelativeMeasure(RelativePosition.CENTER, null));
	}

	public void verticalyCentered()
	{
		setVerticalPosition(new RelativeMeasure(RelativePosition.CENTER, null));
	}

	public void setLeftPosition(int value, GraphicalUnit unit)
	{
		setHorizontalPosition(new RelativeMeasure(RelativePosition.LEFT, new GraphicMeasure(value, unit)));
	}

	public void setRightPosition(int value, GraphicalUnit unit)
	{
		setHorizontalPosition(new RelativeMeasure(RelativePosition.RIGHT, new GraphicMeasure(value, unit)));
	}

	public void setTopPosition(int value, GraphicalUnit unit)
	{
		setVerticalPosition(new RelativeMeasure(RelativePosition.TOP, new GraphicMeasure(value, unit)));
	}

	public void setBottomPosition(int value, GraphicalUnit unit)
	{
		setVerticalPosition(new RelativeMeasure(RelativePosition.BOTTOM, new GraphicMeasure(value, unit)));
	}

	public void setWidth(GraphicMeasure width)
	{
		this.width = width;
	}

	public GraphicMeasure getHeight()
	{
		return height;
	}

	public void setHeight(GraphicMeasure height)
	{
		this.height = height;
	}

	public RelativeMeasure getHorizontalPosition()
	{
		return horizontalPosition;
	}

	public void setHorizontalPosition(RelativeMeasure horizontalPosition)
	{
		if(horizontalPosition.getRelativePosition()!=RelativePosition.LEFT &&
				horizontalPosition.getRelativePosition()!=RelativePosition.RIGHT &&
				horizontalPosition.getRelativePosition()!=RelativePosition.CENTER)
			throw new IllegalArgumentException("horizontalPosition relative measure must be horizontal (eg. RIGHT, LEFT, CENTER)");

		this.horizontalPosition = horizontalPosition;
	}

	public RelativeMeasure getVerticalPosition()
	{
		return verticalPosition;
	}

	public void setVerticalPosition(RelativeMeasure verticalPosition)
	{
		if(verticalPosition.getRelativePosition()!=RelativePosition.TOP &&
				verticalPosition.getRelativePosition()!=RelativePosition.BOTTOM &&
				verticalPosition.getRelativePosition()!=RelativePosition.CENTER)
			throw new IllegalArgumentException("verticalPosition relative measure must be vertical (eg. TOP, BOTTOM, CENTER)");

		this.verticalPosition = verticalPosition;
	}
}
