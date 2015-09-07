package net.forgottenschism.gui.layout;

public class LinearLayoutParameters implements LayoutParameters
{
	private Float weight;
	private Integer length;

	public LinearLayoutParameters()
	{
		this(null, null);
	}

	public LinearLayoutParameters(Float weight, Integer length)
	{
		this.weight = weight;
		this.length = length;
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
