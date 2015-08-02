package net.forgottenschism.gui;

import net.forgottenschism.gui.event.KeyEventListener;
import net.forgottenschism.gui.focus.FocusCycleRoot;
import net.forgottenschism.gui.focus.KeyboardFocusManager;

public interface Control extends GuiComponent, KeyEventListener
{
	boolean isEnabled();

	void setEnabled(boolean enabled);

	boolean isVisible();

	void setVisible(boolean visible);

	boolean isFocusable();

	Size2d getPreferredSize();

	int getForwardFocusTraversalKey();

	int getBackwardFocusTraversalKey();

	int getUpwardFocusTraversalKey();

	boolean isFocusTraversalOnKeyPressed();

	void setParentControl(ControlGroup parent);

	ControlGroup getParentControl();

	FocusCycleRoot getParentFocusCycleRoot();

	void setParentFocusCycleRoot(FocusCycleRoot parentFocusCycleRoot);

	void setKeyboardFocusManager(KeyboardFocusManager keyboardFocusManager);

	KeyboardFocusManager getKeyboardFocusManager();
}
