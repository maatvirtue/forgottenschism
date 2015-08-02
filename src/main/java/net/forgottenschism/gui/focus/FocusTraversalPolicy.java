package net.forgottenschism.gui.focus;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.ControlGroup;

public interface FocusTraversalPolicy
{
	Control getNextFocusControl(FocusCycleRoot focusCycleRoot, Control currentFocusedControl);

	Control getPreviousFocusControl(FocusCycleRoot focusCycleRoot, Control currentFocusedControl);

	Control getFirstFocusControl(FocusCycleRoot focusCycleRoot);

	Control getLastFocusControl(FocusCycleRoot focusCycleRoot);

	Control getDefaultFocusControl(FocusCycleRoot focusCycleRoot);
}
