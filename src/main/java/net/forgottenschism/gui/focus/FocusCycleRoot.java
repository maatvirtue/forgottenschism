package net.forgottenschism.gui.focus;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.ControlGroup;

import java.util.List;

public interface FocusCycleRoot extends ControlGroup
{
	void addControlToCycle(Control control);

	void removeControlFromCycle(Control control);

	List<Control> getCycleChildren();

	int getDownwardFocusTraversalKey();

	FocusTraversalPolicy getFocusTraversalPolicy();
}
