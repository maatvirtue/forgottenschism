package net.forgottenschism.gui.theme;

import net.forgottenschism.util.FontUtil;
import org.newdawn.slick.Font;

import java.io.IOException;

public class Theme
{
	private static Theme defaultTheme;

	private ColorTheme colorTheme;
	private Font defaultFont;

	private Theme()
	{
		//Do nothing
	}

	public static Theme getDefaultTheme()
	{
		if(defaultTheme==null)
			generateDefaultTheme();

		return defaultTheme;
	}

	private static void generateDefaultTheme()
	{
		defaultTheme = new Theme();

		defaultTheme.setColorTheme(ColorTheme.getDefaultColorTheme());

		try
		{
			defaultTheme.setDefaultFont(FontUtil.loadTtfFromResource("font/liberationmono.ttf", 24));
		}
		catch(IOException e)
		{
			throw new RuntimeException(e);
		}
	}

	public ColorTheme getColorTheme()
	{
		return colorTheme;
	}

	private void setColorTheme(ColorTheme colorTheme)
	{
		this.colorTheme = colorTheme;
	}

	public Font getDefaultFont()
	{
		return defaultFont;
	}

	private void setDefaultFont(Font defaultFont)
	{
		this.defaultFont = defaultFont;
	}
}
