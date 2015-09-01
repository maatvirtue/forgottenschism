package net.forgottenschism.gui.layout;

public class LinearLayoutParameters implements LayoutParameters
{
	private Float weight;
	private Integer length;

	public LinearLayoutParameters()
	{
		weight = null;
		length = null;
	}

	public Float getWeight()
	{
		return weight;
	}

	public void setWeight(Float weight)
	{
		this.weight = weight;
	}

	public Integer getLength()
	{
		return length;
	}

	public void setLength(Integer length)
	{
		this.length = length;
	}
}
