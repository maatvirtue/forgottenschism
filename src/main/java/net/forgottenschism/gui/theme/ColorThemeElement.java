package net.forgottenschism.gui.theme;

public enum ColorThemeElement
{
	LABEL_NORMAL,
	LABEL_BOLD,
	LABEL_TITLE,
	TEXTBOX_BACKGROUND_COLOR,
	CONTROL_ACTION_PENDING,
	CONTROL_ENABLED_FOCUSED,
	CONTROL_ENABLED_NOFOCUS,
	CONTROL_DISABLED_FOCUSED,
	CONTROL_DISABLED_NOFOCUS,
	WINDOW_DEFAULT_BACKGROUND;

	/**
	 * Gets ColorThemeElement depending on the control's enable and hasFocus properties.
	 */
	public static ColorThemeElement getControlElement(boolean enabled, boolean hasFocus)
	{
		if(enabled)
			if(hasFocus)
				return CONTROL_ENABLED_FOCUSED;
			else
				return CONTROL_ENABLED_NOFOCUS;
		else if(hasFocus)
			return CONTROL_DISABLED_FOCUSED;
		else
			return CONTROL_DISABLED_NOFOCUS;
	}
}
