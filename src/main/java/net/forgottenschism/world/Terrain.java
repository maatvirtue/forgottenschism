package net.forgottenschism.world;

import net.forgottenschism.gui.bean.Size2d;
import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;

public class Terrain
{
	public static final Size2d PIXEL_SIZE = new Size2d(100, 100);

	private Image image;

	public Terrain(Image image)
	{
		this.image = image;
	}

	public static Terrain loadTerrain(String resourcePath)
	{
		try
		{
			Image image = new Image(resourcePath);

			image.rotate(90);

			return new Terrain(image);
		}
		catch(SlickException exception)
		{
			throw new RuntimeException(exception);
		}
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
