package net.forgottenschism.util;

import org.newdawn.slick.TrueTypeFont;
import org.newdawn.slick.util.ResourceLoader;

import java.awt.*;
import java.io.IOException;
import java.io.InputStream;

public class FontUtil
{
	public static TrueTypeFont loadTtfFromResource(String resourcePath, float fontSize) throws IOException
	{
		try
		{
			InputStream inputStream = ResourceLoader.getResourceAsStream(resourcePath);

			java.awt.Font awtFont = java.awt.Font.createFont(java.awt.Font.TRUETYPE_FONT, inputStream);
			awtFont = awtFont.deriveFont(fontSize);

			return new TrueTypeFont(awtFont, true);
		}
		catch(FontFormatException exception)
		{
			throw new IOException("Unable to load font", exception);
		}
	}
}
