package net.forgottenschism.engine;

import org.newdawn.slick.Image;
import org.newdawn.slick.SlickException;

public class GameAssets
{
	private static GameAssets instance;

	private Image tileCursor;

	private GameAssets()
	{
		loadAssets();
	}

	public static GameAssets getInstance()
	{
		if(instance==null)
			instance = new GameAssets();

		return instance;
	}

	private static Image loadTileImage(String resourcePath)
	{
		try
		{
			Image image = new Image(resourcePath);

			image.rotate(90);

			return image;
		}
		catch(SlickException exception)
		{
			throw new RuntimeException(exception);
		}
	}

	private void loadAssets()
	{
		tileCursor = loadTileImage("cursor.png");
	}

	public Image getTileCursor()
	{
		return tileCursor;
	}
}
