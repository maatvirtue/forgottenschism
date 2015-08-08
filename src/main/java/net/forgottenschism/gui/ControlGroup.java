package net.forgottenschism.gui;

import net.forgottenschism.gui.focus.FocusTraversalPolicy;

import java.util.List;

public interface ControlGroup extends Control
{
	void addControl(Control control);

	void removeControl(Control control);

	void removeAllChildren();

	List<Control> getChildren();

	FocusTraversalPolicy getFocusTraversalPolicy();

	String getControlHierarchy(int tabLevel);
}
