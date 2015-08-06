package net.forgottenschism.gui.bean;

public enum Orientation2d
{
	HORIZONTAL,
	VERTICAL;

	public static Orientation2d getOtherOrientation(Orientation2d orientation)
	{
		if(orientation==HORIZONTAL)
			return VERTICAL;
		else
			return HORIZONTAL;
	}
}
