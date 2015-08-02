package net.forgottenschism.gui.focus;

import net.forgottenschism.gui.Control;
import net.forgottenschism.gui.event.KeyEventDispatcher;
import net.forgottenschism.gui.event.KeyEventPostProcessor;

public interface KeyboardFocusManager extends KeyEventDispatcher, KeyEventPostProcessor
{
	void focusDefaultControl();

	void focusNextControl();

	void focusPreviousControl();

	void upFocusCycle();

	void downFocusCycle();

	Control getFocusedControl();

	void notifyNewControlAdded();
}
