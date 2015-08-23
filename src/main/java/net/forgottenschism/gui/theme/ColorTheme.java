package net.forgottenschism.gui.theme;

import org.newdawn.slick.Color;

import java.util.HashMap;
import java.util.Map;

public class ColorTheme
{
	private static ColorTheme defaultColorTheme;

	private Map<ColorThemeElement, Color> themeMap;

	private ColorTheme()
	{
		themeMap = new HashMap<>();
	}

	public static ColorTheme getDefaultColorTheme()
	{
		if(defaultColorTheme==null)
			generateDefaultColorTheme();

		return defaultColorTheme;
	}

	private static void generateDefaultColorTheme()
	{
		defaultColorTheme = new ColorTheme();

		defaultColorTheme.setColor(ColorThemeElement.LABEL_NORMAL, Color.white);
		defaultColorTheme.setColor(ColorThemeElement.LABEL_BOLD, new Color(91, 26, 0));
		defaultColorTheme.setColor(ColorThemeElement.LABEL_TITLE, Color.orange);
		defaultColorTheme.setColor(ColorThemeElement.CONTROL_ACTION_PENDING, Color.yellow);
		defaultColorTheme.setColor(ColorThemeElement.CONTROL_ENABLED_FOCUSED, Color.red);
		defaultColorTheme.setColor(ColorThemeElement.CONTROL_ENABLED_NOFOCUS, new Color(50, 50, 50));
		defaultColorTheme.setColor(ColorThemeElement.CONTROL_DISABLED_FOCUSED, Color.orange);
		defaultColorTheme.setColor(ColorThemeElement.CONTROL_DISABLED_NOFOCUS, Color.gray);
		defaultColorTheme.setColor(ColorThemeElement.WINDOW_DEFAULT_BACKGROUND, Color.blue);
		defaultColorTheme.setColor(ColorThemeElement.TEXTBOX_BACKGROUND_COLOR, Color.white);
		defaultColorTheme.setColor(ColorThemeElement.SPINNER_BACKGROUND_COLOR, Color.blue);
	}

	public Color getColor(ColorThemeElement key)
	{
		return themeMap.get(key);
	}

	/**
	 * Gets color depending on the control's enable and hasFocus properties.
	 */
	public Color getStatusColor(boolean enabled, boolean hasFocus)
	{
		return getColor(ColorThemeElement.getControlElement(enabled, hasFocus));
	}

	private void setColor(ColorThemeElement key, Color color)
	{
		themeMap.put(key, color);
	}
}
