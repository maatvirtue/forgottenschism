package net.forgottenschism.gui.bean;

public class RelativeMeasure
{
	private RelativePosition relativePosition;
	private GraphicMeasure measure;

	public RelativeMeasure(RelativePosition relativePosition, GraphicMeasure measure)
	{
		this.relativePosition = relativePosition;
		this.measure = measure;
	}

	@Override
	public String toString()
	{
		return "["+relativePosition+" "+measure+"]";
	}

	public RelativePosition getRelativePosition()
	{
		return relativePosition;
	}

	public void setRelativePosition(RelativePosition relativePosition)
	{
		this.relativePosition = relativePosition;
	}

	public GraphicMeasure getMeasure()
	{
		return measure;
	}

	public void setMeasure(GraphicMeasure measure)
	{
		this.measure = measure;
	}
}
