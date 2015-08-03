package net.forgottenschism.gui.bean;

public class GraphicMeasure
{
	private int value;
	private GraphicalUnit unit;

	public GraphicMeasure(int value, GraphicalUnit unit)
	{
		validate(value, unit);

		this.value = value;
		this.unit = unit;
	}

	private void validate(int value, GraphicalUnit unit)
	{
		if(unit==GraphicalUnit.PERCENT && (value<0 || value>100))
			throw new IllegalArgumentException("value must be between 0 and 100 percent");
	}

	@Override
	public String toString()
	{
		String unitText = "";

		if(unit==GraphicalUnit.PERCENT)
			unitText = "%";
		else if(unit==GraphicalUnit.PIXEL)
			unitText = "px";

		return Integer.toString(value)+unitText;
	}

	public int getValue()
	{
		return value;
	}

	public void setValue(int value)
	{
		this.value = value;
	}

	public GraphicalUnit getUnit()
	{
		return unit;
	}

	public void setUnit(GraphicalUnit unit)
	{
		this.unit = unit;
	}
}
