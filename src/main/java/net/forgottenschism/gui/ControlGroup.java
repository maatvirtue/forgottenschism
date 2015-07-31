package net.forgottenschism.gui;

import java.util.List;

public interface ControlGroup extends Control
{
	void addControl(Control control);

	void removeControl(Control control);

	void removeAllChildren();

	List<Control> getChildren();
}
