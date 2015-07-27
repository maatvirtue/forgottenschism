package net.forgottenschism.gui;

import net.forgottenschism.engine.GameComponent;

public interface FocusManager extends GameComponent, InputCapable
{
	void addControl(Control control);

	void removeControl(Control control);
}
