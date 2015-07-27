package net.forgottenschism.world;

import org.newdawn.slick.Image;

public class Terrain
{
	private Image image;

	public Terrain(Image image)
	{
		this.image = image;
	}

	@Override
	public String toString()
	{
		return "[image: "+image.toString()+"]";
	}

	public Image getImage()
	{
		return image;
	}

	public void setImage(Image image)
	{
		this.image = image;
	}
}
