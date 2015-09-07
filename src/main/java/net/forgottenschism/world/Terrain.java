package net.forgottenschism.world;

import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;

public enum Terrain
{
	BLUE("terrain/blue.png"),
	RED("terrain/red.png");

	private Image image;

	private Terrain(String resourcePath)
	{
		try
		{
			image = new Image(resourcePath);

			image.rotate(90);
		}
		catch(SlickException exception)
		{
			throw new RuntimeException(exception);
		}
	}

	@Override
	public String toString()
	{
		return name();
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
